using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurDevilFrontRun : FurDevilBaseState
{
    private bool isDetectingPlayer;
    private bool isOutOfRange;
    private bool isInAttackRange = false;

    public FurDevilFrontRun(Enemy enemy, EnemyStateMachine enemyStateMachine, EnemiesDataSO enemyData, string animationName) : base(enemy, enemyStateMachine, enemyData, animationName)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isDetectingPlayer = enemy.checkPlayerInRange();

        if (isDetectingPlayer)
        {
            if (enemy.DistanceFromSpawn() >= 5)
            {
                isOutOfRange = true;
            }
            else if (enemy.DistanceFromPlayer() <= enemyData.attackRange)
            {
                isInAttackRange = true;
            }

        }
        else
        {
            isOutOfRange = true;
        }

        if (isOutOfRange && enemy.DistanceFromSpawn() <= .5f)
        {
            enemyStateMachine.ChangeState(enemy.FrontIdleState);
            isOutOfRange = false;
        }
        else if (isInAttackRange)
        {
            //enemyStateMachine.ChangeState(enemy.FrontAttackState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (isDetectingPlayer && !isOutOfRange)
        {
            if (enemy.DistanceFromPlayer() >= enemyData.attackRange)
            {
                enemy.MoveToPlayer();
                enemy.spriteRenderer.flipX = enemy.CheckXPlayer();
            }
        }
        else
        {
            enemy.MoveToSpawn();
            enemy.spriteRenderer.flipX = enemy.CheckXSpawn();
        }
    }
}
