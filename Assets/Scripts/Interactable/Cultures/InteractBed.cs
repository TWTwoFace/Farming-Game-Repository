using UnityEngine;

public class InteractBed : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject CulturePrefab;
    private GameObject currentPrefab;
    public void Interact()
    {
        if (currentPrefab == null)
        {
            PlaceCulture();
        }
        else
        {
            TakeCulture();
        }
    }
    private void PlaceCulture()
    {
            currentPrefab = Instantiate(CulturePrefab, transform.position, transform.rotation);
            currentPrefab.transform.SetParent(this.transform);
    }

    private void TakeCulture()
    {
            if(currentPrefab.TryGetComponent<GrowingUp>(out GrowingUp o))
            {
                if (o.Growed == true)
                {
                    Destroy(currentPrefab.gameObject);
                }
            }
    }
}
