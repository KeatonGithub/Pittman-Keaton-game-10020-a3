using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Required to calculate valid NavMesh points

public class FleeState : State
{
    // How far ahead the Chicken will try to pathfind away from the player
    private float fleeDistance = 10f;// How far the chicken will flee
    private float safeDistance = 12f; // The Chicken won't stop fleeing until it is at least this far away

    public FleeState(StateMachine _stateMachine) : base(_stateMachine) { }

    public override void Enter()
    {
        // Set speed to the faster flee speed
        stateMachine.chicken.speed = stateMachine.fleeSpeed;
    }

    public override void Execute()
    {
        // Calculate the direction and distance
        Vector3 diff = stateMachine.transform.position - stateMachine.character.position;
        float currentDist = diff.magnitude;
        Vector3 dirAwayFromPlayer = diff.normalized;

        // Always update the destination to keep running away
        Vector3 fleeTarget = stateMachine.transform.position + (dirAwayFromPlayer * fleeDistance);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleeTarget, out hit, fleeDistance, NavMesh.AllAreas))
        {
            stateMachine.chicken.SetDestination(hit.position);
        }

        // Updated Transition Logic
        stateMachine.canSeePlayer = stateMachine.IsInViewCone();

        // only stop fleeing if:
        // The Chicken is at a safe distance AND we can't see the player anymore
        if (currentDist > safeDistance && !stateMachine.canSeePlayer)
        {
            stateMachine.ChangeState(new SearchState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.chicken.speed = stateMachine.normalSpeed;
    }
}