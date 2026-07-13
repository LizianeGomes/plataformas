using TMPro;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    public TextMeshProUGUI textoMoedasP1;
    public TextMeshProUGUI textoMoedasP2;

    private void OnEnable()
    {
        PlayerOM.OnCoinAdded += AtualizarTexto;
    }

    private void OnDisable()
    {
        PlayerOM.OnCoinAdded -= AtualizarTexto;
    }

    void AtualizarTexto(int playerID, int quantidade)
{
    if (playerID == 1)
        textoMoedasP1.text = "Moedas P1: " + quantidade;
    else
        textoMoedasP2.text = "Moedas P2: " + quantidade;
}
}