using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrazingState : State
{
    public GrazingState(StateMachine _stateMachine) : base(_stateMachine) { } // Constructor: Connects this state to the main StateMachine

    public override void Enter()
    {
        stateMachine.grazingTime = Time.time;// Record the exact time the Chicken started grazing to use for the timer
    }

    public override void Execute()
    {

        stateMachine.chicken.speed = stateMachine.normalSpeed;// Maintain the standard movement speed

        // Forced Blindness: The Chicken is "distracted" by grazing and effectively ignores the player (vision checks are disabled)
        stateMachine.canSeePlayer = false;

        float idleTimeElapsed = Time.time - stateMachine.grazingTime;// How long the Chicken has been grazing
        if (idleTimeElapsed >= stateMachine.idleTimeThreshold)// If the elapsed time exceeds the stateMachine idle time threshhold
        {
            stateMachine.ChangeState(new WanderingState(stateMachine));// Go back to the Wandering State
        }
    }

    public override void Exit()
    {

    }

}
