using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolTargets;

    public static bool isPatrol; //This is managed from AI state script. That's the reason I chose to use static.

    private int currentTarget; 
    
    public float moveSpeed;

    public bool playerInRange; //TODO: Move this in enemy manager script or the script with sensors.

    //private NavMeshAgent navAgent;

    void Start()
    {
        transform.position = patrolTargets[0].position; //Starts teh lol patrol.

        //navAgent = GetComponent<NavMeshAgent>();

        currentTarget = 0;

        playerInRange = false;

        //isPatrol = true; //We do this on AI state script for the moment.
    }

    void Update() //TODO: make it a method and run it when needed instead of f***ing our update.
    {
        if (isPatrol == true)
        {
            if (transform.position == patrolTargets[currentTarget].position) // If we reach our target, adds next target on the list.
            {
                currentTarget++;

                if (currentTarget >= patrolTargets.Length) //Whenever we exceed the patrol array list it resets.
                {
                    currentTarget = 0;
                }
            }

            if (playerInRange == false) //
            {
                //navAgent.SetDestination(patrolTargets[currentTarget].position);
                transform.position =
              Vector3.MoveTowards
                  (
                  transform.position, patrolTargets[currentTarget].position, moveSpeed * Time.deltaTime
                  );
            }
            else if (playerInRange == true)
            {

            }
        }
    }
}