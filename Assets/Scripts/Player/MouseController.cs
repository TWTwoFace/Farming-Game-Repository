using UnityEngine;
using System;

public class MouseController : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    [SerializeField] public Transform player;
    private float xRotation = 0f;

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
            float x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
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
}
