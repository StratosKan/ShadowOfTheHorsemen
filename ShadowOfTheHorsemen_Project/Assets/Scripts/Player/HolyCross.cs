using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HolyCross : MonoBehaviour
{
    [Header("AoE Stun Duration")]
    public float stunDuration = 3f;      //the duration of the stun  

    [Header("Mechanic Radius")]
    public float hCrossStunRadius = 10f;  //the active radius of the mechanic

    [Header("Mechanic Cooldown")]
    public float hCrossMaxCooldown = 8f;  //the cooldown of the mechanic TODO:constant
    private float hCrossCooldown;

    private bool isReady;              //a boolean to check if cross is ready to reuse. It's possible to add a pickup that restores cooldown in the future.

    private bool hCrossMessage;

    private float cooldownOnGui;

    private Vector3 centerOfWrath;     //caching the player Pos

    private string enemy = "Enemy";           //caching enemy string

    public Slider powerSlider;

    private Scene currentScene;
    private int sceneIndex;


    void Start ()
    {
        //Debug.Log("Holy Cross Activated");
        //Setting up the refs
        isReady = true;
        hCrossCooldown = hCrossMaxCooldown;
        hCrossMessage = true;
        //TODO: animator && audio references
	}
	
	void Update ()
    {
        GetScene();
        if (this.enabled)
        {
            powerSlider.gameObject.SetActive(true);
        }

        if (Input.GetMouseButton(1) && isReady)
        {
            //TODO: Animator play && Audio play

            HolyWrath();                          //AoE stun
            hCrossCooldown = hCrossMaxCooldown;
            hCrossMessage = false;
            isReady = false;
        }
        if (!isReady)
        {
            hCrossCooldown -= Time.deltaTime;
            //Debug.Log(hCrossCooldown);
            cooldownOnGui = hCrossCooldown;  //Int cast for clarity.
            DisplayPower();

            //Debug.Log(cooldownOnGui);
            if (hCrossCooldown <= 0)
            {
                hCrossCooldown = hCrossMaxCooldown;
                isReady = true;
            }
        }

    }

    void HolyWrath()
    {
        centerOfWrath = this.gameObject.transform.position;                             //Ref to the current position of the player.

        //Creates a sphere centered on the player with specific radius that detects nearby colliders and adds them in an array.
  
        Collider[] thingsIHit = Physics.OverlapSphere(centerOfWrath, hCrossStunRadius); //TODO: Add enemy layer

        foreach (Collider hit in thingsIHit)                            //Simple loop for the array we created above...
        {
            if (hit.CompareTag(enemy))                                  //... if the hit object is tagged as enemy...
            {
                hit.gameObject.SendMessage("GoStun", stunDuration);       //...Calls the method Stun on every Monobehaviour script in this game object and sends the stunDuration.

                //Debug.Log(hit.gameObject.name + "" + "has been stunned");  //ENABLE IN CASE OF SYSTEM FAILURE NEBS.
            }
        }
    }

    void DisplayPower()
    {      
        if (!hCrossMessage && !isReady)
        {
            powerSlider.value = hCrossCooldown;
        }
        if (hCrossCooldown <= 0)
        {
            powerSlider.value = hCrossMaxCooldown;
        }
    }

    void GetScene()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneIndex = currentScene.buildIndex;
    }

    private void OnGUI()
    {
        if (hCrossMessage && sceneIndex == 2 )
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 4, 200f, 200f), "Press Right-MouseButton to use the Cross and stun enemies within 10 yards (8 seconds cooldown).");
        }
    }
}
