using UnityEngine;

public class PauseScript : MonoBehaviour
{

    //A script for the PauseMananager in the game. The Manager controls the Lore info of the game, when the player clicks on a Parchment
    //As long as there is a parchment open, the movement and the main camera scripts are disabled to pause a bit of the environment
    //When the player presses the X button that every parchment screen has, they are enabled once again.
    // Also the Manager is used for Pausing the game, enabling a simple Pause UI for the player to return to Main Menu or Quit the Game

    public GameObject[] TintScreen;
    public GameObject PauseScreen;
    public GameObject Player;
    public GameObject Camera;

    private PlayerMovement pM;
    private CameraLook cL;

    private bool toggleFlag = true; // the Toggle var in order to activate and deactivate the objects in the scene

    void Awake ()
    {
        pM = Player.GetComponent<PlayerMovement>();
        cL = Camera.GetComponent<CameraLook>();
    }

    void Update ()
    {
        for (int i = 0; i < TintScreen.Length; i++) //We use an array in order to have as many Lore screens as we want
        {
            if (!PauseScreen.activeSelf)
            {
                if (TintScreen[i].activeSelf)
                {
                    toggleFlag = false;
                    break;
                }
                else
                {
                    toggleFlag = true;
                }
            }
        }
        if (Input.GetButtonDown("Cancel") && toggleFlag) //Pause is enabled with the Esc button as usual. The logic is the same with the Lore screens
        {
            pM.enabled = !toggleFlag;
            cL.enabled = !toggleFlag;

            if (PauseScreen.activeSelf)
            {
                PauseScreen.SetActive(false);
                toggleFlag = false;
                Time.timeScale = 1.0f;

            }
            else
            {
                PauseScreen.SetActive(true);
                toggleFlag = true;
                Time.timeScale = 0f;
            }
        }

        if (!PauseScreen.activeSelf)
        {
            if (toggleFlag)
            {
                pM.enabled = true;
                cL.enabled = true;
            }
            else
            {
                pM.enabled = false;
                cL.enabled = false;
            }
        }
    }
}