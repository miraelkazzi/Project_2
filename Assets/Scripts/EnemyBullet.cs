using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 5f;

    private void Start()
    {
        Debug.Log("🟢 Enemy bullet SPAWNED at: " + transform.position);

        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        // 🔴 DRAW BULLET PATH
        Debug.DrawRay(transform.position, transform.forward * 1f, Color.green);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ignore enemy
        if (collision.gameObject.CompareTag("Enemy"))
            return;

        Debug.Log("💥 Bullet hit: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("🎯 PLAYER GOT HIT");

            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

            if (player != null)
            {
                player.TakeDamage(1);
            }
        }

        Destroy(gameObject);
    }
}