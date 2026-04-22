using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public WeaponData currentWeapon;

    public void EquipWeapon(WeaponData newWeapon)
    {
        currentWeapon = newWeapon;

        Debug.Log("Equipped: " + newWeapon.weaponName);

        // Update ammo / UI here later if needed
    }
}