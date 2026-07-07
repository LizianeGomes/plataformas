using UnityEngine;

[CreateAssetMenu(fileName = "Bolinha", menuName = "Sumo/Bolinha")]
public class BolinhaData : ScriptableObject
{
    public string nome;

    public Material material;

    public float velocidade = 10;

    public float forca = 20;

    public float massa = 1;

    public float tamanho = 1;
}