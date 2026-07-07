using TMPro;
using UnityEngine;

public class VictoryController : MonoBehaviour
{
    public TMP_Text texto;

    void Start()
    {
        texto.text = "JOGADOR " +
                     GameManager.Instance.vencedor +
                     " VENCEU!";
    }

    public void VoltarMenu()
    {
        GameManager.Instance.CarregarCena("MenuPrincipal");
    }
}