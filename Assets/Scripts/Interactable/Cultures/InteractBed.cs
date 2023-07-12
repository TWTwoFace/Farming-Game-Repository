using UnityEngine;
using UnityEngine.Rendering;

public class InteractBed : MonoBehaviour
{
    [SerializeField] private GameObject CulturePrefab;
    private GameObject currentPrefab;

    public void PlaceCulture()
    {
        if (currentPrefab == null)
        {
            currentPrefab = Instantiate(CulturePrefab, transform.position, transform.rotation);
            currentPrefab.transform.SetParent(this.transform);
        }
    }

    public void TakeCulture()
    {
        if (currentPrefab != null)
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
}
