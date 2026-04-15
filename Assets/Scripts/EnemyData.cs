using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "Enemies/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName = "Enemy";
    public int maxHealth = 3;
    public float moveSpeed = 3.5f;
    public float stopDistance = 2f;
}