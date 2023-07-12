using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OpenBox : MonoBehaviour
{
    private bool IsOpen = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SwitchOpen()
    {
        IsOpen = !IsOpen;
        anim.SetBool(nameof(IsOpen), IsOpen);
    }
}
