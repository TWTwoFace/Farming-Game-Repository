using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private CharacterController characterController;

    private Vector3 velocity;
    private Vector2 moveDirection = new Vector2(0, 0);
    
    private void Update()
    {
        if (characterController.isGrounded == false)
        {
            velocity.y += Physics.gravity.y * speed * Time.deltaTime;
        }
        else
        {
            velocity.y = 0f;
        }
        Vector3 direction = transform.right * moveDirection.x + transform.forward * moveDirection.y;
        characterController.Move(new Vector3(direction.x, 0, direction.z) * speed * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }
}
