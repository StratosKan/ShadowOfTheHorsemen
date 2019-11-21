using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.7f;
    float timer;
    public int attackDamage = 1;

    GameObject player;                                        //Reference to the player object.
    PlayerHealth playerHealth;                                //Reference to the player health script.
    EnemyHealth enemyHealth;

    bool playerInRange;
    
	void Awake ()
    {
        //Setting up the references.
        player = GameObject.FindGameObjectWithTag("Player");  
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
	}

    void Update()
    {
        timer += Time.deltaTime;

        if (timer>=timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0) //Making sure everything is ok to procceed to attack.
        {
            Attack();
            //Debug.Log("Enemy Attacking " + Time.deltaTime);
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)            //If player triggers a collider...
        {
            playerInRange = true;                  //...set our boolean to true
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)           //...and when he exits   
        {
            playerInRange = false;                //...set it back to false.
        }
    }

    void Attack()
    {
        if(playerHealth.currentHealth > 0)          //If player health is higher than zero...
        {
            playerHealth.TakeDamage(attackDamage);  //...deal damage to him
        }

        timer = 0f;                                 //...and reset the timer

        //TODO:Attack animation && sound.
    }
}
