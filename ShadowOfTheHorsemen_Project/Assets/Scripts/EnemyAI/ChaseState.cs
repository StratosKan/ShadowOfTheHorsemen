using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    private NavMeshAgent navAgent;
    private Transform player;

    public ChaseState(NavMeshAgent navAgent, Transform player)
    {
        this.navAgent = navAgent;
        this.player = player;
    }

    public void Enter()
    {
        //throw new System.NotImplementedException();
        //TODO: Big(10.0f) Sensors With OnTriggerExit(player)
    }

    public void Execute()
    {
        navAgent.SetDestination(player.position);                   //Let's hunt.
        //TODO: if remaining distance is < 0.4f , atk animation , navAgentSpeed=0 for animationDuration and then applyDamage
    }

    public void Exit()
    {
        //throw new System.NotImplementedException();
    }
}
