using UnityEngine;
using NavKeypad;
using System.Collections;
using TMPro;

public class KeypadTrigger : MonoBehaviour
{
    public GameObject keypadPrefab;
    public Transform playerCamera;
    public GameObject crosshair;

    public EnemySpawner spawner;
    public TextMeshProUGUI messageText;

    public GameObject doorBlocker;

    private GameObject currentKeypad;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (spawner != null && (spawner.GetCurrentWave() < 2 || !spawner.IsWaveCleared()))
        {
            StartCoroutine(ShowMessage());
            return;
        }

        SpawnKeypad();
    }

    void SpawnKeypad()
    {
        if (currentKeypad != null) return;

        Vector3 spawnPos = playerCamera.position + playerCamera.forward * 2f;

        currentKeypad = Instantiate(keypadPrefab, spawnPos, Quaternion.identity);

        currentKeypad.transform.rotation =
            Quaternion.LookRotation(currentKeypad.transform.position - playerCamera.position);

        if (crosshair != null)
            crosshair.SetActive(false);

        Keypad keypad = currentKeypad.GetComponent<Keypad>();
        SimpleDoor door = FindFirstObjectByType<SimpleDoor>();

        if (keypad != null && door != null)
        {
            keypad.OnAccessGranted.AddListener(() =>
            {
                door.OpenDoor();

                if (doorBlocker != null)
                    doorBlocker.SetActive(false);

                StartCoroutine(CloseAfterDelay());
            });
        }

        Light light = currentKeypad.AddComponent<Light>();
        light.type = LightType.Directional;
        light.range = 2f;
        light.intensity = 1f;
        light.color = Color.white;

        FreezePlayer(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(1.2f);

        if (currentKeypad != null)
            currentKeypad.SetActive(false);

        FreezePlayer(false);

        if (crosshair != null)
            crosshair.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator ShowMessage()
    {
        if (messageText == null) yield break;

        messageText.gameObject.SetActive(true);

        if (spawner != null && spawner.GetCurrentWave() < 2)
            messageText.text = "Finish previous waves";
        else
            messageText.text = "Kill all enemies";

        Color color = messageText.color;
        float duration = 0.3f;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(0, 1, t / duration);
            messageText.color = color;
            yield return null;
        }

        color.a = 1;
        messageText.color = color;

        yield return new WaitForSeconds(1.2f);

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(1, 0, t / duration);
            messageText.color = color;
            yield return null;
        }

        color.a = 0;
        messageText.color = color;

        messageText.gameObject.SetActive(false);
    }

    void FreezePlayer(bool freeze)
    {
        var controller = FindFirstObjectByType<StarterAssets.FirstPersonController>();
        if (controller != null)
        {
            controller.enabled = !freeze;
        }

        var gun = FindFirstObjectByType<GunShoot>();
        if (gun != null)
        {
            gun.enabled = !freeze;
        }
    }
}