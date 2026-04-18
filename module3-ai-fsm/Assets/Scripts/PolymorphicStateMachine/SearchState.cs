using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SearchState : State
{
    private float rotationDirection;

    public SearchState(StateMachine _stateMachine) : base(_stateMachine) { }

    public override void Enter()
    {
        stateMachine.searchTime = Time.time;//How long to search
        stateMachine.chicken.isStopped = true;//Stop the Chicken so it doesn't keep walking
        stateMachine.chicken.ResetPath();

        // Randomly choose to rotate left or right
        rotationDirection = Random.value > 0.5f ? 1f : -1f;
    }

    public override void Execute()
    {
        // Rotates the Chicken in place
        // Uses the rotationSpeed defined in StateMachine
        stateMachine.transform.Rotate(Vector3.up, rotationDirection * stateMachine.rotationSpeed * 50f * Time.deltaTime);

        float searchTimeElapsed = Time.time - stateMachine.searchTime;

        //Vision checking
        stateMachine.canSeePlayer = stateMachine.IsInViewCone();

        if (stateMachine.canSeePlayer)
        {
            stateMachine.ChangeState(new FleeState(stateMachine));
        }

        //Time checking
        if (searchTimeElapsed >= stateMachine.searchTimeThreshold)
        {
            stateMachine.ChangeState(new WanderingState(stateMachine));
        }
    }

    public override void Exit()
    {
        //Telling the chicken it is allowed to move again for the next state
        stateMachine.chicken.isStopped = false;
    }
}