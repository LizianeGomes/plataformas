using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    public float minDistance = 10f;
    public float maxDistance = 20f;

    public float minZoom = 12f;
    public float maxZoom = 25f;

    void LateUpdate()
    {
        float distancia =
            Vector3.Distance(player1.position,
                player2.position);

        float zoom =
            Mathf.Lerp(
                minZoom,
                maxZoom,
                distancia / maxDistance);

        transform.position = new Vector3(
            0,
            zoom,
            -zoom);

        transform.LookAt(
            (player1.position + player2.position) / 2f);
    }
}