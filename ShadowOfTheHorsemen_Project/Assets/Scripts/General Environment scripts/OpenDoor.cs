using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    //handles opening mostly secret doors or single doors not being handled by doobledooropenscript
    //basicly same functionality with doobledooropenscript but instead of rotating the doors are handled by axisMovement a vector3
    //for simple xyz axis moving

    private PlayerInventory playerstats;

    public Vector3 axisMovement;
    public float smoothingFactor = 1f;

    public bool instantOpen = false;
    public bool toRemoveKey = false;

    public int doorNumber;
    public string requiredKey;
    string pickedUpKey;

    private Vector3 desiredDoorPosition;

    bool toOpenDoors = false;

    public AudioSource SlidingSound;

    void Start()
    {
        playerstats = PlayerInventory.playerInventory;
    }

    void FixedUpdate()
    {
        desiredDoorPosition = axisMovement;

        pickedUpKey = playerstats.CurrentKeys(doorNumber);

        if (instantOpen && pickedUpKey == requiredKey && toOpenDoors)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, desiredDoorPosition, smoothingFactor * Time.fixedDeltaTime);

            if (!SlidingSound.isPlaying && SlidingSound != null)
            {
                SlidingSound.Play();
            }

        }

        if (transform.localPosition.Equals(desiredDoorPosition))
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
}