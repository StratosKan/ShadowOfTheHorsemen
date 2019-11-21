using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyCrossMechanic : MonoBehaviour
{
    HolyCross holyCrossActivate;      //Reference to the HolyCross script component on player. Instead of enabling a boolean we are enabling the whole script.
 
    void Start ()
    {
        holyCrossActivate = GameObject.FindGameObjectWithTag("Player").GetComponent<HolyCross>();       
	}

    void OnTriggerEnter(Collider other)
    {
        //TODO: PersistentScene add HolyCross

        holyCrossActivate.enabled = true;

        //TODO: Enable HolyCross extension-prefab on Player

        Destroy(gameObject);             //Destroys the Holy Cross game object (TODO: add animation in a future version)
    }
}
