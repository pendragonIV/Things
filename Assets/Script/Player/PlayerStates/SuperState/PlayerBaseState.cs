using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : PlayerState
{
    protected float InputHorizontal;
    protected float InputVertical;

    public PlayerBaseState(Player player, StateMachine stateMachine, PlayerProps playerProps, string animationName) : base(player, stateMachine, playerProps, animationName)
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

        InputHorizontal = player.inputSystem.InputHorizontal;
        InputVertical = player.inputSystem.InputVertical;

        #region Switch state based on attack input
        if (player.inputSystem.IsAttack)
        {
            player.inputSystem.ResetAttackInput();

            if (player.stateMachine.CurrentState == player.sideIdleState 
                || player.stateMachine.CurrentState == player.sideWalkState
                || player.stateMachine.CurrentState == player.topDiagonalWalkState
                || player.stateMachine.CurrentState == player.bottomDiagonalWalkState)
            {
                stateMachine.ChangeState(player.sideAttackState);
            }
            else if(player.stateMachine.CurrentState == player.backIdleState || player.stateMachine.CurrentState == player.backWalkState)
            {
                stateMachine.ChangeState(player.backAttackState);
            }
            else if(player.stateMachine.CurrentState == player.frontIdleState || player.stateMachine.CurrentState == player.frontWalkState)
            {
                stateMachine.ChangeState(player.frontAttackState);
            }

        }
        #endregion

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
