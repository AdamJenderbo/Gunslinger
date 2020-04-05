using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingTest : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;

    public Tilemap tilemap;

    Pathfinding pathfinding;



    // Start is called before the first frame update
    void Start()
    {
        pathfinding = new Pathfinding(tilemap);
        List<Vector3> path = pathfinding.FindPath(new Vector3(0, 0, 0), new Vector3(2, 2, 0));
        foreach(Vector3 vec in path)
        {
            Debug.Log(vec.ToString());
        }
    }
}
