using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullWolfState
{
    protected SkullWolf skullWolf;
    protected SkullWolfStateMachine skullWolfStateMachine;
    protected EnemiesDataSO skullWolfData;

    // Time when the state started, it get set eveytime the state is entered
    // => how long we've been in the state
    protected float startTime;

    // Name of the animation that will be played when the state is entered
    public string animationName;


    //Constructor
    public SkullWolfState(SkullWolf skullWolf, SkullWolfStateMachine skullWolfStateMachine, EnemiesDataSO skullWolfData, string animationName)
    {
        this.skullWolf = skullWolf;
        this.skullWolfStateMachine = skullWolfStateMachine;
        this.skullWolfData = skullWolfData;
        this.animationName = animationName;
    }


    //Called when the state is entered
    public virtual void Enter()
    {
        //Check for any conditions that will cause a transition to a different state
        DoChecks();

        //Reset the start time
        startTime = Time.time;

        //Play the animation
        skullWolf.animator.Play(animationName);

        //Debug.Log("Entered " + animationName);

    }

    //Called when the state is exited
    public virtual void Exit()
    {
        //Stop the animation
        skullWolf.animator.StopPlayback();
    }


    //Just like Update() and it's called every frame
    public virtual void LogicUpdate()
    {

    }

    //Just like FixedUpdate() and it's called every physics frame
    public virtual void PhysicsUpdate()
    {

    }

    //Called every frame to check for any conditions that will cause a transition to a different state
    // => This is where we will check for any input
    public virtual void DoChecks()
    {

    }

    public virtual void DoCheckUpdate()
    {

    }
}
