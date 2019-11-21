using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    //TODO: Singleton for this script.
    public int startingHealth = 6;  //TODO: make these private or set them as RULES.
    public int currentHealth;

    bool isDead = false;
    bool isDamaged;

    //HealthBar healthBar;
    public Slider healthGlobe;
    public Image damageImage;
    public float flashSpeed = 5f;                             //The speed the damageImage will fade at. 5 seconds.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);   //The color the damageImage is set to flash.

    private PlayerMovement playerMovement;                    //Reference to the player movement script.
    private Scene currentScene;
    //TODO: AudioClips & Animator when we implement these.

    void Awake()
    {
        playerMovement = this.GetComponent<PlayerMovement>(); //Link between this variable and the actual script location.
        currentScene = SceneManager.GetActiveScene();
        currentHealth = startingHealth; 
    }

	void Update ()
    {
        healthGlobe.value = currentHealth;
        ScreenFlash();                                 //Flash screen if damaged.
    }

    /* THIS IS THE PUBLIC METHOD WE USE TO DAMAGE THE PLAYER
     * USE IT WITH CAUTION WHEN MAKING YOUR PUZZLES/TRAPS/ANYDAMAGE (REFERENCE THIS SCRIPT AND JUST USE e.g. playerhealth.TakeDamage(1)
     * Game Design Guideline (Player has 6 health (3 hearts on UI) on levels 100/101/102, 8 health on lvl 103 , 10 health on 104/105)
     * Damage Input usually 1 health damage from A.I. per second, 1 health damage from Arrow(s), 2 or more from harder puzzle fails)*/

    public void TakeDamage (int amount)                
    {
        isDamaged = true;                               //Setting to true, so screen flashes.

        currentHealth -= amount;                       //Remove the desired amount from the player health.
        //Debug.Log("Player Taking Damage "+currentHealth+" "+Time.deltaTime);

        //healthbar.remainingHealth = currentHealth;
        //TODO: playerHurtAudio.Play();

        if (currentHealth <= 0 && !isDead)          //!isDead is here to make sure death audio/anim/effects play 1 time ONLY.
        {
            Death();

        }       
    }

    public void Heal (int amount)
    {
        isDead = false;                               //If the player is healed then is not dead
        playerMovement.shouldPlayerMove = true;        //If the player died but is now alive through healling make him able to walk again
        currentHealth += amount;                      //Add the desired amount to the player health.

        //TODO:isHealed = true;   maybe add heal animation/screen flash later on.

        //healthbar.remainingHealth = currentHealth;
    }

    public void Death()
    {
        isDead = true;                                    //

        PlayerInventory.playerInventory.ClearKeys();

        //TODO: animator & audio

        playerMovement.shouldPlayerMove = false;                   //Disables
    }

    void OnGUI() //Testing Debug REMOVE ME
    {
        if (isDead)
        {
            if (GUI.Button(new Rect(10, 220, 100, 30), "Restart ?"))
            {
                SceneManager.LoadScene(currentScene.buildIndex);
            }
        }

    }

    public void ScreenFlash ()
    {
        if (isDamaged)                                     //If the player has been damaged...
        {
            damageImage.color = flashColour;               //...flash the screen RED (flashcolour)
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime); //Otherwise, just clear it back to normal over 5 sec (flashspeed).
        }                                                                                                //The reason we multiply it with Time.deltaTime is to make certain  
                                                                                                         //no problem is created with different refresh rates on monitors.
        isDamaged = false;
    }
}
