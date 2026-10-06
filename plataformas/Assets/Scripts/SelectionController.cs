using TMPro;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    [Header("Bolinha")]
    public BolinhaData[] bolinhas;

    [Header("Nome")]
    public TMP_Text textoP1;
    public TMP_Text textoP2;

    [Header("Preview 3D")]
    public BallPreview previewP1;
    public BallPreview previewP2;

    [Header("Atributos P1")]
    public TMP_Text velocidadeP1;
    public TMP_Text forcaP1;
    public TMP_Text massaP1;

    [Header("Atributos P2")]
    public TMP_Text velocidadeP2;
    public TMP_Text forcaP2;
    public TMP_Text massaP2;

    int indiceP1;
    int indiceP2;

    bool confirmouP1;
    bool confirmouP2;
    bool carregandoCena;

    void Start()
    {
        AtualizarP1();
        AtualizarP2();
    }

    void Update()
    {
        

        if (!confirmouP1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                indiceP1--;

                if (indiceP1 < 0)
                    indiceP1 = bolinhas.Length - 1;

                AtualizarP1();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                indiceP1++;

                if (indiceP1 >= bolinhas.Length)
                    indiceP1 = 0;

                AtualizarP1();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                confirmouP1 = true;

                Debug.Log(
                    "Jogador 1 escolheu: " +
                    bolinhas[indiceP1].ballName
                );
            }
        }

        

        if (!confirmouP2)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                indiceP2--;

                if (indiceP2 < 0)
                    indiceP2 = bolinhas.Length - 1;

                AtualizarP2();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                indiceP2++;

                if (indiceP2 >= bolinhas.Length)
                    indiceP2 = 0;

                AtualizarP2();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                confirmouP2 = true;

                Debug.Log(
                    "Jogador 2 escolheu: " +
                    bolinhas[indiceP2].ballName
                );
            }
        }

        

        if (confirmouP1 && confirmouP2 && !carregandoCena)
        {
            carregandoCena = true;

            GameSetup.Instance.jogador1 =
                bolinhas[indiceP1];

            GameSetup.Instance.jogador2 =
                bolinhas[indiceP2];

            Debug.Log(
                "P1: " +
                GameSetup.Instance.jogador1.ballName
            );

            Debug.Log(
                "P2: " +
                GameSetup.Instance.jogador2.ballName
            );

            GameManager.Instance.CarregarCena("SampleScene");
        }
    }

    

    void AtualizarP1()
    {
        BolinhaData dados = bolinhas[indiceP1];

     
        if (textoP1 != null)
            textoP1.text = dados.ballName;

      
        if (previewP1 != null)
            previewP1.MostrarBolinha(dados);

        
        if (velocidadeP1 != null)
            velocidadeP1.text =
                "Velocidade: " + dados.initialVelocity.ToString("0.0");

        if (forcaP1 != null)
            forcaP1.text =
                "Força: " + dados.basePushForce.ToString("0.0");

        if (massaP1 != null)
            massaP1.text =
                "Massa: " + dados.baseMass.ToString("0.0");

        Debug.Log(
            "P1 selecionou: " + dados.ballName
        );
    }

  

    void AtualizarP2()
    {
        BolinhaData dados = bolinhas[indiceP2];

       
        if (textoP2 != null)
            textoP2.text = dados.ballName;

        
        if (previewP2 != null)
            previewP2.MostrarBolinha(dados);

       
        if (velocidadeP2 != null)
            velocidadeP2.text =
                "Velocidade: " + dados.initialVelocity.ToString("0.0");

        if (forcaP2 != null)
            forcaP2.text =
                "Força: " + dados.basePushForce.ToString("0.0");

        if (massaP2 != null)
            massaP2.text =
                "Massa: " + dados.baseMass.ToString("0.0");

        Debug.Log(
            "P2 selecionou: " + dados.ballName
        );
    }
}