using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public Animator animator;

    public GameObject bulletPrefab;

    public Transform firePoint;
    public Transform[] firePoints;

    public EnemySpawner spawner;

    private int currentHealth;
    private Transform player;
    private NavMeshAgent agent;

    private bool isDead = false;
    private float nextAttackTime = 0f;
    public float attackDistance = 10f;

    public GameObject coinPrefab;

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

            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 3f;
            }
        }

        if (distance <= enemyData.attackDistance)
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 3f;
            }
        }
    }

    void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void ShootBullet()
    {
        if (bulletPrefab == null || player == null) return;

        // 🔥 MULTI FIRE (virus enemy)
        if (firePoints != null && firePoints.Length > 0)
        {
            for (int i = 0; i < firePoints.Length; i++)
            {
                Transform fp = firePoints[i];
                if (fp == null) continue;

                Vector3 direction = (player.position - fp.position).normalized;

                Instantiate(
                    bulletPrefab,
                    fp.position,
                    Quaternion.LookRotation(direction)
                );
            }
        }
       
        else if (firePoint != null)
        {
            Instantiate(
                bulletPrefab,
                firePoint.position,
                firePoint.rotation
            );
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
        yield return new WaitForSeconds(2f);

        if (coinPrefab != null)
        {
            Instantiate(
                coinPrefab,
                transform.position + Vector3.up * 1f,
                Quaternion.identity
            );
        }

        if (spawner != null)
        {
            spawner.EnemyDied();
        }

        Destroy(gameObject);
    }

    public void PlayVictory()
    {
        if (isDead) return;

        if (agent != null)
        {
            agent.isStopped = true;
        }

        this.enabled = false;

        if (animator != null)
        {
            animator.SetBool("IsMoving", false);
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Victory");
        }
    }
}