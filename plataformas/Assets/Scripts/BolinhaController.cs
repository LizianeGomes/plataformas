using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class BolinhaController : MonoBehaviour

{
    [Header("Vidas")]
    public int vidas = 2;

    Vector3 posicaoInicial;
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
    
    
    [Header("Moedas")]
    public int moedas = 0;
    [SerializeField] float bonusForca = 2f;
    [SerializeField] float bonusMassa = 0.5f;
    [SerializeField] float perdaVelocidade = 0.5f;

    float velocidadeInicial;
    float forcaInicial;
    float massaInicial;
    
    private Rigidbody rb;
    private float ultimoPush;
    public float forcaEmpurrao = 15f;
    public float alcance = 5f;


    private float ultimoEmpurrao;
    
    
    
    public void ColetarMoeda()
    {
        moedas++;

        velocidade -= 0.5f;

        forcaBase += 2f;

        rb.mass += 0.5f;

        if (velocidade < 3f)
            velocidade = 3f;
        
        PlayerOM.AddCoin(playerID);

        Debug.Log(name + " coletou uma moeda. Total: " + moedas);
       
    }


    public float GetCooldownPercent()
    {
        return Mathf.Clamp01((Time.time - ultimoPush) / cooldown);
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        posicaoInicial = transform.position;
        velocidadeInicial = velocidade;
        forcaInicial = forcaBase;
        massaInicial = rb.mass;
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
    
    public void PerderVida()
    {
        vidas--;

        Debug.Log(name + " perdeu uma vida! Restam: " + vidas);

        if (vidas <= 0)
        {
            GameManager.Instance.FimDeJogo(this);
        }
        else
        {
            Respawn();
        }
    }

    void Respawn()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = posicaoInicial;
        transform.rotation = Quaternion.identity;
    }
}
    