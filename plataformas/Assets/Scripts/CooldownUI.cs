using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public int playerID;
    public Image barra;

    private BolinhaController jogador;

    void Start()
    {
        GameObject obj = GameObject.Find("BolaP" + playerID);

        if (obj != null)
            jogador = obj.GetComponent<BolinhaController>();
    }

    void Update()
    {
        if (jogador == null)
            return;

        barra.fillAmount = jogador.GetCooldownPercent();
    }
}