using UnityEngine;
using UnityEngine.Events;

public class StartPlace : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent place;
    public void Interact()
    {
        place.Invoke();
    }
}
