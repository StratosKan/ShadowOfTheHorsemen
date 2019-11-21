using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    private GameObject ownerGameObject;                           // Who am I talking to?
    private NavMeshAgent navAgent;                                // Who do i command?
    private Transform[] patrolTargets;                            // What are my targets Sir?
    private Transform[] patrol2Targets;
    private int currentTarget;

    private int routeNumber;

    private enum Routes { Route1, Route2, Alert }     //Sub-state for patrol state.
    Routes routes;

    public PatrolState(GameObject ownerGameObject, NavMeshAgent navAgent, Transform[] patrolTargets, Transform[] patrol2Targets,int currentTarget,int routeNumber)
    {
        this.ownerGameObject = ownerGameObject;
        this.navAgent = navAgent;
        this.patrolTargets = patrolTargets;
        this.patrol2Targets = patrol2Targets;
        this.currentTarget = currentTarget;
        this.routeNumber = routeNumber;
    }

    public void Enter()
    {
        if (routeNumber == 1)
        {
            routes = Routes.Route1;
        }
        else if (routeNumber == 2)
        {
            routes = Routes.Route2;
        }
        else if (routeNumber == 3) //NOT IMPLEMENTED YET
        {
            routes = Routes.Alert;
        }
        //Debug.Log("Patrol State entered with " + routes);
    }

    public void Execute()
    {
        if (this.routes == Routes.Route1)
        {
            //Debug.Log("Doing route 1");
            navAgent.SetDestination(this.patrolTargets[currentTarget].position);     //Go to the current target...

            if (ownerGameObject.transform.position == this.patrolTargets[currentTarget].position)    //...and when you arrive
            {

                if (navAgent.remainingDistance < 0.5f)                          //doublecheck to be certain (overkill for optimization but it was buggy on first place)
                {
                    currentTarget++;                                            //...go NEXT!

                    if (currentTarget >= patrolTargets.Length)                 //Whenever we exceed the patrol array list...
                    {
                        currentTarget = 0;                                     //... it resets.
                    }

                }
                //Debug.Log(navAgent.remainingDistance);
            }

        }
        else if (this.routes == Routes.Route2)
        {
            navAgent.SetDestination(this.patrol2Targets[currentTarget].position);     //Go to the current target...

            if (ownerGameObject.transform.position == this.patrol2Targets[currentTarget].position)    //...and when you arrive
            {
                //Debug.Log("Doing route 2");

                if (navAgent.remainingDistance < 0.5f)                          //doublecheck to be certain (overkill for optimization but it was buggy on first place)
                {
                    this.currentTarget++;                                            //...go NEXT!

                    if (this.currentTarget >= this.patrol2Targets.Length)                 //Whenever we exceed the patrol array list...
                    {
                        this.currentTarget = 0;                                     //... it resets.
                    }

                }
                //Debug.Log(navAgent.remainingDistance);
            }
        }
    }

    public void Exit()
    {
        //throw new System.NotImplementedException();
    }
    
    void ChangeRoute(int routeNumber)  //TODO: CLEAN UP
    {
        if (routeNumber == 1)
        {
            routes = Routes.Route1;
        }
        else if (routeNumber == 2)
        {
            routes = Routes.Route2;
        }
        //else if (routeNumber == 3)  //Alert
        //{
        //    routes = Routes.Alert;
        //}
    }
}
