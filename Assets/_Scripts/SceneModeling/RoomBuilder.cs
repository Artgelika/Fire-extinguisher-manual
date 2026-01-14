using UnityEngine;

public class RoomBuilder : MonoBehaviour
{
    void Start()
    {
        CreateFloor();
        CreateWalls();
        CreateCeiling();
    }

    void CreateFloor()
    {
        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.name = "Floor";
        floor.transform.position = Vector3.zero;
        floor.transform.localScale = new Vector3(1, 1, 1);
        floor.transform.SetParent(this.transform, true);
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

    void CreateCeiling()
    {
        GameObject ceiling = GameObject.CreatePrimitive(PrimitiveType.Cube);
        ceiling.name = "Ceiling";
        ceiling.transform.position = new Vector3(0, 5f, 0);
        ceiling.transform.localScale = new Vector3(20f, 0.2f, 20f);
        ceiling.transform.SetParent(this.transform, true);
    }
}
