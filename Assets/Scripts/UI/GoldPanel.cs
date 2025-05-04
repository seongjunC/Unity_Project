using System.Collections;
using TMPro;
using UnityEngine;

public class GoldPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text coin;

    private void Start()
    {
        coin.text = Manager.Game.gold.ToString();
    }

    private void OnEnable()
    {
        Manager.Game.OnGoldChanged += GoldChanged;
    }

    private void OnDisable()
    {
        Manager.Game.OnGoldChanged -= GoldChanged;
    }

    private void GoldChanged(int value)
    {
        coin.text = value.ToString();
    }
}
