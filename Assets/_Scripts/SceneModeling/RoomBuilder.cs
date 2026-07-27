using UnityEngine;

namespace SceneModeling
{
    public class RoomBuilder : MonoBehaviour
    {
    void Start()
    {
        CreateFloor();
        CreateWalls();
        CreateTable();
        CreateCeiling();
    }

    void CreateFloor()
    {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.name = "Floor";
        floor.transform.position = Vector3.zero;
        floor.transform.localScale = new Vector3(Constants.LocalScale, Constants.LocalScale, Constants.LocalScale);
        floor.transform.SetParent(this.transform, true);

        // Remove any existing collider (Plane primitives often come with a MeshCollider)
        if (floor.TryGetComponent<Collider>(out var existingCollider))
        {
            Destroy(existingCollider);
        }
        GenerateBoxCollider(floor);
    }

    void CreateCeiling()
    {
        GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ceiling.name = "Ceiling";
        ceiling.transform.position = new Vector3(0, 5f, 0);
        ceiling.transform.localScale = new Vector3(Constants.LocalScale, Constants.LocalScale, Constants.LocalScale); 
        ceiling.transform.SetParent(this.transform, true);
    }

    BoxCollider GenerateBoxCollider(GameObject gameObject)
    {
        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        Vector3 finalScale = transform.lossyScale;
        float planeWidth = 10f * finalScale.x;
        float planeDepth = 10f * finalScale.z;
        float thickness = 0.1f;
        box.size = new Vector3(planeWidth, thickness, planeDepth);
        box.center = new Vector3(0f, -thickness / 2f, 0f);
        return box;
    }

    void CreateWalls()
    {
        // Back Wall (negative Z)
        CreateWall(
            "Wall_Back",
            new Vector3(0f, Constants.RoomHeight / 2f, -Constants.Offset),
            new Vector3(Constants.RoomWidth, Constants.RoomHeight, Constants.WallThickness)
        );

        // Front Wall (positive Z)
        CreateWall(
            "Wall_Front",
            new Vector3(0f, Constants.RoomHeight / 2f, Constants.Offset),
            new Vector3(Constants.RoomWidth, Constants.RoomHeight, Constants.WallThickness)
        );

        // Left Wall (negative X)
        CreateWall(
            "Wall_Left",
            new Vector3(-Constants.Offset, Constants.RoomHeight / 2f, 0f),
            new Vector3(Constants.WallThickness, Constants.RoomHeight, Constants.RoomWidth)
        );

        // Right Wall (positive X)
        CreateWall(
            "Wall_Right",
            new Vector3(Constants.Offset, Constants.RoomHeight / 2f, 0f),
            new Vector3(Constants.WallThickness, Constants.RoomHeight, Constants.RoomWidth)
        );
    }

    void CreateWall(string name, Vector3 position, Vector3 scale)
    {
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = name;
        wall.transform.SetParent(this.transform, true);
        wall.transform.position = position;
        wall.transform.localScale = scale;
    }

    void CreateTable()
    {
        GameObject tableTop = GameObject.CreatePrimitive(PrimitiveType.Cube);
        tableTop.name = "Table_Top";
        tableTop.transform.position = new Vector3(0, Constants.TableHeight+Constants.TableTopThickness/2, 1.3f);
        tableTop.transform.localScale = new Vector3(3f, Constants.TableTopThickness, 1f);
        tableTop.GetComponent<MeshRenderer>().material.color = new Color(0.55f, 0.27f, 0.07f); // Brown color
        tableTop.transform.SetParent(transform, true);
        Vector3[] legPositions = new Vector3[]
        {
            new(-1.4f, Constants.LegHeight / 2f, 0.9f),
            new(1.4f, Constants.LegHeight / 2f, 0.9f),
            new(-1.4f, Constants.LegHeight / 2f, 1.7f),
            new(1.4f, Constants.LegHeight / 2f, 1.7f),
        };
        foreach (Vector3 pos in legPositions)
        {
            GameObject leg = GameObject.CreatePrimitive(PrimitiveType.Cube);
            leg.name = "Table_Leg";
            leg.transform.position = pos;
            leg.transform.localScale = new Vector3(Constants.LegThickness, Constants.LegHeight, Constants.LegThickness);
            leg.GetComponent<MeshRenderer>().material.color = new Color(0.55f, 0.27f, 0.07f); // Brown color
            leg.transform.SetParent(transform, true);
        }
    }
    }
}