using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 10, -10);
    public float velocidade = 5f;

    void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 posicaoDesejada = player.position + offset;
        transform.position = Vector3.Lerp(
            transform.position, posicaoDesejada,velocidade * Time.deltaTime);
    }
}