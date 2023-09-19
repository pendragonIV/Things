using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkullWolf : Enemy
{

    // EnemiesDataSO skullWolfData;

    #region Define Animation

    public const string SKULL_WOLF_IDLE = "SkullWolf_Idle";
    public const string SKULL_WOLF_ATTACK = "SkullWolf_Attack";
    public const string SKULL_WOLF_DEAD = "SkullWolf_Dead";
    public const string SKULL_WOLF_ATTACKED = "SkullWolf_Attacked";

    #endregion


    private void Awake()
    {
        this.enemyStateMachine = new EnemyStateMachine();
        //this.enemyData = skullWolfData;
        
        SideIdleState = new SkullWolfIdleState(this, enemyStateMachine, enemyData, SKULL_WOLF_IDLE);
        SideAttackState = new SkullWolfAttackState(this, enemyStateMachine, enemyData, SKULL_WOLF_ATTACK);
        SideDeadState = new SkullWolfDeadState(this, enemyStateMachine, enemyData, SKULL_WOLF_DEAD);
        SideHitState = new SkullWolfAttackedState(this, enemyStateMachine, enemyData, SKULL_WOLF_ATTACKED);

    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        spawnPosition = transform.position;

        enemyStateMachine.Initialize(SideIdleState);
    }

    // Update is called once per frame
    void Update()
    {
        enemyStateMachine.CurrentState.LogicUpdate();

        currentVelocity = rb.velocity; 
    }

    private void FixedUpdate()
    {
        enemyStateMachine.CurrentState.PhysicsUpdate();
    }

}
