using TMPro;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    public BolinhaData[] bolinhas;

    public TMP_Text textoP1;
    public TMP_Text textoP2;

    int indiceP1;
    int indiceP2;

    bool confirmouP1;
    bool confirmouP2;

    void Start()
    {
        AtualizarTela();
    }

    void Update()
    {
        // Jogador 1
        if (!confirmouP1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                indiceP1--;
                if (indiceP1 < 0)
                    indiceP1 = bolinhas.Length - 1;

                AtualizarTela();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                indiceP1++;
                if (indiceP1 >= bolinhas.Length)
                    indiceP1 = 0;

                AtualizarTela();
            }

            if (Input.GetKeyDown(KeyCode.Space))
                confirmouP1 = true;
        }

        // Jogador 2
        if (!confirmouP2)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                indiceP2--;
                if (indiceP2 < 0)
                    indiceP2 = bolinhas.Length - 1;

                AtualizarTela();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                indiceP2++;
                if (indiceP2 >= bolinhas.Length)
                    indiceP2 = 0;

                AtualizarTela();
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter))
                confirmouP2 = true;
        }

        if (confirmouP1 && confirmouP2)
        {
            Debug.Log("Os dois confirmaram!");

            GameSetup.Instance.jogador1 = bolinhas[indiceP1];
            GameSetup.Instance.jogador2 = bolinhas[indiceP2];

            GameManager.Instance.CarregarCena("SampleScene");
        }
    }

    void AtualizarTela()
    {
        textoP1.text = bolinhas[indiceP1].ballName;
        textoP2.text = bolinhas[indiceP2].ballName;
    }
}