using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Identity")]
    public string weaponName = "New Weapon";

    [Header("Combat")]
    public float damage = 10f;
    public float fireRate = 0.2f;
    public float bulletSpeed = 30f;
    public int maxAmmo = 30;

    [Header("Bullet")]
    public GameObject bulletPrefab;
}