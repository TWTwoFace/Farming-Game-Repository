using UnityEngine;

public class BulidingsGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);
    public Building[,] grid;

    private Building flyingBuilding;
    private Camera mainCamera;
    private Color green;

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < GridSize.x; x++)
        {
            for (int y = 0; y < GridSize.y; y++)
            {
                if (x % 2 == 0 && y % 2 == 0)
                {
                    Gizmos.color = new Color(0, 0, 255, 0.3f);
                }
                else
                {
                    Gizmos.color = new Color(255, 0, 0, 0.3f);
                }
                Gizmos.DrawCube(transform.position + new Vector3(x, 0f, y), new Vector3(1, 0.1f, 1));
            }
        }
    }

    private void Awake()
    {
        grid = new Building[GridSize.x, GridSize.y];
        mainCamera = Camera.main;
        green = Color.green;
        green.a = .3f;
    }

    public void StartPlacingBuilding(Building BuildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }
        flyingBuilding = Instantiate(BuildingPrefab);
        flyingBuilding.GetComponent<BoxCollider>().enabled = false;
        flyingBuilding.GetComponentInChildren<MeshRenderer>().enabled = false;
    }
    
    private void Update()
    {
        if (flyingBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (groundPlane.Raycast(ray, out float position))
            {
                if (position <= 2f)
                {
                    flyingBuilding.GetComponentInChildren<MeshRenderer>().enabled = true;
                    flyingBuilding.MainRenderer.material.color = green;
                    Vector3 worldPos = ray.GetPoint(position);
                    int x = Mathf.RoundToInt(worldPos.x);
                    int y = Mathf.RoundToInt(worldPos.z);
                    flyingBuilding.transform.position = new Vector3(x, 0f, y);
                    bool available = true;

                    if (x < 0 || x > GridSize.x - flyingBuilding.Size.x) available = false;
                    if (y < 0 || y > GridSize.y - flyingBuilding.Size.y) available = false;
                    if (available && isPlaceTaken(x, y)) available = false;

                    if (Input.GetMouseButton(0) && available)
                    {
                        PlaceFlyingBuilding(x, y);
                    }
                    if (Input.GetMouseButton(1))
                    {
                        Destroy(flyingBuilding.gameObject);
                    }
                }
                else
                {
                    flyingBuilding.GetComponentInChildren<MeshRenderer>().enabled = false;
                }
            }
        }
    }

    private bool isPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                if(grid[placeX + x, placeY + y] != null) return true;
            }
        }
        return false;
    }

    private void PlaceFlyingBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.Size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.Size.y; y++)
            {
                grid[placeX + x, placeY + y] = flyingBuilding;
            }
        }
        flyingBuilding.SetDefault();
        flyingBuilding = null;
    }
}
