using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public int playerID;
    public Slider barra;

    private void OnEnable()
    {
        PlayerOM.OnCooldownMudou += Atualizar;
    }

    private void OnDisable()
    {
        PlayerOM.OnCooldownMudou -= Atualizar;
    }

    void Atualizar(int id, float valor)
    {
        if (id != playerID)
            return;

        barra.value = valor;
    }
}