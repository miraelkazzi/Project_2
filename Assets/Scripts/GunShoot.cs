using UnityEngine;

public class GunShoot : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform firePoint;

    private float nextFireTime;
    private int currentAmmo;

    private void Start()
    {
        if (weaponData != null)
        {
            currentAmmo = weaponData.maxAmmo;
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
        nextFireTime = Time.time + weaponData.fireRate;

        Destroy(bullet, 5f);
    }

    public void RefillAmmo()
    {
        currentAmmo = weaponData.maxAmmo;
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