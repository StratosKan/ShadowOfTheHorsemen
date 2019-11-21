using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveManager : MonoBehaviour
{
    //jimmos favourite script this will be used alot in the future it basicly stores simple variables in a serialized format on a location to the disk
    //not with playerPrefs but with proper serialization to a binary file
    //is very good no bad grade
    //currently saves when the player comes back from a room so at sceneIndex 0,2,4,6

    public static SaveManager manager;

    public int gameState; //there are 8 game states for each key obtained/not obtained
    public float timeSaveIsShown = 3f;

    private float startingTimeToShowSave;
    private bool loaded = false;
    private int sceneIndex = 0;
    private Scene currentScene;
    private bool savedFlag = false;

    void Awake () //Singleton
    {
        startingTimeToShowSave = timeSaveIsShown;

        GetScene();
        if (currentScene.buildIndex != 0) //Don't want to have on Main Menu scene
        {
            if (manager == null)
            {
                DontDestroyOnLoad(gameObject);
                manager = this;
            }
            else if (manager != this)
            {
                Destroy(gameObject);
            }
        }
	}

    void OnEnable() //Detects a scene change
    {
        Time.timeScale = 1.0f;
        SceneManager.sceneLoaded += SceneLoaded;
    }

    void FixedUpdate()
    {
        GetScene();

        if (savedFlag)
        {
            TimerStart();
        }

        if (loaded)
        {
            loaded = false;
            gameState = sceneIndex;

            switch (sceneIndex) //Depending on sceneIndex saves happen and keys are handled
            {
                /*                  ==== READ ME ====
                 * PlayerInventory slots 0 - 4 are slots only for the ORBS
                 * If adding/messing with slots please leave 0 - 4 always unused by puzzles              
                 */
                case 0:
                    Time.timeScale = 1.0f;
                    gameState = 0;
                    break;
                case 1:
                    gameState = 1;
                    PlayerInventory.playerInventory.ClearKeys();
                    Save(); //Also effectively clears the save file
                    savedFlag = true;
                    break;
                case 2:
                    gameState = 2;
                    PlayerInventory.playerInventory.ClearKeys();
                    PlayerInventory.playerInventory.AddKey("Room 1 Key", 0);
                    break;
                case 3:
                    gameState = 3;
                    PlayerInventory.playerInventory.ClearKeys();
                    PlayerInventory.playerInventory.AddKey("Room 1 Key", 0);
                    PlayerInventory.playerInventory.AddKey("Room 2 Key", 1);
                    Save(); //save upon entering the main room with Water orb
                    savedFlag = true;
                    break;
                case 4:
                    gameState = 4;
                    PlayerInventory.playerInventory.AddKey("Room 1 Key", 0);
                    PlayerInventory.playerInventory.AddKey("Room 2 Key", 1);
                    break;
                case 5:
                    gameState = 5;
                    PPPressScript.pressurePlatesPressed = 0; //Resets static variable for that room's Puzzle
                    PlayerInventory.playerInventory.ClearKeys();
                    PlayerInventory.playerInventory.AddKey("Room 1 Key", 0);
                    PlayerInventory.playerInventory.AddKey("Room 2 Key", 1);
                    PlayerInventory.playerInventory.AddKey("Room 3 Key", 2);
                    Save(); //save upon entering the main room with Earth orb
                    savedFlag = true;
                    break;
                case 6:
                    gameState = 6;
                    PlayerInventory.playerInventory.AddKey("Room 1 Key", 0);
                    PlayerInventory.playerInventory.AddKey("Room 2 Key", 1);
                    PlayerInventory.playerInventory.AddKey("Room 3 Key", 2);
                    break;
                case 7:
                    gameState = 7;
                    PlayerInventory.playerInventory.ClearKeys();
                    PlayerInventory.playerInventory.AddKey("Room 1 Key", 0);
                    PlayerInventory.playerInventory.AddKey("Room 2 Key", 1);
                    PlayerInventory.playerInventory.AddKey("Room 3 Key", 2);
                    PlayerInventory.playerInventory.AddKey("Room 4 Key", 3);
                    Save(); //save upon entering the main room with Air orb
                    savedFlag = true;
                    break;
            }
        }
    }

    void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode) //Passes boolean that scene changed
    {
        loaded = true;
    }

    public void GetScene()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneIndex = currentScene.buildIndex;
    }

    void OnGUI()
    {
        if(savedFlag)
        {
            GUI.Box(new Rect(Screen.width - 130, Screen.height - 50, 100, 20)," ");
            GUI.Label(new Rect(Screen.width - 100, Screen.height - 50, 100, 50), "Saved");
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savegame.save");

        PlayerData data = new PlayerData();
        data.gameState = gameState;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/savegame.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savegame.save",FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            gameState = data.gameState;
            SceneManager.LoadScene(gameState);
        }
    }

    public void Clear()
    {
        if (File.Exists(Application.persistentDataPath + "/savegame.save"))
        {
            File.Delete(Application.persistentDataPath + "/savegame.save");
        }
    }

    [Serializable]
    class PlayerData
    {
        public int gameState;
    }


    //For showing save message
    public void TimerStart()
    {
        timeSaveIsShown -= Time.fixedDeltaTime;
        if (timeSaveIsShown <= 0f)
        {
            TimerEnded();
        }
    }

    public void TimerEnded()
    {
        savedFlag = false;
        timeSaveIsShown = startingTimeToShowSave;
    }
}
