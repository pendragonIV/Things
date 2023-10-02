using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurDevil : Enemy
{


    #region Define Animation

    private const string FUR_DEVIL_FRONT_IDLE = "FurDevil_Front_Idle";
    private const string FUR_DEVIL_FRONT_ATTACK = "FurDevil_Front_Attack";
    private const string FUR_DEVIL_FRONT_DEAD = "FurDevil_Front_Dead";
    private const string FUR_DEVIL_FRONT_HIT = "FurDevil_Front_Hit";
    private const string FUR_DEVIL_FRONT_RUN = "FurDevil_Front_Run";

    #endregion


    private void Awake()
    {
        enemyStateMachine = new EnemyStateMachine();

        FrontIdleState = new FurDevilFrontIdle(this, enemyStateMachine, enemyData, FUR_DEVIL_FRONT_IDLE);
        FrontRunState = new FurDevilFrontRun(this, enemyStateMachine, enemyData, FUR_DEVIL_FRONT_RUN);
        FrontAttackState = new FurDevilFrontAttack(this, enemyStateMachine, enemyData, FUR_DEVIL_FRONT_ATTACK);
        FrontHitState = new FurDevilFrontHit(this, enemyStateMachine, enemyData, FUR_DEVIL_FRONT_HIT);
        FrontDeadState = new FurDevilFrontDead(this, enemyStateMachine, enemyData, FUR_DEVIL_FRONT_DEAD);
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
