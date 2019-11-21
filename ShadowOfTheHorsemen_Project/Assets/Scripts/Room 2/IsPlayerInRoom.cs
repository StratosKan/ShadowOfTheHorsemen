using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerInRoom : MonoBehaviour
{

    //I simple script just to check if player is in the second room or not, in order for the puzzle to take place
    public bool InTheRoom; 

	void Start () {

        InTheRoom = false;
	}
		
	void OnTriggerEnter (Collider other) {
		InTheRoom = true;

	}
	void OnTriggerExit (Collider other) {
        InTheRoom = false;
	}

}
