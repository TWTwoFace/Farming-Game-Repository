using UnityEngine;

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
            if(currentPrefab.GetComponent<GrowingUp>().Growed == true)
            {
                Destroy(currentPrefab.gameObject);
            }
        }
    }
}
