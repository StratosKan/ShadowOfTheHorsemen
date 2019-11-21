using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Room2_Puzzle : MonoBehaviour {

    //Room with Angel Puzzle. Script attached always to Angel Object

    public GameObject PlayerObject;

    private NavMeshAgent PlayerNav;

	public Transform StartingPosition;

    public GameObject Grandfather;
    protected Clock clock;

    RaycastHit See;
    Quaternion startRot;

    void Start () {
        startRot = transform.rotation;

        PlayerNav = PlayerObject.GetComponent<NavMeshAgent>();

        clock = Grandfather.GetComponent<Clock>();
	}
	

	void Update ()
    {
        if (clock.IsHiting)
        {
            IsLooking(); // When the clock is hiting midnight, the angel sees the moving player and warps him back at the start
        }
        else if (clock.IsTicking)
        {
            IsNotLooking(); // When the clock is just ticking, the player can move
        }
    }

	void IsLooking()
    {
        if (Physics.Raycast(transform.position, PlayerObject.transform.position, out See, Mathf.Infinity))
        {
            //Look at player exluding the y axis so that angel doesn't look down(or up)
            Vector3 lookPos = PlayerObject.transform.position - transform.position;
            lookPos.y = 0f;
            Quaternion fixedRot = Quaternion.LookRotation(lookPos);
            transform.rotation = fixedRot;

            if (Vector3.Distance(See.point,transform.position) > 1)
            {
                if (PlayerNav.hasPath)
                {
                    PlayerNav.Warp(StartingPosition.position); //Send player to start
                }
                else
                {
                }
            }  
        }       
    }

	void IsNotLooking ()
    {
        //So that the statue returns facing the wall
        transform.rotation = startRot;      
    }
}
