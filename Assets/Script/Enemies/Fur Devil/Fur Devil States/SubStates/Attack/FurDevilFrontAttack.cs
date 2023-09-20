using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurDevilFrontAttack : FurDevilBaseState
{
    
    public FurDevilFrontAttack(Enemy enemy, EnemyStateMachine enemyStateMachine, EnemiesDataSO enemyData, string animationName) : base(enemy, enemyStateMachine, enemyData, animationName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void DoCheckUpdate()
    {
        base.DoCheckUpdate();
    }

    public override void Enter()
    {
        base.Enter();
        base.isDetectingPlayer = true;
        base.isOutOfRange = false;
        base.isInAttackRange = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(enemy.DistanceFromPlayer() > enemyData.attackRange)
        {
            base.isInAttackRange = false;
            enemyStateMachine.ChangeState(enemy.FrontRunState);
        }
        else if (enemy.isHit)
        {
            enemyStateMachine.ChangeState(enemy.FrontHitState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.SetVelocityX(0);
        enemy.SetVelocityY(0);
    }
}
