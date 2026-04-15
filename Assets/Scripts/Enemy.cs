using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public Animator animator;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private int currentHealth;
    private Transform player;
    private NavMeshAgent agent;

    private bool isDead = false;
    private float nextAttackTime = 0f;

    private void Start()
    {
        if (enemyData != null)
        {
            currentHealth = enemyData.maxHealth;
        }
        else
        {
            Debug.LogWarning("EnemyData is missing on " + gameObject.name);
        }

        agent = GetComponent<NavMeshAgent>();

        if (agent != null && enemyData != null)
        {
            agent.speed = enemyData.moveSpeed;
            agent.stoppingDistance = enemyData.stopDistance;
        }

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("No object with Player tag found.");
        }
    }

    private void Update()
    {
        if (isDead) return;
        if (player == null || agent == null || enemyData == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // 🟢 MOVEMENT
        if (distance > enemyData.stopDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);

            if (animator != null)
            {
                animator.SetBool("IsMoving", true);
            }
        }
        else
        {
            agent.isStopped = true;

            if (animator != null)
            {
                animator.SetBool("IsMoving", false);
            }

            // 🔴 ATTACK
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 3f;
            }
        }
    }

    // 🎬 ONLY triggers animation
    void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    // 🔫 CALLED FROM ANIMATION EVENT
    public void ShootBullet()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Debug.Log("🔥 SHOOTING BULLET");

            Instantiate(
                bulletPrefab,
                firePoint.position,
                firePoint.rotation
            );
        }
        else
        {
            Debug.LogWarning("❌ bulletPrefab or firePoint missing");
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;

        if (animator != null)
        {
            animator.SetTrigger("GetHit");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        isDead = true;

        if (agent != null)
        {
            agent.isStopped = true;
        }

        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        StartCoroutine(DieAfterDelay());
    }

    IEnumerator DieAfterDelay()
    {
        yield return new WaitForSeconds(20f);
        Destroy(gameObject);
    }

    // 🏆 CALLED WHEN PLAYER DIES
    public void PlayVictory()
    {
        if (isDead) return;

        Debug.Log("🏆 ENEMY VICTORY");

        // stop movement
        if (agent != null)
        {
            agent.isStopped = true;
        }

        // stop all behavior
        this.enabled = false;

        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Victory");
        }
    }
}