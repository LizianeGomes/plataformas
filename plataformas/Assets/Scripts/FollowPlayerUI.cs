using UnityEngine;

public class FollowPlayerUI : MonoBehaviour
{
    public Transform alvo;
    public Vector3 offset = new Vector3(0, 2f, 0);

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (alvo == null) return;

        Vector3 pos = cam.WorldToScreenPoint(alvo.position + offset);

        transform.position = pos;
    }
}