using UnityEngine;

public class PPPressScript : MonoBehaviour
{   
    //this script basicly handles each pressure plate movement and actions for room 3 in 102

    public static int pressurePlatesPressed = 0;
    public bool toBounceBack = false;
    public bool isPlateTrigger = false;

    private Vector3 targetTransform;
    private GameObject Player;
    private PlayerHealth playerHealth;
    private bool toMove = false;

    private AudioSource PPrightsource;
    public AudioClip PPrightclip;

    //references
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = Player.GetComponent<PlayerHealth>();

        PPrightsource = GetComponentInChildren<AudioSource>();
    }

    void FixedUpdate()
    {
        if (toMove)
        {
            gameObject.transform.localPosition = PPMover(gameObject.transform.localPosition, targetTransform);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Chair"))
        {
            targetTransform = new Vector3(0, -0.01f, 0);
            toMove = true;
            pressurePlatesPressed++;

            if (PPrightclip != null)
            {
                PPrightsource.PlayOneShot(PPrightclip, 0.05f);          
            }

            if (gameObject.CompareTag("PPIncorrect")) //damages player if the pressure plate has tag 
            {
                playerHealth.TakeDamage(1);
                Debug.Log("Trap -1 life. Total life: " + playerHealth.currentHealth);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Chair") && isPlateTrigger)
        {
            targetTransform = new Vector3(0, -0.01f, 0);

            if (toBounceBack)
            {
                toMove = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Chair"))
        {
            targetTransform = new Vector3(0, 0.03f, 0);
            toMove = false;
            pressurePlatesPressed--;
            if (gameObject.CompareTag("PPIncorrect") || toBounceBack)
            {
                toMove = true;
            }
        }
    }

    //controls the pressure plate vector for movement
    Vector3 PPMover(Vector3 startPos,Vector3 endPos)
    {    
       return Vector3.Lerp(startPos, endPos, 6 * Time.fixedDeltaTime);
    }
}