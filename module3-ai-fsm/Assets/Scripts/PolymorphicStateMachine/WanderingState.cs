using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingState : State
{
    public WanderingState(StateMachine _stateMachine) : base(_stateMachine) { }

    public override void Enter()
    {
    }

    public override void Execute()
    {
        stateMachine.chicken.speed = stateMachine.normalSpeed;

        Transform patrolTransform = stateMachine.wanderingWaypoints[stateMachine.wanderingIndex];
        stateMachine.chicken.SetDestination(patrolTransform.position);

        Vector3 positionXZ = stateMachine.transform.position;
        positionXZ.y = 0.0f;

        Vector3 patrolPositionXZ = patrolTransform.position;
        patrolPositionXZ.y = 0.0f;

        float distance = Vector2.Distance(positionXZ, patrolPositionXZ);
        if (distance < stateMachine.waypointThreshold)
        {
            stateMachine.ChangeState(new GrazingState(stateMachine));
        }

        stateMachine.canSeePlayer = stateMachine.IsInViewCone();
        if (stateMachine.canSeePlayer)
        {
            stateMachine.ChangeState(new FleeState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.wanderingIndex++;
        if (stateMachine.wanderingIndex >= stateMachine.wanderingWaypoints.Length)
            stateMachine.wanderingIndex = 0;
    }

}
