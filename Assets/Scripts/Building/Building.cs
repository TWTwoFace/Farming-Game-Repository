using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Building : MonoBehaviour
{
    [SerializeField] public Vector2Int Size = new Vector2Int();
    public Renderer MainRenderer;
    
    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                Gizmos.color = new Color(0, 255, 255, 0.7f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0f, y), new Vector3(1, 0.1f, 1));
            }
        }
    }

    public void SetDefault()
    {
        GetComponentInChildren<MeshRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        MainRenderer.material.color = Color.white;
    }

}
