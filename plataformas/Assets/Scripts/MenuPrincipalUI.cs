using UnityEngine;

public class MenuPrincipalUI : MonoBehaviour
{
    public void IniciarJogo()
    {
        GameManager.Instance.CarregarCena("SelecaoBolinhas");
    }

    public void SairJogo()
    {
        Application.Quit();
    }
}