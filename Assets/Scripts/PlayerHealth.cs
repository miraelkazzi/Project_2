using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public Slider healthBar; 

    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;

    
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        Debug.Log("TakeDamage CALLED");

        if (isDead) return;

        currentHealth -= amount;

        Debug.Log("Player Health: " + currentHealth);

      
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        Debug.Log("PLAYER DIED");

     
        Enemy enemy = FindFirstObjectByType<Enemy>();

        if (enemy != null)
        {
            enemy.PlayVictory();
        }

     
        Invoke(nameof(PauseGame), 2f);
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("GAME PAUSED");
    }
}