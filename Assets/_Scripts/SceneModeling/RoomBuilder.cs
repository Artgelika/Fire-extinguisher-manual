using UnityEngine;

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
        floor.transform.localScale = new Vector3(1, 1, 1);
        floor.transform.SetParent(this.transform, true);

        // Remove any existing collider (Plane primitives often come with a MeshCollider)
        Collider existingCollider = floor.GetComponent<Collider>();
        if (existingCollider != null)
        {
            Destroy(existingCollider);
        }
        GenerateBoxCollider(floor);
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
        float roomWidth = 10f;
        float roomHeight = 5f;
        float wallThickness = 0.2f;
        float halfWidth = roomWidth / 2f;
        float offset = halfWidth - (wallThickness / 2f); // place wall center so inner face aligns with floor edge

        // Back Wall (negative Z)
        CreateWall(
            "Wall_Back",
            new Vector3(0f, roomHeight / 2f, -offset),
            new Vector3(roomWidth, roomHeight, wallThickness)
        );

        // Front Wall (positive Z)
        CreateWall(
            "Wall_Front",
            new Vector3(0f, roomHeight / 2f, offset),
            new Vector3(roomWidth, roomHeight, wallThickness)
        );

        // Left Wall (negative X)
        CreateWall(
            "Wall_Left",
            new Vector3(-offset, roomHeight / 2f, 0f),
            new Vector3(wallThickness, roomHeight, roomWidth)
        );

        // Right Wall (positive X)
        CreateWall(
            "Wall_Right",
            new Vector3(offset, roomHeight / 2f, 0f),
            new Vector3(wallThickness, roomHeight, roomWidth)
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
        tableTop.transform.position = new Vector3(0, 1f, 1.3f);
        tableTop.transform.localScale = new Vector3(3f, 0.1f, 1f);
        tableTop.GetComponent<MeshRenderer>().material.color = new Color(0.55f, 0.27f, 0.07f); // Brown color
        tableTop.transform.SetParent(transform, true);
        float legHeight = 1f;
        float legThickness = 0.05f;
        Vector3[] legPositions = new Vector3[]
        {
            new(-1.4f, legHeight / 2f, 0.9f),
            new(1.4f, legHeight / 2f, 0.9f),
            new(-1.4f, legHeight / 2f, 1.7f),
            new(1.4f, legHeight / 2f, 1.7f),
        };
        foreach (Vector3 pos in legPositions)
        {
            GameObject leg = GameObject.CreatePrimitive(PrimitiveType.Cube);
            leg.name = "Table_Leg";
            leg.transform.position = pos;
            leg.transform.localScale = new Vector3(legThickness, legHeight, legThickness);
            leg.GetComponent<MeshRenderer>().material.color = new Color(0.55f, 0.27f, 0.07f); // Brown color
            leg.transform.SetParent(transform, true);
        }
    }

    void CreateCeiling()
    {
        GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ceiling.name = "Ceiling";
        ceiling.transform.position = new Vector3(0, 5f, 0);
        ceiling.transform.localScale = new Vector3(20f, 0.2f, 20f);
        ceiling.transform.SetParent(this.transform, true);
    }
}
