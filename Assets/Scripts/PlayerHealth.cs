using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public Slider healthBar;

    private bool isDead = false;
    public GameObject losePanel;


    private bool isShieldActive = false;
    public GameObject shieldIcon;
    public TextMeshProUGUI shieldTimerText;

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

        if (shieldIcon != null)
            shieldIcon.SetActive(true);

        if (shieldTimerText != null)
            shieldTimerText.gameObject.SetActive(true);

        float timeLeft = duration;

        while (timeLeft > 0)
        {
            if (shieldTimerText != null)
                shieldTimerText.text = timeLeft.ToString("0.0") + "s";

            timeLeft -= Time.deltaTime;
            yield return null;
        }

        if (shieldIcon != null)
            shieldIcon.SetActive(false);

        if (shieldTimerText != null)
            shieldTimerText.gameObject.SetActive(false);

        isShieldActive = false;
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

        if (losePanel != null)
            losePanel.SetActive(true);

        SoundManager.Instance.PlaySFX(SoundManager.Instance.loseSound);

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        var controller = FindFirstObjectByType<StarterAssets.FirstPersonController>();
        if (controller != null)
            controller.enabled = false;

        var gun = FindFirstObjectByType<GunShoot>();
        if (gun != null)
            gun.enabled = false;
    }
    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void PlayButtonSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.buttonClick);
    }
}