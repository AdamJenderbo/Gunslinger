using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // stats
    public int health;
    public float attackSpeed;


    public Player player;

    // state
    public State startState;
    private State state;


    // modules
    IEnemyTargeting enemyTargeting;
    [HideInInspector]
    public Shooting shooting;

    // field of view
    //[SerializeField] private Transform pfFieldOfView;
    //[HideInInspector]
    //FieldOfView fieldOfView;


    private void Awake()
    {
        enemyTargeting = GetComponent<IEnemyTargeting>();
        shooting = GetComponent<Shooting>();
    }

    protected override void Start()
    {
        base.Start();
        player = Player.instance;
        //fieldOfView = Instantiate(pfFieldOfView, null).GetComponent<FieldOfView>();
        //fieldOfView.SetFov(enemyTargeting.GetFOV());
        //fieldOfView.SetViewDistance(enemyTargeting.GetViewDistance() / 2);
        state = startState;
        state.Begin();


    }

    protected override void Update()
    {
        base.Update();
        //fieldOfView.SetOrigin(transform.position);
        //fieldOfView.SetAimAngle(GetAimAngle());
        //fieldOfView.SetFov(0f);
        //fieldOfView.SetViewDistance(0f);

    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0) Kill();
    }

    private void Kill()
    {
        //Destroy(fieldOfView.gameObject);
        Destroy(gameObject);
    }

    public void ChangeState(State newState)
    {
        state.End();
        state.enabled = false;
        state = newState;
        state.enabled = true;
        state.Begin();
    }



    public bool SeesPlayer()
    {
        return enemyTargeting.SeesPlayer();
    }


    public void ToIdleView()
    {
        //fieldOfView.SetColor(new Color(1, 1, 1));
        //fieldOfView.SetFov(45f);
    }

    public void ToSearchView()
    {
        //fieldOfView.SetColor(new Color(1, 1, 0));
        //fieldOfView.SetFov(45f);
    }

    public void ToShootView()
    {
        //fieldOfView.SetColor(new Color(1, 0, 0));
        //fieldOfView.SetFov(3f);
    }

}
