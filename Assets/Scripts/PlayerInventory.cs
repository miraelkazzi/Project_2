using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public int coins = 0;

    public int healthItems = 0;
    public int ammoItems = 0;
    public int shieldItems = 0;

    public TMP_Text coinText;
    public TMP_Text healthCountText;
    public TMP_Text ammoCountText;
    public TMP_Text shieldCountText;


    private void Start()
    {
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    public bool SpendCoins(int amount)
    {
        if (coins < amount) return false;

        coins -= amount;
        UpdateUI();
        return true;
    }

    public void AddHealthItem()
    {
        healthItems++;
        UpdateUI();
    }

    public void AddAmmoItem()
    {
        ammoItems++;
        UpdateUI();
    }

    public void AddShieldItem()
    {
        shieldItems++;
        UpdateUI();
    }

    public void UseHealth()
    {
        if (healthItems <= 0) return;

        healthItems--;

        PlayerHealth health = GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.Heal(10);
        }

        UpdateUI();
    }

    public void UseAmmo()
    {
        if (ammoItems <= 0) return;

        ammoItems--;

        GunShoot gun = GetComponent<GunShoot>();
        if (gun != null)
        {
            gun.RefillAmmo();
        }

        UpdateUI();
    }

    public void UseShield()
    {
        if (shieldItems <= 0) return;

        shieldItems--;

        PlayerHealth health = GetComponent<PlayerHealth>();
        if (health != null)
        {
            health.ActivateShield(10f);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        if (coinText != null) coinText.text = coins.ToString();
        if (healthCountText != null) healthCountText.text = healthItems.ToString();
        if (ammoCountText != null) ammoCountText.text = ammoItems.ToString();
        if (shieldCountText != null) shieldCountText.text = shieldItems.ToString();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            UseHealth();
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            UseAmmo();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            UseShield();
        }
    }
}