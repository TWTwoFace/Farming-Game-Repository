using UnityEngine;

public class TakeLetter : MonoBehaviour
{
    [SerializeField] private GameObject paper;

    public void OnTake()
    {
        paper.GetComponent<MeshRenderer>().enabled = false;
    }
}
