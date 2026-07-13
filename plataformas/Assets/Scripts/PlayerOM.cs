using System;

public static class PlayerOM
{
    public static Action<int, int> OnCoinAdded;
    public static Action<int, float> OnCooldownMudou;

    public static void AddCoin(int playerID, int quantidade)
    {
        OnCoinAdded?.Invoke(playerID, quantidade);
    }
}