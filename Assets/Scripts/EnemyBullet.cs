using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public BulletData bulletData;

    private void Start()
    {
        Debug.Log("🟢 Enemy bullet SPAWNED at: " + transform.position);

        Destroy(gameObject, bulletData.lifetime);
    }

    private void Update()
    {
        transform.position += transform.forward * bulletData.speed * Time.deltaTime;

        Debug.DrawRay(transform.position, transform.forward * 1f, Color.green);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            return;

        Debug.Log("💥 Coin hit: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("🎯 PLAYER GOT HIT");

            PlayerHealth player = other.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.TakeDamage(3); // stronger than bullet
            }
        }

        Destroy(gameObject);
    }
}