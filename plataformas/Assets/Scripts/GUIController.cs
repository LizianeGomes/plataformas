using UnityEngine;
using TMPro;

public class GUIController : MonoBehaviour
{
    public TextMeshProUGUI textoMoedas;

    int moedas = 0;

    private void OnEnable()
    {
        PlayerOM.OnMoedaColetada += AtualizarMoedas;
    }

    private void OnDisable()
    {
        PlayerOM.OnMoedaColetada -= AtualizarMoedas;
    }

    void AtualizarMoedas(int valor)
    {
        moedas += valor;

        textoMoedas.text = "Moedas: " + moedas;
    }
}