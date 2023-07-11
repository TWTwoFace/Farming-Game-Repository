using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector3 velocity;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        moveDirection = transform.right * x + transform.forward * y;
        if (characterController.isGrounded == false)
        {
            velocity.y += Physics.gravity.y * speed * Time.deltaTime;
        }
        else
        {
            velocity.y = 0f;
        }
        characterController.Move(moveDirection * speed * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);
    }
}
