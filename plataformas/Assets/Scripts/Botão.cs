using UnityEngine;

public class Botão : MonoBehaviour
{
    public void VoltarMenu()
    {
        GameManager.Instance.CarregarCena("MenuPrincipal");
    }
}