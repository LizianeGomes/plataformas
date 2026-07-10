using UnityEngine;

[CreateAssetMenu(fileName = "NewBallData", menuName = "Sumo/BallData")]
public class BolinhaData : ScriptableObject
{
    public string ballName;
    public float initialVelocity;
    public float basePushForce;
    public float baseMass;
    public float visualScale = 1f;
    public float forceCooldownTime = 2f;
    public GameObject prefab;   
    
    public Color player1Color;
    public Color player2Color;
    public Material material;
} 