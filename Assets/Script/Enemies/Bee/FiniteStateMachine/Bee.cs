using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    #region Define Animation

    private const string BEE_FRONT_DEAD = "Bee_Front_Dead";
    private const string BEE_FRONT_FLY = "Bee_Front_Fly";

    #endregion


    private void Awake()
    {
        enemyStateMachine = new EnemyStateMachine();

        FrontIdleState = new BeeIdleState(this, enemyStateMachine, enemyData, BEE_FRONT_FLY);
        FrontRunState = new BeeFlyState(this, enemyStateMachine, enemyData, BEE_FRONT_FLY);
        FrontDeadState = new BeeDeadState(this, enemyStateMachine, enemyData, BEE_FRONT_DEAD);
        FrontHitState = new BeeHitState(this, enemyStateMachine, enemyData, BEE_FRONT_FLY);
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
