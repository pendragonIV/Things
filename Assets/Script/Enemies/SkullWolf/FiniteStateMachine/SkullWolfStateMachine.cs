using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWolfStateMachine
{
    public SkullWolfState CurrentState { get; private set; }
    public SkullWolfState PreviousState { get; private set; }

    //Initialize the state machine with a starting state
    public void Initialize(SkullWolfState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    //Exit the current state and enter a new one
    public void ChangeState(SkullWolfState newState)
    {
        PreviousState = CurrentState;
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
