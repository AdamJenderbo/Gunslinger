using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 7.5f;
    public Rigidbody2D bulletRigidbody;

    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletRigidbody.velocity = transform.right * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
