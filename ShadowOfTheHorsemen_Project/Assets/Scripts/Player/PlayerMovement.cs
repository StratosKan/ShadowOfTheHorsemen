using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour  
{
    //second bread and butter of the game this handles how the player is able to move
    //basicly the mouse hit from raycasting finds a position that if is on the navmesh then the player tries to get to
    //also spawns some "Sparkles" which is basicly a pointer to where the player is heading

    public PlayerMovement playerMovement { get; set; }
    private NavMeshAgent agent;
    private GameObject sparklePrefab;
    private GameObject sparkle;

    public static float moveAmount;

    public bool shouldPlayerMove = true;

    Vector3 agentPath;
    RaycastHit hit;

    void Start()
    {
        sparklePrefab = (GameObject)(Resources.Load("Sparkle"));
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5f || !shouldPlayerMove)
        {
            agent.ResetPath();
        }
        if (Input.GetMouseButton(0) && shouldPlayerMove)
        {
            Move();  //Η κίνηση του παίκτη μας
        }
    }
    void LateUpdate()
    {
        if (agent.hasPath)
        {
            moveAmount = Mathf.Clamp(agent.remainingDistance * 0.3f, 0, 2);
        }
        else
        {
            moveAmount = 0;
        }
    }

    void Move()
    {
        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 35f, layerMask: 9))
            {
                if (hit.collider.gameObject.layer != 10)
                {
                    agent.SetDestination(hit.point);
                    sparkle = Instantiate(sparklePrefab, hit.point, Quaternion.LookRotation(hit.normal));
                    sparkle.transform.LookAt(2 * transform.position - agent.transform.position);
                    sparkle.transform.Rotate(-90, 0, 180);
                    sparkle.transform.Translate(0, 0, 0.1f);
                    Destroy(sparkle, 1.25f*Time.deltaTime);
                }
            }
        }
    }
}