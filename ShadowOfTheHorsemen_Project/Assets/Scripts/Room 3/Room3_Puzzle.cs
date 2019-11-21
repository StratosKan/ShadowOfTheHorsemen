using UnityEngine;

public class Room3_Puzzle : MonoBehaviour
{
    //basicly controls the 3rd room in 102
    //is simple it reads which candles are on
    //and if all pressure plates are pressed for pppressscript and then enables the script which controls the door opening

    public OpenDoor scriptEnabler;

    private bool platesReady = false;
    private bool candlesReady = false;

	
	void Update ()
    {
        if (PlayerInventory.playerInventory.CurrentKeys(6) != null && PlayerInventory.playerInventory.CurrentKeys(7) == null && PlayerInventory.playerInventory.CurrentKeys(8) != null)
        {
            candlesReady = true;
        }
        else
        {
            candlesReady = false;
        }

        if (PPPressScript.pressurePlatesPressed == 3)
        {
            platesReady = true;
        }
        else
        {
            platesReady = false;
        }

        if (candlesReady && platesReady)
        {
            scriptEnabler.enabled = true;
        }
        else
        {
            scriptEnabler.enabled = false;
        }
    }
}
