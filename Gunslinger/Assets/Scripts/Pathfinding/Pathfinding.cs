using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding 
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    static Pathfinding instance;

    public static Pathfinding Instance 
    { 
        get
        {
            if (instance == null)
                instance = new Pathfinding();
            return instance;
        }
    }

    Grid<PathNode> grid;
    List<PathNode> openList;
    List<PathNode> closedList;

    Tilemap tilemap;

    public Pathfinding()
    {
    }

    public Pathfinding(Tilemap tilemap) : this(tilemap.size.x, tilemap.size.y, tilemap.CellToWorld(tilemap.origin), tilemap)
    {
       
    }

    public Pathfinding(int width, int height, Vector3 origin, Tilemap tilemap)
    {
        grid = new Grid<PathNode>(width, height, 0.1f, origin, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
        this.tilemap = tilemap;
        SetupUnwalkable();

        DrawGrid();
    }

    public void SetTilemap(Tilemap tilemap)
    {
        this.tilemap = tilemap;
        grid = new Grid<PathNode>(tilemap.size.x, tilemap.size.y, 1f, tilemap.CellToWorld(tilemap.origin), (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
        SetupUnwalkable();

    }

    public List<Vector3> FindPath(Vector3 start, Vector3 end)
    {
        grid.GetXY(start, out int startX, out int startY);
        grid.GetXY(end, out int endX, out int endY);
        List<PathNode> path = FindPath(startX, startY, endX, endY);
        if (path == null) return null;
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach(PathNode pathNode in path)
            {
                vectorPath.Add((new Vector3(pathNode.x, pathNode.y) * grid.cellSize) + grid.origin + new Vector3(grid.cellSize / 2, grid.cellSize / 2, 0));
            }
            return vectorPath;
        }
    }


    private List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetValue(startX, startY);
        PathNode endNode = grid.GetValue(endX, endY);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for(int x = 0; x < grid.Width; x++)
        {
            for(int y = 0; y < grid.Height; y++)
            {
                PathNode pathNode = grid.GetValue(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if(currentNode == endNode)
            {
                return CalculatePath(endNode);
            }
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(PathNode neighbour in GetNeighbours(currentNode))
            {
                if (closedList.Contains(neighbour)) continue;
                if(!neighbour.isWalkable)
                {
                    closedList.Add(neighbour);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbour);
                if(tentativeGCost < neighbour.gCost)
                {
                    neighbour.cameFromNode = currentNode;
                    neighbour.gCost = tentativeGCost;
                    neighbour.hCost = CalculateDistanceCost(neighbour, endNode);
                    neighbour.CalculateFCost();

                    if(!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                }
            }
        }

        return null;
    }

    private List<PathNode> GetNeighbours(PathNode currentNode)
    {
        List<PathNode> neighbours = new List<PathNode>();

        if(currentNode.x - 1 >= 0)
        {
            neighbours.Add(GetNode(currentNode.x - 1, currentNode.y));
            if (currentNode.y - 1 >= 0) neighbours.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
            if (currentNode.y + 1 < grid.Height) neighbours.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }
        if(currentNode.x + 1 < grid.Width)
        {
            neighbours.Add(GetNode(currentNode.x + 1, currentNode.y));
            if (currentNode.y - 1 >= 0) neighbours.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
            if (currentNode.y + 1 < grid.Height) neighbours.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }
        if (currentNode.y - 1 >= 0) neighbours.Add(GetNode(currentNode.x, currentNode.y - 1));
        if (currentNode.y + 1 < grid.Height) neighbours.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighbours;
    }

    private PathNode GetNode(int x, int y)
    {
        return grid.GetValue(x, y);
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while(currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }


    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodes)
    {
        PathNode lowestFCostNode = pathNodes[0];
        for(int i = 1; i < pathNodes.Count; i++)
        {
            if(pathNodes[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodes[i];
            }
        }
        return lowestFCostNode;
    }

    public void SetupUnwalkable()
    {
        for(int i = 0; i < grid.Width; i++)
        {
            for(int j = 0; j < grid.Height; j++)
            {
                Tile tile = tilemap.GetTile<Tile>(GridToCell(i,j));
                if (tile == null)
                    continue;
                if(tile.colliderType == Tile.ColliderType.Sprite)
                {
                    grid.GetValue(i, j).isWalkable = false;
                }
            }
        }
    }

    private Vector3Int GridToCell(int x, int y)
    {
        return tilemap.WorldToCell(grid.GetWorldPosition(x, y));
    }

    private void DrawGrid()
    {
        for(int x = 0; x < grid.Width; x++)
        {
            for(int y = 0; y < grid.Height; y++)
            {
                Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(grid.GetWorldPosition(x, y), grid.GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
    }
}
