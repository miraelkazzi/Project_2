using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("🔥 TakeDamage CALLED");

        if (isDead) return;

        currentHealth -= amount;

        Debug.Log("💔 Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

  
    void Die()
    {
        isDead = true;

        Debug.Log("💀 PLAYER DIED");

        // 🔥 Tell enemy to play victory animation
        Enemy enemy = FindFirstObjectByType<Enemy>();

        if (enemy != null)
        {
            enemy.PlayVictory();
        }

        // ⏳ Pause game after delay (so animation can play)
        Invoke(nameof(PauseGame), 2f);
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("⏸️ GAME PAUSED");
    }
}