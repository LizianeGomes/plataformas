using System;

public static class PlayerOM
{
   
    public static Action<int> OnCoinAdded;
    public static Action<int, float> OnCooldownMudou;

    public static void AddCoin(int playerID)
    {
        OnCoinAdded?.Invoke(playerID);
    }
}