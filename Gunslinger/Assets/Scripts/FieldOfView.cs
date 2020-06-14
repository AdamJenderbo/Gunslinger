using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public Color color;

    [SerializeField] private LayerMask layerMask;
    Mesh mesh;
    Vector3 origin;
    float angle;
    float fov;
    float viewDistance;
    MeshRenderer meshRenderer;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "Ground";
        meshRenderer.sortingOrder = 1;
        SetColor(new Color(color.r, color.g, color.b));
    }

    private void LateUpdate()
    {
        int rayCount = 50;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            //Vector3 vertex = origin + GetVectorFromAngle(angle) * viewDistance;

            Vector3 vertex;

            RaycastHit2D rayCastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);;
            if (rayCastHit2D.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = rayCastHit2D.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
    }



    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimAngle(float angle)
    {
        this.angle = angle + (fov / 2f);
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        this.angle = GetAngleFromVectorFloat(aimDirection) - fov / 2f;
    }
    public void SetFov(float fov)
    {
        this.fov = fov;
    }

    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }

    public void SetColor(Color color)
    {
        if (meshRenderer == null)
            return;
        meshRenderer.material.color = new Color(color.r, color.g, color.b, 0.5f);
    }

    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        return n;
    }


}
