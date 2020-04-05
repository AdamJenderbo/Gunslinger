using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public List<Sprite> sprites;

    [Header("Movement")]

    // movement

    public float moveSpeed;
    protected Rigidbody2D rigidBody;
    protected Vector2 moveDirection;
    private List<Vector3> path;
    private int currentPathIndex;


    public Bullet bullet;
    protected Gun gun;
    protected Vector3 target;
    private Transform aim;

    protected SpriteRenderer spriteRenderer;
    protected Animator animator;
    protected Camera cam;




    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //animator = GetComponent<Animator>();
        aim = transform.Find("Aim");
        gun = aim.Find("Right Hand").GetComponentInChildren<Gun>();
        gun.SetBullet(bullet);
        cam = Camera.main;
    }

    protected void LookAtTarget()
    {
        // rotate gun hand
        Vector2 offset = new Vector2(target.x - transform.position.x, target.y - transform.position.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;


  

        if (angle > 90 || angle < -90) { spriteRenderer.transform.localScale = Vector3.one; }
        else { spriteRenderer.transform.localScale = new Vector3(-1, 1, 1); }

        aim.rotation = Quaternion.Euler(0, 0, angle);

        // turn character



        // flip gun
        Vector3 localScale = Vector3.one;
        localScale.y = (angle > 90 || angle < -90) ? -1f : 1f;
        aim.localScale = localScale;
    }

    protected void SetTarget(Vector3 target)
    {
        currentPathIndex = 0;
        path = Pathfinding.Instance.FindPath(transform.position, target);
        if(path != null && path.Count > 1)
        {
            path.RemoveAt(0);
        }
        this.target = target;
    }


    protected void FollowTarget()
    {
        if(path != null)
        {
            Vector3 targetPosition = path[currentPathIndex];
            if(Vector3.Distance(transform.position, targetPosition) > .1f)
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                transform.position = transform.position + moveDir * moveSpeed * Time.deltaTime;
            }
            else
            {
                currentPathIndex++;
                if(currentPathIndex >= path.Count)
                {
                    Stop();
                }
            }
        }
    }

    protected void Move()
    {
        rigidBody.velocity = moveDirection * moveSpeed;
    }

    protected void Stop()
    {
        rigidBody.velocity = Vector2.zero;
    }
}
