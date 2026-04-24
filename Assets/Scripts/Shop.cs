using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;

    public int healthPrice = 20;
    public int ammoPrice = 15;
    public int shieldPrice = 25;

    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = FindFirstObjectByType<PlayerInventory>();

        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);


        Time.timeScale = 0f;


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        var controller = FindFirstObjectByType<StarterAssets.FirstPersonController>();
        if (controller != null)
        {
            controller.enabled = false;
        }


        var gun = FindFirstObjectByType<GunShoot>();
        if (gun != null)
        {
            gun.enabled = false;
        }
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);


        Time.timeScale = 1f;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        var controller = FindFirstObjectByType<StarterAssets.FirstPersonController>();
        if (controller != null)
        {
            controller.enabled = true;
        }


        var gun = FindFirstObjectByType<GunShoot>();
        if (gun != null)
        {
            gun.enabled = true;
        }
    }

    public void BuyHealth()
    {
        if (playerInventory != null && playerInventory.SpendCoins(healthPrice))
        {
            playerInventory.AddHealthItem();
        }

        SoundManager.Instance.PlaySFX(SoundManager.Instance.buyItem);
    }

    public void BuyAmmo()
    {
        if (playerInventory != null && playerInventory.SpendCoins(ammoPrice))
        {
            playerInventory.AddAmmoItem();
        }
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buyItem);
    }

    public void BuyShield()
    {
        if (playerInventory != null && playerInventory.SpendCoins(shieldPrice))
        {
            playerInventory.AddShieldItem();
        }
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buyItem);
    }
}