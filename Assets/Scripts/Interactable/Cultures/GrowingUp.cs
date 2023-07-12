using System.Collections;
using UnityEngine;

public class GrowingUp : MonoBehaviour
{
    [SerializeField] private string[] tags;
    [SerializeField] private float[] stagesTimes;

    public bool Growed;
    public int stage;

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

        stage = 0;
        Wait(stagesTimes[0], stage, tags, arr);
        yield return new WaitForSeconds(stagesTimes[0]);

        stage = 1;
        Wait(stagesTimes[1], stage, tags, arr);
        yield return new WaitForSeconds(stagesTimes[1]);

        stage = 2;
        Wait(stagesTimes[2], stage, tags, arr);
        Growed = true;
        yield break;

    }
}
