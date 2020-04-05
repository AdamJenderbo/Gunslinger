using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    public int width;
    public int height;

    public GameObject prefab;
    SpriteRenderer[,] sprites;

    int startX, startY, endX, endY;

    private void Start()
    {
        sprites = new SpriteRenderer[width, height];
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                GameObject tile = Instantiate(prefab, transform);
                sprites[i,j] = tile.GetComponent<SpriteRenderer>();
                tile.transform.position = new Vector3(i + 0.5f, j + 0.5f);
            }
        }
        prefab.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (Input.GetMouseButtonDown(0))
            {
                sprites[startX, startY].color = new Color(255, 255, 255);

                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                Debug.Log(hit.point);
                Debug.Log(hit.point.ToString());
                startX = Mathf.FloorToInt(hit.point.x);
                startY = Mathf.FloorToInt(hit.point.y);

                sprites[startX, startY].color = new Color(255, 0, 0);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                sprites[endX, endY].color = new Color(255, 255, 255);

                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                Debug.Log(hit.point);
                Debug.Log(hit.point.ToString());
                endX = Mathf.FloorToInt(hit.point.x);
                endY = Mathf.FloorToInt(hit.point.y);

                sprites[endX, endY].color = new Color(0, 0, 255);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                sprites[startX, startY].color = new Color(255, 255, 255);

                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                Debug.Log(hit.point);
                Debug.Log(hit.point.ToString());
                startX = Mathf.FloorToInt(hit.point.x);
                startY = Mathf.FloorToInt(hit.point.y);

                sprites[startX, startY].color = new Color(255, 0, 0);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                sprites[endX, endY].color = new Color(255, 255, 255);

                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
                Debug.Log(hit.point);
                Debug.Log(hit.point.ToString());
                endX = Mathf.FloorToInt(hit.point.x);
                endY = Mathf.FloorToInt(hit.point.y);

                sprites[endX, endY].color = new Color(0, 0, 255);
            }
        }

    }
}
