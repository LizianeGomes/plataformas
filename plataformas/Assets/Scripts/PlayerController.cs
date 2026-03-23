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

    private Vector2 _moveInput = Vector2.zero; // cached input from the Input System
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
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
            _rb.AddForce(moveDir.normalized * speed, ForceMode.Acceleration);
        }

        // clamp horizontal velocity to maxSpeed while preserving vertical velocity (gravity, jumps, slopes)
        Vector3 horizontalVel = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
        float horizontalSpeed = horizontalVel.magnitude;
        if (horizontalSpeed > maxSpeed)
        {
            Vector3 limitedHorizontal = horizontalVel.normalized * maxSpeed;
            _rb.linearVelocity = new Vector3(limitedHorizontal.x, _rb.linearVelocity.y, limitedHorizontal.z);
        }
    }

    // Optional: allow other scripts to read current input
    public Vector2 GetMoveInput() => _moveInput;
}




