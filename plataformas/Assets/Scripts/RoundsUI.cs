using UnityEngine;
using UnityEngine.UI;

public class RoundsUI : MonoBehaviour
{
    public Image p1Round1;
    public Image p1Round2;

    public Image p2Round1;
    public Image p2Round2;

    BolinhaController jogador1;
    BolinhaController jogador2;

    void Start()
    {
        ProcurarJogadores();
    }

    void ProcurarJogadores()
    {
        BolinhaController[] jogadores =
            FindObjectsByType<BolinhaController>(FindObjectsSortMode.None);

        foreach (BolinhaController jogador in jogadores)
        {
            if (jogador.playerID == 1)
                jogador1 = jogador;
            else if (jogador.playerID == 2)
                jogador2 = jogador;
        }
    }

    void Update()
    {
        if (jogador1 == null || jogador2 == null)
        {
            ProcurarJogadores();
            return;
        }

        p1Round1.enabled = jogador1.vidas >= 2;
        p1Round2.enabled = jogador1.vidas >= 1;

        p2Round1.enabled = jogador2.vidas >= 2;
        p2Round2.enabled = jogador2.vidas >= 1;
    }
}