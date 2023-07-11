using UnityEngine;

public class OpenBox : MonoBehaviour
{
    private bool isOpen = false;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SwitchOpen()
    {
        isOpen = !isOpen;
        anim.SetBool("IsOpen", isOpen);
    }
}
