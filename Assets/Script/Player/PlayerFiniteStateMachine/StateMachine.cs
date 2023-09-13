using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
 
    public PlayerState CurrentState { get; private set; }
    public PlayerState PreviousState { get; private set; }

    //Initialize the state machine with a starting state
    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    } 

    //Exit the current state and enter a new one
    public void ChangeState(PlayerState newState)
    {
        PreviousState = CurrentState;
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

}
