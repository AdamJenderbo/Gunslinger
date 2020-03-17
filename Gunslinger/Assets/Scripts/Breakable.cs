using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public int maxPieces = 5;

    public bool shouldDropItem;
    public GameObject[] itemToDrop;
    public float itemDropPercent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            /*
            if (Player.instance.Dashing())
            {
                Break();
            }
            */
        }
        else if (other.tag == "PlayerBullet")
        {
            Break();
        }
    }

    private void Break()
    {
        Destroy(gameObject);

        /*

        //show broken pieces
        int pieceToDrop = Random.Range(1, maxPieces);

        for (int i = 0; i < pieceToDrop; i++)
        {
            int randomPiece = Random.Range(0, brokenPieces.Length);
            Instantiate(brokenPieces[randomPiece], transform.position, transform.rotation);
        }
        */

        // drop items
        if (shouldDropItem)
        {
            float dropChance = Random.Range(0f, 100f);

            if (dropChance < itemDropPercent)
            {
                int randomItem = Random.Range(0, itemToDrop.Length);
                Instantiate(itemToDrop[randomItem], transform.position, transform.rotation);
            }
        }
    }
}
