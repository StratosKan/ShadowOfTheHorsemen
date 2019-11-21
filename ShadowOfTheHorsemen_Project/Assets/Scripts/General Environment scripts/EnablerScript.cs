using UnityEngine;

public class EnablerScript : MonoBehaviour
{
    //just a simple script which looks for a key in playerInventory
    //it is mostly used for triggering the exit door when the player has the required prerequisites

    public int doorNumber;
    public string requiredKey;
    string pickedUpKey;

    public GameObject orbToEnable;
    public BoxCollider boxAreaToEnable;

    private PlayerInventory playerstats;
    private BoxCollider boxCollider;

	void Start ()
    {
        playerstats = PlayerInventory.playerInventory;
        boxCollider = boxAreaToEnable.GetComponent<BoxCollider>();
    }

    void Update ()
    {
        pickedUpKey = playerstats.CurrentKeys(doorNumber);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (pickedUpKey == requiredKey)
            {
                if (orbToEnable != null)
                {
                    orbToEnable.SetActive(true);
                }
                if (boxCollider != null)
                {
                    boxCollider.enabled = true; //rudimentary probably should enable a state to a global manager.
                }
            }
        }
    }
}
