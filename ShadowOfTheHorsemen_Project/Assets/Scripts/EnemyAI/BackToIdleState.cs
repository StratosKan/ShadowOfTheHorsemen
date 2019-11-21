using UnityEngine;
using UnityEngine.AI;

public class BackToIdleState : IState
{
    private NavMeshAgent navAgent;
    private Transform defaultTarget;
    private GameObject ownerGameObject;                           // Who am I talking to?
    private NotSoBasicAI notSoBasicAI;
    //private StateMachine enemyStateMachine;  //Can't do it this way because of the constructor.
    
    public BackToIdleState(NavMeshAgent navAgent, Transform defaultTarget, GameObject ownerGameObject)
    {
        this.navAgent = navAgent;
        this.defaultTarget = defaultTarget;
        this.ownerGameObject = ownerGameObject;
    }

    public void Enter()
    {
        //throw new System.NotImplementedException();
        this.notSoBasicAI = ownerGameObject.GetComponent<NotSoBasicAI>(); //phewww        
    }

    public void Execute()
    {
        navAgent.SetDestination(defaultTarget.position);              //Go back home...

        if (navAgent.remainingDistance < 2f)                        //...and when you arrive
        {
            //Debug.Log("BackToIdle_State -> Patrol_State");
            notSoBasicAI.GoPatrol();                                 //...start patrolling again.
        }
    }

    public void Exit()
    {
        //throw new System.NotImplementedException();
    }
    
}
