using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class BolinhaController : MonoBehaviour
{
    [Header("Referencias")] public BolinhaController inimigo;

    [Header("Movimento")] public float velocidade = 10f;

    [Header("Empurrao")] public float forcaBase = 20f;
    public float distanciaMaxima = 10f;
    public float cooldown = 2f;

    [Header("Input")] public InputActionReference moveAction;
    public InputActionReference pushAction;
    
    [Header("UI")]
    public UnityEvent<float> OnCooldownChanged;
    
    [Header("Jogador")]
    public int playerID;
    
    private Rigidbody rb;
    private float ultimoPush;
    public float forcaEmpurrao = 15f;
    public float alcance = 5f;


    private float ultimoEmpurrao;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnEnable()
    {
        moveAction.action.Enable();

        pushAction.action.Enable();
        pushAction.action.performed += OnPush;
    }

    void OnDisable()
    {
        moveAction.action.Disable();

        pushAction.action.performed -= OnPush;
        pushAction.action.Disable();
    }

    void FixedUpdate()
    {
        Vector2 move = moveAction.action.ReadValue<Vector2>();

        Vector3 dir = new Vector3(move.x, 0, move.y);

        rb.AddForce(dir * velocidade);
    }

    void OnPush(InputAction.CallbackContext ctx)
    {
        if (Time.time < ultimoPush + cooldown)
            return;

        ultimoPush = Time.time;

        if (inimigo == null)
            return;

        Vector3 direcao =
            (inimigo.transform.position - transform.position).normalized;

        float distancia =
            Vector3.Distance(transform.position,
                inimigo.transform.position);

        float multiplicador =
            Mathf.Clamp01(1f - (distancia / distanciaMaxima));

        float forcaFinal =
            forcaBase * (1f + multiplicador);

        inimigo.rb.AddForce(
            direcao * forcaFinal,
            ForceMode.Impulse);
    }
    void Update()
    {
        float valor =
            Mathf.Clamp01(
                (Time.time - ultimoPush) / cooldown);

        PlayerOM.OnCooldownMudou?.Invoke(playerID, valor);
    }
}
    