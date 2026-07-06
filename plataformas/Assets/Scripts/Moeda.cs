using UnityEngine;

public class Moeda : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BolinhaController jogador = other.GetComponent<BolinhaController>();

        if (jogador != null)
        {
            jogador.ColetarMoeda();
            Destroy(gameObject);
        }
    }
}