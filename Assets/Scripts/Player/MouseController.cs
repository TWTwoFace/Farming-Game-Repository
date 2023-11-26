using UnityEngine;
using System;
using UnityEngine.InputSystem;
public class MouseController : MonoBehaviour
{
    
    [SerializeField, Range(0f, 200f)] private float mouseSensitivity = 1f;
    [SerializeField, Range(0f, 400f)] private float gamepadSensitivity = 1f;
    [SerializeField] private Transform player;
    private float xRotation = 0f;
    private Vector2 deltaLook;
    private bool isGamepad;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void ToggleCursorMode()
    {
        Cursor.visible = !Cursor.visible;
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void Update()
    {
        if (Cursor.visible == false)
        {
            float x = deltaLook.x * Time.deltaTime;
            float y = deltaLook.y * Time.deltaTime;
            if (isGamepad)
            {
                x = x * gamepadSensitivity;
                y = y * gamepadSensitivity;
            }
            else
            {
                x = x * mouseSensitivity;
                y = y * mouseSensitivity;
            }
            xRotation -= y;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            player.Rotate(Vector3.up * (float)Math.Round(x, 2));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleCursorMode();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        deltaLook = context.ReadValue<Vector2>();
        if(context.control.device is Gamepad)
        {
            isGamepad = true;
        }
        else
        {
            isGamepad = false;
        }
    }
}
