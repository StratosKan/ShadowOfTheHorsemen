using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    private enum States { Patrol, Chase, Stunned, BacktoIdle}  //Old Enemy State machine 

    States states;

    private enum Routes { Route1, Route2, Alert}     //Sub-state for patrol state.

    Routes routes;                                   //WARNING: THIS ONLY CHANGES FROM EVENTS ON LEVEL.
    
    private NavMeshAgent navAgent;

    public Transform[] patrolTargets;                //Route1
    public Transform[] patrol2Targets;               //Route2

    private int currentTarget;

    private Transform DefaultDest; 
    public Transform Target;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();                              //Reference.
        DefaultDest = patrolTargets[0];                                       //We set the default destination
        states = States.Patrol;                                               //... and the default state.
        routes = Routes.Route1; 
        currentTarget = 0;        
    }

    private void Update()
    {

        if(Vector3.Distance(transform.position, Target.position) < 10.0f)        //When distance from player is lower than Xf run after him. 
        {
            states = States.Chase;
        }
        else if (Vector3.Distance(transform.position, Target.position) >= 10.0f) //When distance from player is bigger than Xf return to idle state.
        {
            if (states != States.Patrol)
            {
                states = States.BacktoIdle;
            }
        }

        //Debug.Log(states);
        EnemyState();        
    }
    IEnumerator Stun(float stunDuration)     // TODO: Make it proper method.
    {
        states = States.Stunned;
        navAgent.speed = 0;

        yield return new WaitForSeconds(stunDuration);

        states = States.Patrol;
        navAgent.speed = 10;
    }
    void ChangeRoute(int routeNumber)
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
    void EnemyState()
    {
        switch (states)
        {
            case States.BacktoIdle:

                navAgent.SetDestination(DefaultDest.position);              //Go back home...

                if (navAgent.transform.position == DefaultDest.position)    //...and when you arrive
                {
                    //Debug.Log("State Change to patrol");
                    states = States.Patrol;                                 //...start patrolling again.
                }

                break;

            case States.Chase:

                navAgent.SetDestination(Target.position);                   //Let's hunt.
                break;

            case States.Stunned:                                            //TODO: proper statement.
                break;

            case States.Patrol:

                if (routes == Routes.Route1)
                {
                    navAgent.SetDestination(patrolTargets[currentTarget].position);     //Go to the current target...

                    if (transform.position == patrolTargets[currentTarget].position)    //...and when you arrive
                    {

                        if (navAgent.remainingDistance < 0.3f)                          //doublecheck to be certain (overkill for optimization but it was buggy on first place)
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
                else if (routes == Routes.Route2)
                {
                    navAgent.SetDestination(patrol2Targets[currentTarget].position);     //Go to the current target...

                    if (transform.position == patrol2Targets[currentTarget].position)    //...and when you arrive
                    {

                        if (navAgent.remainingDistance < 0.3f)                          //doublecheck to be certain (overkill for optimization but it was buggy on first place)
                        {
                            currentTarget++;                                            //...go NEXT!

                            if (currentTarget >= patrol2Targets.Length)                 //Whenever we exceed the patrol array list...
                            {
                                currentTarget = 0;                                     //... it resets.
                            }

                        }
                        //Debug.Log(navAgent.remainingDistance);
                    }
                }
                break;
        }
    }
}
