using System.Collections;
using UnityEngine;

public class GrowingUp : MonoBehaviour
{
    [SerializeField] private string[] tags;
    [SerializeField] private float[] stagesTimes;

    public bool Growed;

    private void Start()
    {
        Growed = false;
        StartCoroutine(Cout());
    }

    private void Wait(float timer, int stage, string[] tags, Component[] arr)
    {
        foreach (Component comp in arr)
        {
            if (comp.tag != tags[stage])
            {
                MeshRenderer mr = comp.GetComponent<MeshRenderer>();
                mr.enabled = false;
            }
            else
            {
                MeshRenderer mr = comp.GetComponent<MeshRenderer>();
                mr.enabled = true;
            }
        }
    }

    private IEnumerator Cout()
    {
        Component[] arr = GetComponentsInChildren(typeof(MeshRenderer));
        Wait(stagesTimes[0], 0, tags, arr);
        yield return new WaitForSeconds(stagesTimes[0]);
        Debug.Log("Stage1");
        Wait(stagesTimes[1], 1, tags, arr);
        yield return new WaitForSeconds(stagesTimes[1]);
        Debug.Log("Stage2");
        Wait(stagesTimes[2], 2, tags, arr);
        Growed = true;
        yield break;

    }
}
