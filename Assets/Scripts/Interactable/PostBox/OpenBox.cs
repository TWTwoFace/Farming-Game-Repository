using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OpenBox : MonoBehaviour, IInteractable
{
    private bool IsOpen = false;
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Interact()
    {
        if (!IsOpen)
        {
            Open();
        }
        else
        {
            Close();
        }
        anim.SetBool(nameof(IsOpen), IsOpen);
    }
    private void Open()
    {
        IsOpen = true;
    }
    private void Close()
    {
        IsOpen = false;
    }
}