using TMPro;
using UnityEngine;

public class InteractionHint : MonoBehaviour
{
    [SerializeField] private TMP_Text hint;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask mask;

    private Color alphaWhite = Color.white;

    private bool textEnabled;

    private Outline lastHit;
    private Outline currentObj;

    private void Start()
    {
        alphaWhite.a = 0f;
        hint.color = alphaWhite;
    }

    private void EnableOutline(Outline obj) 
    {

        if (obj != null)
        {
            obj.enabled = true;
        }
    }

    private void DisableOutline(Outline obj)
    {
        if (obj != null)
        {
            obj.enabled = false;
        }
    }

    private void EnableHint()
    {
        hint.color = Color.white;
        if (lastHit != null)
        {
            lastHit.enabled = true;
        }
        textEnabled = true;
    }

    private void DisableHint()
    {
        hint.color = alphaWhite;
        if (lastHit != null)
        {
            lastHit.enabled = false;
        }
        textEnabled = false;
    }

    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out var hit, rayDistance, mask))
        {
            currentObj = hit.collider.gameObject.GetComponentInChildren<Outline>();
            if (currentObj != null)
            {
                EnableOutline(currentObj);
                if (lastHit == null)
                {
                    lastHit = currentObj;
                }
            }
            
            if (textEnabled == false)
            {
                lastHit = hit.collider.gameObject.GetComponentInChildren<Outline>();
                EnableHint();
            }
        }
        else
        {
            if (textEnabled == true)
            {
                DisableHint();
            }
        }
        if (lastHit != currentObj)
        {
            DisableOutline(lastHit);
        }
        lastHit = currentObj;
    }
}
