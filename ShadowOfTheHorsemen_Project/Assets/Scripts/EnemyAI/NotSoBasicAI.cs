using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]                      //To use this script we must have a NavMeshAgent component attached to this gameObject.

public class NotSoBasicAI : MonoBehaviour
{
    private StateMachine enemyStateMachine = new StateMachine();

    private NavMeshAgent navAgent;
    [SerializeField]
    private Transform[] patrolTargets;                //Route1
    [SerializeField]        
    private Transform[] patrol2Targets;               //Route2
    [SerializeField]
    private Transform player;

    private int currentTarget = 0;
    private Transform defaultTarget;
    private int routeNum;

    //private IState searchForPlayer; //TODO: REMOVE
    private LayerMask playerMask;
    private float viewRange = 10f;
    //private int viewAngle = 120;
    private string playerTag = "Player";
    private bool playerFound = false;
    [SerializeField]
    private float searchTimer = 1f;      
    private string currentStateName; //TODO: OPTIMIZE FIELD AND ADD STATE NAME CHECK.

    private RaycastHit hit;  // V3

    private void Start ()
    {
        this.navAgent = this.GetComponent<NavMeshAgent>();
        this.defaultTarget = patrolTargets[0];
        this.playerMask = LayerMask.GetMask("Player");
        this.routeNum = 1;
        this.enemyStateMachine.ChangeState(new PatrolState(this.gameObject,this.navAgent,this.patrolTargets,this.patrol2Targets,this.currentTarget,this.routeNum));
        this.currentStateName = "Patrol";
        //this.searchForPlayer = new SearchForState(this.player1, this.gameObject, this.viewRange, this.playerTag, this.PlayerFound);  //MAYBE CHANGESTATE IS THE ANSWER???
	}

	private void Update ()
    {
        this.searchTimer -= Time.deltaTime;                //maybe make this method with enum in the future.

        if (this.searchTimer <= 0f)
        {
            this.playerFound = false;                      //always setting this to false on search start
            //this.searchForPlayer = new SearchForState(this.player1, this.gameObject, this.viewRange, this.playerTag, this.PlayerFound);
            this.enemyStateMachine.ChangeState(new SearchForState(this.playerMask, this.gameObject, this.viewRange, this.playerTag, this.PlayerFound));

            this.searchTimer = 1f;

            this.enemyStateMachine.ExecuteStateUpdate();
            //Debug.Log("Doing Search: " + (int)Time.time +" seconds");

            this.enemyStateMachine.SwitchToPreviousState();
            //Debug.Log("Switching to Previous State");
        }
        else
        {

            if (this.playerFound)
            {
                //Debug.Log("Chasing the player");
                this.enemyStateMachine.ChangeState(new ChaseState(this.navAgent, this.player)); //TODO: Setup chase state.
                this.currentStateName = "Chase";
            }
            else if (this.currentStateName != "Patrol")  //NOTE: Not using current state name for SearchForState.
            {
                this.enemyStateMachine.ChangeState(new BackToIdleState(this.navAgent, this.defaultTarget, this.gameObject)); // BackToIdle moves A.I. to default patrol point
                this.currentStateName = "BackToIdle";
                //Debug.Log("BackToIdle");
            }

            this.enemyStateMachine.ExecuteStateUpdate();
        }
	}

    public void PlayerFound(SearchResults searchResults)
    {
        var foundPlayer = searchResults.allHitObjectsWithRequiredTag;   //var = List<Collider> 

        if(foundPlayer[0] != null)
        {
            //var dist = foundPlayer[0].transform.position - this.transform.position;     //The distance between the player and the OwnerObject(A.I.). var = Vector3

            //Debug.Log(Vector3.Angle(dist, transform.forward));


            //if(Vector3.Angle(dist,transform.forward) < viewAngle / 2)                      //Algebra shiet to see if player is in the view angle.
            //{                                                                              //Needs Adjustment
            //    Debug.Log("Detected Playah");

            //    if (Physics.Raycast(this.transform.position , foundPlayer[0].transform.position, out hit, Mathf.Infinity))
            //    {
            //        Debug.Log("Raycast hit " + hit.collider.name);
            //        Debug.DrawLine(this.transform.position, foundPlayer[0].transform.position,Color.cyan,3f);
            //    }

            //    if (hit.collider.CompareTag(playerTag))
            //    {
                    //Debug.Log("Found " + foundPlayer[0].name);
                    playerFound = true;
            //    }
            //}           
        }
        else
        {
            //Debug.Log("Can't find Player");
            playerFound = false;
        }        
    }
    public void GoPatrol()
    {
        this.enemyStateMachine.ChangeState(new PatrolState(this.gameObject, this.navAgent, this.patrolTargets, this.patrol2Targets, this.currentTarget,this.routeNum));
        this.currentStateName = "Patrol";
        //Debug.Log("Patrol_State : Complete");
    }
    //public void GoStun(float stunDuration)
    //{
    //    var stunDur = stunDuration;

    //    this.currentStateName = "StunnedState";

    //    this.enemyStateMachine.ChangeState(new StunnedState(this.navAgent, (float)stunDur)); //cast might be worked arround.



    //    //MAYBE RETURN TO PREVIOUS STATE??? CHECK

    //}
    IEnumerator GoStun(float stunDuration)     // NOT WORKING
    {
        navAgent.speed = 0;

        //Debug.Log("Stunned " + Time.time);

        yield return new WaitForSeconds(stunDuration);

        //Debug.Log("UnStunned " + Time.time);

        navAgent.speed = 10;   //Also can set this to imported speed and use returnToPreviousSpeed;
    }
    public void ChangeRoute(int routeNumber)
    {
        if(routeNumber == 2)  //optimal
        {
            this.routeNum = 2;
            this.defaultTarget = patrol2Targets[0];  //patrol 2 array
            GoPatrol();

        }
        else if (routeNumber == 1)  //optimal
        {
            this.routeNum = 1;
            this.defaultTarget = patrolTargets[0];
            GoPatrol();
        }
        else if (routeNumber == 3)  //optimal
        {
            this.routeNum = 3;
            this.defaultTarget = patrol2Targets[0];
            GoPatrol();
        }

    }
}
