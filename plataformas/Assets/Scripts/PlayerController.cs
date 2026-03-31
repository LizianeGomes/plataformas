using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("Acceleration applied to the ball (ForceMode.Acceleration)")]
    [SerializeField] private float speed = 15f;
    [Tooltip("Maximum horizontal speed (m/s)")]
    [SerializeField] private float maxSpeed = 8f;
    [Tooltip("If true, movement directions are relative to the camera's forward/right. Otherwise world-relative.")]
    [SerializeField] private bool useCameraRelative = true;
    [Tooltip("Optional camera transform; if null the scene's main camera will be used")]
    [SerializeField] private Transform cameraTransform;

    [Header("Input")]
    [Tooltip("Assign the Move action (from your Input Actions asset). Use an Input Action Reference pointing to the Player/Move action.")]
    [SerializeField] private InputActionReference moveAction;

    [Header("Debug")]
    [Tooltip("Enable verbose debug logging for input and applied forces (useful while debugging movement issues)")]
    [SerializeField] private bool verboseLogging = false;

    private Vector2 _moveInput = Vector2.zero; // cached input from the Input System
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb == null)
            Debug.LogError("PlayerController requires a Rigidbody but none was found.");

        // Quick runtime checks to surface common configuration problems that stop movement
        if (_rb != null)
        {
            if (_rb.isKinematic)
                Debug.LogWarning("Player Rigidbody is kinematic — physics forces won't move it. Set isKinematic=false to allow movement.");
            if (gameObject.isStatic)
                Debug.LogWarning("Player GameObject is marked Static — Unity may prevent physics-driven movement.");
        }
    }

    private void OnEnable()
    {
        // ensure camera reference
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        if (moveAction != null && moveAction.action != null)
        {
            moveAction.action.performed += OnMovePerformed;
            moveAction.action.canceled += OnMoveCanceled;
            // Ensure the action is enabled so it will produce values. If it's part of a larger asset
            // that you enable elsewhere, this call is safe because Enable is idempotent.
            moveAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (moveAction != null && moveAction.action != null)
        {
            moveAction.action.performed -= OnMovePerformed;
            moveAction.action.canceled -= OnMoveCanceled;
            // Do not disable the whole asset here; we only disable the specific action reference if desired.
            moveAction.action.Disable();
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        _moveInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        // convert 2D input to 3D movement direction
        Vector3 inputDir = new Vector3(_moveInput.x, 0f, _moveInput.y);

        // If no Input Action Reference is assigned or enabled, try to read input directly
        // from common devices so the player still responds. This allows the script to
        // work even if the moveAction reference wasn't set in the inspector.
        if ((moveAction == null || moveAction.action == null || !moveAction.action.enabled) && _moveInput == Vector2.zero)
        {
            // Try Gamepad
            if (UnityEngine.InputSystem.Gamepad.current != null)
            {
                Vector2 gp = UnityEngine.InputSystem.Gamepad.current.leftStick.ReadValue();
                if (gp != Vector2.zero)
                    inputDir = new Vector3(gp.x, 0f, gp.y);
            }
            // Try Keyboard (WASD / arrow keys)
            else if (UnityEngine.InputSystem.Keyboard.current != null)
            {
                var kb = UnityEngine.InputSystem.Keyboard.current;
                float x = (kb.dKey.isPressed ? 1f : 0f) + (kb.rightArrowKey.isPressed ? 1f : 0f) - (kb.aKey.isPressed ? 1f : 0f) - (kb.leftArrowKey.isPressed ? 1f : 0f);
                float y = (kb.wKey.isPressed ? 1f : 0f) + (kb.upArrowKey.isPressed ? 1f : 0f) - (kb.sKey.isPressed ? 1f : 0f) - (kb.downArrowKey.isPressed ? 1f : 0f);
                if (Mathf.Abs(x) > 0f || Mathf.Abs(y) > 0f)
                    inputDir = new Vector3(x, 0f, y);
            }
            // Fallback to legacy Input (if project still has it enabled)
            else
            {
                float lx = UnityEngine.Input.GetAxisRaw("Horizontal");
                float ly = UnityEngine.Input.GetAxisRaw("Vertical");
                if (Mathf.Abs(lx) > 0f || Mathf.Abs(ly) > 0f)
                    inputDir = new Vector3(lx, 0f, ly);
            }
        }

        Vector3 moveDir;
        if (useCameraRelative && cameraTransform != null)
        {
            Vector3 camForward = cameraTransform.forward;
            camForward.y = 0f;
            camForward.Normalize();

            Vector3 camRight = cameraTransform.right;
            camRight.y = 0f;
            camRight.Normalize();

            moveDir = camRight * inputDir.x + camForward * inputDir.z;
        }
        else
        {
            moveDir = inputDir;
        }

        if (moveDir.sqrMagnitude > 0f)
        {
            // apply acceleration-style force so physics integration handles momentum
            Vector3 applied = moveDir.normalized * speed;
            _rb.AddForce(applied, ForceMode.Acceleration);
            if (verboseLogging)
            {
                Debug.Log($"Applying force {applied} (speed={speed}) — inputDir={inputDir} moveDir={moveDir}");
            }
        }

        if (verboseLogging)
        {
            // draw direction we want to move along so user can visually inspect
            Debug.DrawRay(transform.position, moveDir.normalized, Color.green);
        }

        // clamp horizontal velocity to maxSpeed while preserving vertical velocity (gravity, jumps, slopes)
        // Use Rigidbody.linearVelocity (project uses this API); velocity is marked obsolete in this project.
        Vector3 currentVel = _rb.linearVelocity;
        Vector3 horizontalVel = new Vector3(currentVel.x, 0f, currentVel.z);
        float horizontalSpeed = horizontalVel.magnitude;
        if (horizontalSpeed > maxSpeed)
        {
            Vector3 limitedHorizontal = horizontalVel.normalized * maxSpeed;
            _rb.linearVelocity = new Vector3(limitedHorizontal.x, currentVel.y, limitedHorizontal.z);
        }

        // Additional diagnostics to help debug why movement may not occur
        if (verboseLogging)
        {
            Debug.Log($"[PlayerController] input={_moveInput} inputDir={inputDir} moveDir={moveDir} currentVel={currentVel} isKinematic={_rb.isKinematic} isStatic={gameObject.isStatic} constraints={_rb.constraints}");
        }
    }

    // Optional: allow other scripts to read current input
    public Vector2 GetMoveInput() => _moveInput;
}








