using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;

    public int damage;

    //public GameObject impactEffect;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);

        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().Damage(damage);
        }
        else if (other.tag == "Player")
        {
            Player.instance.Damage(damage);
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
