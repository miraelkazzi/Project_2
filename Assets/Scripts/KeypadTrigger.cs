using UnityEngine;
using NavKeypad;
using System.Collections;

public class KeypadTrigger : MonoBehaviour
{
    public GameObject keypadPrefab;
    public Transform playerCamera;

    private GameObject currentKeypad;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        SpawnKeypad();
    }

    void SpawnKeypad()
    {
        if (currentKeypad != null) return;

        Vector3 spawnPos = playerCamera.position + playerCamera.forward * 2f;

        currentKeypad = Instantiate(keypadPrefab, spawnPos, Quaternion.identity);

        // 🔥 CONNECT KEYPAD TO DOOR
        Keypad keypad = currentKeypad.GetComponent<Keypad>();
        SimpleDoor door = FindFirstObjectByType<SimpleDoor>();

        if (keypad != null && door != null)
        {
            keypad.OnAccessGranted.AddListener(() =>
            {
                door.OpenDoor();

                StartCoroutine(CloseAfterDelay());
            });
        }

        IEnumerator CloseAfterDelay()
        {
            yield return new WaitForSeconds(1.2f); // let keypad finish animation

            currentKeypad.SetActive(false);

            FreezePlayer(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        currentKeypad.transform.rotation =
            Quaternion.LookRotation(currentKeypad.transform.position - playerCamera.position);

        // 🔥 ADD LIGHT HERE
        Light light = currentKeypad.AddComponent<Light>();
        light.type = LightType.Directional;
        light.range = 2f;
        light.intensity = 1f;
        light.color = Color.white;

        FreezePlayer(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void FreezePlayer(bool freeze)
    {
        var controller = FindFirstObjectByType<StarterAssets.FirstPersonController>();
        if (controller != null)
        {
            controller.enabled = !freeze;
        }

        // 🔥 DISABLE SHOOTING
        var gun = FindFirstObjectByType<GunShoot>();
        if (gun != null)
        {
            gun.enabled = !freeze;
        }
    }
}
