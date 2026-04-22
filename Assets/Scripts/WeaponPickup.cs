using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public WeaponData weaponData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GunShoot gun = other.GetComponentInChildren<GunShoot>();

            if (gun != null)
            {
                gun.EquipWeapon(weaponData);
            }

            Destroy(gameObject);
        }
    }
    void Update()
    {
        transform.Rotate(0f, 60f * Time.deltaTime, 0f);
    }
}