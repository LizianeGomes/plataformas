using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
    int moedas = 0;

    private void OnEnable()
    {
        PlayerOM.OnMoedaColetada += AdicionarMoeda;
    }

    private void OnDisable()
    {
        PlayerOM.OnMoedaColetada -= AdicionarMoeda;
    }

    void AdicionarMoeda(int valor)
    {
        moedas += valor;

        Debug.Log("Pegou moeda");
    }

    
}