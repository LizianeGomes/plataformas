using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject moedaPrefab;

    public float tempoSpawn = 5f;

    public Vector3 centroArena;

    public Vector3 tamanhoArena;
    
    public int maxMoedas = 10;

    void Start()
    {
        InvokeRepeating(nameof(SpawnMoeda), 2f, tempoSpawn);
    }

    void SpawnMoeda()
    {
        if (GameObject.FindGameObjectsWithTag("Coin").Length >= maxMoedas)
            return;
        Vector3 posicao = new Vector3(
            Random.Range(
                centroArena.x - tamanhoArena.x / 2,
                centroArena.x + tamanhoArena.x / 2),

            1f,

            Random.Range(
                centroArena.z - tamanhoArena.z / 2,
                centroArena.z + tamanhoArena.z / 2));

        Instantiate(moedaPrefab, posicao, Quaternion.identity);
    }
}