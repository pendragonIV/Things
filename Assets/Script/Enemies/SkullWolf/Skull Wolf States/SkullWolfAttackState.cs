using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWolfAttackState : EnemyState
{

    private bool isDetectingPlayer;
    private bool isOutOfRange;
    

    public SkullWolfAttackState(SkullWolf skullWolf, EnemyStateMachine skullWolfStateMachine, EnemiesDataSO skullWolfData, string animationName) : base(skullWolf, skullWolfStateMachine, skullWolfData, animationName)
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
        isDetectingPlayer = true;
        isOutOfRange = false;
    }

    public override void Exit()
    {
        base.Exit();
        isDetectingPlayer = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isDetectingPlayer = enemy.checkPlayerInRange();

        if(isDetectingPlayer)
        {
            if(enemy.DistanceFromSpawn() >= 5)
            {
                isOutOfRange = true;
            }

        }
        else
        {
            isOutOfRange = true;
        }

        if(isOutOfRange && enemy.DistanceFromSpawn() <= 1f)
        {
            enemyStateMachine.ChangeState(enemy.SideIdleState);
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

