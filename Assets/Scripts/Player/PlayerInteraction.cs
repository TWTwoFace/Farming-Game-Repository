using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float rayDistance = 2f;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hit, rayDistance, mask))
            {
                GameObject obj = hit.collider.gameObject;
                if (obj.TryGetComponent<IInteractable>(out var o))
                {
                    o.Interact();
                }
            }
        }
    }
}
