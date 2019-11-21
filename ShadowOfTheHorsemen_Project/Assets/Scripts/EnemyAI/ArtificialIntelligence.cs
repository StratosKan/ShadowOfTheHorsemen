using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class ArtificialIntelligence : MonoBehaviour
{
    //Credits to Akis-san for AI state help.

    private enum States { Patrol, Chase, BacktoIdle }

    States states;

    private NavMeshAgent navAgent;
    
    public Transform Target;

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();        //reference
        
        states = States.Patrol;
    }

    public void Update()
    {

        if (Vector3.Distance(transform.position, Target.position) < 12.0f) //Checks distance from player. If less than 4 meter we chase him.
        {
            states = States.Chase;
        }
        else if (Vector3.Distance(transform.position, Target.position) > 12.0f)
        {
            states = States.Patrol;
        }

        Debug.Log(states);


        switch (states)
        {
            case States.BacktoIdle:

                EnemyPatrol.isPatrol = false;
                //DefaultDest = Point1;
                //navAgent.SetDestination(GameObject.Find("PatrolTarget1").position); //Make connection with Enemy Patrol script or add a default destination
                break;

            case States.Chase:

                EnemyPatrol.isPatrol = false;

                navAgent.SetDestination(Target.position);
                
                break;

            case States.Patrol:

                EnemyPatrol.isPatrol = true;

                //navAgent.SetDestination(DefaultDest.position);

                //if (DefaultDest == Point1)
                //{
                //    if (navAgent.remainingDistance < 0.5f)
                //    {
                //        DefaultDest = Point2;

                //    }
                //}
                //else if (DefaultDest == Point2)
                //{
                //    if (navAgent.remainingDistance < 0.5f)
                //    {
                //        DefaultDest = Point1;
                //    }
                //}
                break;
        }
    }
}