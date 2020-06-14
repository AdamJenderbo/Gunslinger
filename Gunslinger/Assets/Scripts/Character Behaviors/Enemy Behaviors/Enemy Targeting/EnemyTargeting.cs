using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour, IEnemyTargeting
{
    public float fov;
    public float viewDistance;

    Enemy enemy;
    Player player;
    bool seesPlayer;

    public float GetFOV()
    {
        return fov;
    }

    public float GetViewDistance()
    {
        return viewDistance;
    }

    public bool SeesPlayer()
    {
        return seesPlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        player = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        seesPlayer = false;
        if (Vector3.Distance(transform.position, player.transform.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
            if (Vector3.Angle(enemy.GetAimDir(), dirToPlayer) < fov / 2f)
            {
                RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, dirToPlayer, viewDistance, 8448);
                if (raycastHit2D.collider != null)
                {
                    if (raycastHit2D.collider.gameObject.tag == "Player")
                    {
                        seesPlayer = true;
                    }
                }
            }
        }
    }
}
