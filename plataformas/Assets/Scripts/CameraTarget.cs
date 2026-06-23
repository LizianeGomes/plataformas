using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    void Update()
    {
        transform.position =
            (player1.position + player2.position) / 2f;
    }
}