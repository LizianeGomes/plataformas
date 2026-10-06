using UnityEngine;

public class BallPreview : MonoBehaviour
{
    public Transform pontoPreview;
    public Camera cameraPreview;

    [Header("Prefab base da bolinha")]
    public GameObject prefabBolinha;

    private GameObject bolinhaAtual;

    public void MostrarBolinha(BolinhaData dados)
    {
        
        if (bolinhaAtual != null)
            Destroy(bolinhaAtual);

        
        bolinhaAtual = Instantiate(
            prefabBolinha,
            pontoPreview.position,
            Quaternion.identity
        );

        
        bolinhaAtual.transform.SetParent(pontoPreview);

       
        bolinhaAtual.transform.localScale =
            Vector3.one * dados.visualScale;

        
        Renderer renderer = bolinhaAtual.GetComponentInChildren<Renderer>();

        if (renderer != null && dados.material != null)
        {
            renderer.material = dados.material;
        }

        
        Rigidbody rb = bolinhaAtual.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

       
        Collider col = bolinhaAtual.GetComponent<Collider>();

        if (col != null)
            col.enabled = false;

        cameraPreview.transform.position =
            pontoPreview.position + new Vector3(0, 0, -5);

        
        cameraPreview.transform.LookAt(
            bolinhaAtual.transform
        );
    }
}