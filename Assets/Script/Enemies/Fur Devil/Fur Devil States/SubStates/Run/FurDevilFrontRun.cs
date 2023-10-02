using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurDevilFrontRun : FurDevilBaseState
{
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
        base.isOutOfRange = false;
        base.isInAttackRange = false;
        base.isDetectingPlayer = true;
        if (!GameManager.instance.player.GetComponent<Player>().enemies.Contains(enemy)) { 
            GameManager.instance.player.GetComponent<Player>().enemies.Add(enemy);
        }
        
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
                base.isOutOfRange = true;
            }
            else if (enemy.DistanceFromPlayer() <= enemyData.attackRange)
            {
                base.isInAttackRange = true;
            }

        }
        else
        {
            base.isOutOfRange = true;
        }

        if (isOutOfRange && enemy.DistanceFromSpawn() <= .5f)
        {
            enemyStateMachine.ChangeState(enemy.FrontIdleState);
        }
        else if (isInAttackRange)
        {
            enemyStateMachine.ChangeState(enemy.FrontAttackState);
        }
        else if (enemy.isHit)
        {
            enemyStateMachine.ChangeState(enemy.FrontHitState);
        }

        if (isOutOfRange)
        {
            if (GameManager.instance.player.GetComponent<Player>().enemies.Contains(enemy))
            {
                GameManager.instance.player.GetComponent<Player>().enemies.Remove(enemy);
            }
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
