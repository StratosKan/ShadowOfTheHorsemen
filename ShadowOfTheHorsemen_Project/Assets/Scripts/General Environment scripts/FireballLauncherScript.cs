using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLauncherScript : MonoBehaviour
{
    //this is nested inside the FireballLauncher prefab
    //it loads the Fireball prefab for the resources folder and procedes to fire it through
    //instantiating by grabbing the rigidbody component and adding a force pointing towards the player position

    public float fireballSpeed = 5f;
    public float timeBetweenShots = 3f;
    public bool toLookAtPlayer = false;

    private Rigidbody rb;
    private GameObject player;
    private Vector3 direction;

    private GameObject fireballPrefab;
    private GameObject fireball;
    private Rigidbody fireball_rb;
    private bool isCreated = false;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fireballPrefab = (GameObject)(Resources.Load("Fireball"));
    }

    void Update()
    {
        ThrowFireball();  
    }

    //simple raycast
    void ThrowFireball()
    {
        RaycastHit hit;
        Vector3 rayDirection = player.transform.position - transform.position;
        direction = player.transform.position - transform.position;
        direction.Normalize();

        if (Physics.Raycast(transform.position, rayDirection, out hit, Mathf.Infinity))
        {
            if(hit.collider.gameObject == player.gameObject)
            {
                if (toLookAtPlayer)
                {
                    transform.LookAt(player.transform.position);
                }
                if (!isCreated)
                {
                    fireball = Instantiate(fireballPrefab, transform.position, transform.rotation);

                    fireball_rb = fireball.GetComponent<Rigidbody>();

                    fireball_rb.AddForce(direction * fireballSpeed, ForceMode.VelocityChange);

                    isCreated = true;

                    StartCoroutine(TimeToPass(fireball));
                }
            }
        }
    }

    //just a simple timer to handle how much fireballs get launched
    public IEnumerator TimeToPass(GameObject fireball) //Starts a timer in seconds until the fireball is destroyed.
    {
        yield return new WaitForSeconds(timeBetweenShots); //Waits 3 (default) seconds and then...

        isCreated = false;
    }
}