using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private int _curGold;
    public int gold { get { return _curGold; }  private set { _curGold = value; OnGoldChanged?.Invoke(_curGold); } }

    public event Action<int> OnGoldChanged;

    public void AddGold(int amount) => gold += amount;

    public void RemoveGold(int amount)
    {
        if (gold - amount < 0)
            return;

        gold -= amount;
    }
}
