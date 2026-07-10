using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public static GameSetup Instance;

    public BolinhaData jogador1;
    public BolinhaData jogador2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
}