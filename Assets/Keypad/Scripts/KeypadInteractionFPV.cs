using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NavKeypad
{
    public class KeypadInteractionFPV : MonoBehaviour
    {
        private Camera cam;

        private void Awake()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            if (cam == null) return;

            var ray = cam.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("CLICK DETECTED");

                if (Physics.Raycast(ray, out var hit, 10f))
                {
                    Debug.Log("HIT: " + hit.collider.name);

                    if (hit.collider.TryGetComponent(out KeypadButton keypadButton))
                    {
                        Debug.Log("BUTTON SCRIPT FOUND: " + hit.collider.name);
                        keypadButton.PressButton();
                    }
                    else
                    {
                        Debug.Log("NO KeypadButton ON: " + hit.collider.name);
                    }
                }
                else
                {
                    Debug.Log("RAY HIT NOTHING");
                }
            }
        }
    }
}