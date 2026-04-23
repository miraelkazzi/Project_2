using UnityEngine;
using TMPro;

public class PlayerCoins : MonoBehaviour
{
    public int coins = 0;

    public TMP_Text coinText;

    private void Start()
    {
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = coins.ToString();
        }
    }
}