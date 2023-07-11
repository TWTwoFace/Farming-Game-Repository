using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private float rayDistance = 2f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out var hit, rayDistance, mask))
            {
                GameObject obj = hit.collider.gameObject;
                var o = obj.GetComponent<Interactable>();
                if (o != null)
                {
                    o.Interact();
                }
            }
        }
    }
}
