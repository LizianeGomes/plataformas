using System;

public static class PlayerOM
{
  
    public static Action<int> OnMoedaColetada;

    
    public static void ColetarMoeda(int quantidade)
    {
        OnMoedaColetada?.Invoke(quantidade);
    }
}