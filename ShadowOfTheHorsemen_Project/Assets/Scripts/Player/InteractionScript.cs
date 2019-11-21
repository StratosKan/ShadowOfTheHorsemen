using UnityEngine;
using UnityEngine.UI;

public class InteractionScript : MonoBehaviour
{
    //This is the bread and butter of game most game functions are controlled with this

    //When the mouse hovers over the GameObject, it turns to this color
    public Color m_MouseOverColor = Color.yellow;
    public bool isObjectHighlightable = true;

    //This stores the GameObject’s original color
    Color m_OriginalColor;

    //Get the GameObject’s mesh renderer to access the GameObject’s material and color
    MeshRenderer m_Renderer;

    GameObject playerCamera;

    public enum KeyObject { KEY,INTERACTABLE };
    public KeyObject currentObject;

    public string keyName;
    public int keyNumber;
    public GameObject[] objectToToggle;
    private bool toggleFlag = false;

    public float interactRange = 15f;

    public Canvas canvas;
    public Slider slider;

    public int maxSliderValue = 1;

    bool ableToPickup = false;
    bool isOverObject = false;

    private GameObject Player;
    private PlayerMovement playerMovement;

    private GameObject ai;
    private NotSoBasicAI aiController;
    private int newRoute = 2;


    //We grab all neccessary preferences
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); //Finds the player
        playerMovement = Player.GetComponent<PlayerMovement>(); //Grabs the PlayerMovement script in order for the player to stop moving when interacting

        slider.maxValue = maxSliderValue;
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

        ai = GameObject.FindGameObjectWithTag("Enemy"); // WORKS FOR 1 ENEMY (VIABLE FOR PROTOTYPE , MAKE ARRAY FOR MORE AI)
        if (ai != null)
        {
            aiController = ai.GetComponent<NotSoBasicAI>();
        }

        //Fetch the mesh renderer component from the GameObject
        m_Renderer = GetComponent<MeshRenderer>();
        //Fetch the original color of the GameObject
        m_OriginalColor = m_Renderer.material.color;

        //this is to check if objects are enabled/disabled before being manipulated by the user
        foreach (GameObject objects in objectToToggle)
        {
            toggleFlag = objects.activeSelf;
            if (toggleFlag)
            {
                PlayerInventory.playerInventory.AddKey(keyName, keyNumber);
            }
            else
            {
                PlayerInventory.playerInventory.RemoveKey(keyNumber);
            }
        }

    }

    void Update()
    {
        //this script is based on enumarators instead of tags
        //depending on currentObject that we select from inspector we can effectively create an instance of an object
        if (currentObject == KeyObject.KEY)
        {
            if (ableToPickup)
            {
                PlayerInventory.playerInventory.AddKey(keyName,keyNumber);
                playerMovement.shouldPlayerMove = true;
                ableToPickup = false;
                if (ai != null)
                {
                    aiController.ChangeRoute(newRoute);    //Command the AI to follow the 2nd patrol path.
                }
                gameObject.SetActive(false);

            }
        }
        if (currentObject == KeyObject.INTERACTABLE)
        {
            if (ableToPickup)
            {              
                PlayerInventory.playerInventory.AddKey(keyName, keyNumber);
                foreach (GameObject objects in objectToToggle)
                {
                    toggleFlag = objects.activeSelf;
                    if (!toggleFlag)
                    {
                        PlayerInventory.playerInventory.AddKey(keyName, keyNumber);
                    }
                    else
                    {
                        PlayerInventory.playerInventory.RemoveKey(keyNumber);
                    }
                    objects.SetActive(!toggleFlag);
                }
                ableToPickup = false;

                if (!ableToPickup)
                {
                    float time =+ Time.deltaTime;
                    if(time >= 1f)
                    {
                        playerMovement.shouldPlayerMove = true;
                    }
                }
            }
        }
        Interact();
    }

    void OnMouseOver()
    {
        isOverObject = true;
    }

    void OnMouseExit()
    {
        isOverObject = false;
        playerMovement.shouldPlayerMove = true;
    }

    //this mainly works with raycasting objects that have the interactionScript and then things happen accordingly
    void Interact()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), interactRange, layerMask: 9)) //If the camera is interactRange away from the object then is able to be interacted
        {
            if (isOverObject)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    playerMovement.shouldPlayerMove = false;
                    if (currentObject == KeyObject.INTERACTABLE)
                    {
                        ableToPickup = true;
                    }
                }
                else if (Input.GetMouseButton(0) && currentObject == KeyObject.KEY)
                {
                    canvas.transform.LookAt(playerCamera.transform);
                    if (slider.value < maxSliderValue)
                    {
                        slider.value++;
                        playerMovement.shouldPlayerMove = false;

                    }
                    else if (slider.value == maxSliderValue)
                    {
                        playerMovement.shouldPlayerMove = true;
                        slider.value = maxSliderValue;
                        ableToPickup = true;
                    }

                }
                if (Input.GetMouseButtonUp(0))
                {
                    playerMovement.shouldPlayerMove = true;
                    slider.value = 0;
                    ableToPickup = false;
                }
            }
            else
            {
                if (isObjectHighlightable)
                {
                    // Reset the color of the GameObject back to normal
                    m_Renderer.material.color = m_OriginalColor;
                }
                slider.value = 0;
            }

            if (isObjectHighlightable)
            {
                // Change the color of the GameObject to red when the mouse is over GameObject
                m_Renderer.material.color = m_MouseOverColor;
            }
        }
    }
}