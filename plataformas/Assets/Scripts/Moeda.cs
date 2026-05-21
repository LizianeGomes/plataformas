using UnityEngine;

public class Moeda : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            PlayerOM.ColetarMoeda(1);

         
            Destroy(gameObject);
        }
    }
}