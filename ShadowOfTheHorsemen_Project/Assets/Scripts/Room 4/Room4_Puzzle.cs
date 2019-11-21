using UnityEngine;

public class Room4_Puzzle : MonoBehaviour
{
    //basicly handles room 4 in 103 
    //if the player manages to kill the enemy with their own fireballs
    //then the enemy and launchers stop so that the doorscript gets enabled

    public GameObject enemy;
    public OpenDoor scriptEnabler;
    public FireballLauncherScript launcherOne;
    public FireballLauncherScript launcherTwo;

	void Update ()
    {
        if (enemy.activeSelf)
        {
            scriptEnabler.enabled = false;
            launcherOne.enabled = true;
            launcherTwo.enabled = true;
        }
        else
        {
            scriptEnabler.enabled = true;
            launcherOne.enabled = false;
            launcherTwo.enabled = false;
        }
    }
}