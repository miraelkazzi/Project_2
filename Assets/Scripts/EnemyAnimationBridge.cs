using UnityEngine;

public class EnemyAnimationBridge : MonoBehaviour
{
    public Enemy enemy;

    public void ShootBullet()
    {
        enemy.ShootBullet();
    }
}