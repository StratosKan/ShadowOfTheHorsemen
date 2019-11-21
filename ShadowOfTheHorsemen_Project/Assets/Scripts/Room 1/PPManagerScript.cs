using UnityEngine;
using UnityEngine.AI;

public class PPManagerScript : MonoBehaviour
{
    //this mainly handles Room1 in 101 room

    public GameObject[] Plates;

    public Transform Spawn;
    private GameObject Player;
    private NavMeshAgent navMeshAgent;
    private PlayerHealth playerHealth;
    private bool insideRoomOne = false;
    private int tempHealth;
    //cheat sheet
    //path will be 4,9,10,15,20,19,18,17,16,26,27,32
    //by array num 3,8,9,14,19,18,17,16,15,25,26,31

    //set up references
    void Start ()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = Player.GetComponent<PlayerHealth>();
        navMeshAgent = Player.GetComponent<NavMeshAgent>();
    }

    void FixedUpdate ()
    {
        if (playerHealth.currentHealth == 0 && insideRoomOne) // this is to remember the health that that player got in the room with since they effectively die when the room resets
        {
            playerHealth.Heal(tempHealth);
            navMeshAgent.Warp(Spawn.position); //SPAWN SHOULD BE SET EXACTLY AT CORRECT TRANSFORM ELSE NAVMESH GETS STUPID

            for (int i = 0; i < Plates.Length; i++)
            {
                Plates[i].transform.localPosition = new Vector3(0, 0.03f, 0);
            }
        }
	}

    //room enter
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tempHealth = playerHealth.currentHealth;
            insideRoomOne = true;
        }
    }

    //room exit
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideRoomOne = false;
        }
    }
}
