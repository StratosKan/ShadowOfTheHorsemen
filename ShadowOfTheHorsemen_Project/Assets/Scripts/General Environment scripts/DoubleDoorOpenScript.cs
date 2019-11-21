using UnityEngine;

public class DoubleDoorOpenScript : MonoBehaviour
{
    //this script controls all double doors that need to be opened by a specific key in PlayerInventory
    //through the inspector you can set if either the player has to get close to trigger the door or the door opens on it's own
    private PlayerInventory playerstats;

    public GameObject leftDoor;
    public GameObject rightDoor;

    public float doorOpenAngle = -80f;
    public float doorCloseAngle = 0f;
    public float smoothingFactor = 5f;

    public bool instantOpen = false; //this makes doors open without trigger
    public bool toRemoveKey = false; //this removes key from playerInventory

    public int doorNumber;
    public string requiredKey;
    string pickedUpKey;

    private Quaternion desiredDoorPosition;
    private Quaternion leftDoorRotation;
    private Quaternion rightDoorRotation;

    bool toOpenDoors = false;
    bool hasEntered = false;
    bool isNextToDoor = false;

    public AudioSource OpenSound;

    void Start ()
    {
        playerstats = PlayerInventory.playerInventory;
	}

    //basic rotation to handle the door opening
	void Update ()
    {
        pickedUpKey = playerstats.CurrentKeys(doorNumber);

        desiredDoorPosition = rightDoorRotation;

        if (isNextToDoor && pickedUpKey == requiredKey && toOpenDoors)
        {
            leftDoorRotation = Quaternion.Euler(0, -doorOpenAngle, 0);
            rightDoorRotation = Quaternion.Euler(0, doorOpenAngle, 0);

            if (!instantOpen)
            {
                leftDoor.transform.localRotation = Quaternion.Lerp(leftDoor.transform.localRotation, leftDoorRotation, smoothingFactor * Time.deltaTime);
                rightDoor.transform.localRotation = Quaternion.Lerp(rightDoor.transform.localRotation, rightDoorRotation, smoothingFactor * Time.deltaTime);
            }
            else
            {
                leftDoor.transform.localRotation = Quaternion.Lerp(leftDoor.transform.localRotation, leftDoorRotation, 180f * Time.deltaTime);
                rightDoor.transform.localRotation = Quaternion.Lerp(rightDoor.transform.localRotation, rightDoorRotation, 180f * Time.deltaTime);
            }
        }

        if (rightDoor.transform.localRotation == desiredDoorPosition)
        {
            toOpenDoors = false;
            if (toRemoveKey)
            {
                playerstats.RemoveKey(doorNumber);
            }
        }
        else
        {
            toOpenDoors = true;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isNextToDoor = true;

            if (!hasEntered && OpenSound != null)
            {
                OpenSound.Play();
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isNextToDoor = false;
            hasEntered = true;
        }
    }
}