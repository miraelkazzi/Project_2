using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet Data", menuName = "Weapons/Bullet Data")]
public class BulletData : ScriptableObject
{
    public int damage = 1;
    public float speed = 5f;
    public float lifetime = 5f;
}