using UnityEngine;
using TMPro;

public class GunShoot : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform firePoint;

    public Transform weaponHolder;
    private GameObject currentWeaponObject;

    private float nextFireTime;
    private int currentAmmo;


    public TMP_Text ammoText;

    private void Start()
    {
        if (weaponData != null)
        {
            currentAmmo = weaponData.maxAmmo;

            SpawnWeaponModel();
            UpdateAmmoUI();
        }
    }

    private void Update()
    {
        if (weaponData == null || firePoint == null) return;

        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime && currentAmmo > 0)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(
            weaponData.bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * weaponData.bulletSpeed;
        }

        currentAmmo--;
        UpdateAmmoUI();

        nextFireTime = Time.time + weaponData.fireRate;

        Destroy(bullet, 5f);
    }

    void SpawnWeaponModel()
    {
        if (weaponHolder == null || weaponData.weaponPrefab == null) return;

        currentWeaponObject = Instantiate(
            weaponData.weaponPrefab,
            weaponHolder.position,
            weaponHolder.rotation,
            weaponHolder
        );

        Transform newFirePoint = currentWeaponObject.transform.Find("FirePoint");

        if (newFirePoint != null)
        {
            firePoint = newFirePoint;
        }
        else
        {
            Debug.LogWarning("No FirePoint found on weapon!");
        }
    }

    public void EquipWeapon(WeaponData newWeapon)
    {
        weaponData = newWeapon;

        if (currentWeaponObject != null)
        {
            Destroy(currentWeaponObject);
        }

        SpawnWeaponModel();

        currentAmmo = weaponData.maxAmmo;
        UpdateAmmoUI();

        Debug.Log("Equipped: " + weaponData.weaponName);
    }

    public void RefillAmmo()
    {
        currentAmmo = weaponData.maxAmmo;
        UpdateAmmoUI();
    }


    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = currentAmmo + " / " + weaponData.maxAmmo;
        }
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public int GetMaxAmmo()
    {
        return weaponData != null ? weaponData.maxAmmo : 0;
    }
}