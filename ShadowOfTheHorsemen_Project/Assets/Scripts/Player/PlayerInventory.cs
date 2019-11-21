using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    /*                  ==== READ ME ===
     * PlayerInventory slots 0 - 4 are slots only for the ORBS
     * If adding/messing with slots please leave 0 - 4 always unused by puzzles              
     */
    public static PlayerInventory playerInventory;

    private int keys = 11; //number
    public string[] keyNames; //id

    void Awake() //Singleton creation for our project
    {
        if (playerInventory == null)
        {
            DontDestroyOnLoad(gameObject);
            playerInventory = this;

        }
        else if (playerInventory != this)
        {
            Destroy(gameObject);
        }

        keyNames = new string[keys];
    }

    public void AddKey(string keyName,int keyNumber) //adds a key with name and number(id)
    {
        keyNames[keyNumber] = keyName;
    }

    public void RemoveKey(int keyNumber) //removes key id
    {
        keyNames[keyNumber] = null;
    }

    public void ClearKeys() //clears keys for mainly death purposes
    {
        for (int i = 0; i < keyNames.Length; i++)
        {
            keyNames[i] = null;
        }
    }

    public string CurrentKeys(int keyNumber) //check if id exists and with what string
    {
        return keyNames[keyNumber];
    }
}
