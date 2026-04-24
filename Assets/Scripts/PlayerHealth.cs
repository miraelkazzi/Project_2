using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public Slider healthBar;

    private bool isDead = false;


    private bool isShieldActive = false;

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


        if (isShieldActive)
        {
            Debug.Log("Shield absorbed damage");
            return;
        }

        currentHealth -= amount;

        Debug.Log("Player Health: " + currentHealth);

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthUI();
    }


    public void ActivateShield(float duration)
    {
        StartCoroutine(ShieldRoutine(duration));
    }

    private IEnumerator ShieldRoutine(float duration)
    {
        isShieldActive = true;
        Debug.Log("Shield ON");

        yield return new WaitForSeconds(duration);

        isShieldActive = false;
        Debug.Log("Shield OFF");
    }


    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
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