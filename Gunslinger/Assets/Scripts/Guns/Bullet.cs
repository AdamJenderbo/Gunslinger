using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;

    public GameObject impactEffect;

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
        Instantiate(impactEffect, transform.position, transform.rotation);

        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.Damage(damage);
            enemy.Flash();
        }
        else if (other.tag == "Player")
        {
            Player.instance.Damage(damage);
        }

        Destroy(gameObject);

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
