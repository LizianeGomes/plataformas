using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BolinhaController jogador =
            other.GetComponent<BolinhaController>();

        if (jogador != null)
        {
            jogador.PerderVida();
        }
    }
}