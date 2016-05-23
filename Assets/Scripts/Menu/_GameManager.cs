// Author: Michael Stojsic 2016.
// NOTE: - Attach this script to the 'Game Manager' in the first loaded scene, it will then live throughout each scene from there.
//       - For debugging you can place a Gamemanger in every scene but note any changes made to the non _instance scene GameManagers MUST be updated to the _instance one in the first scene,
//         as the first scenes GameManager will be the _instance when the player plays the game and will delete any non _instance GameManagers if comes across.
//       - remember to set execution order, go to unity (edit -> Project Settings -> Script Execution Order -> Set GameManger to -100)!!!
//         so GameManager runs a little earlier than all other scripts in the game, 
//         otherwise values taken from _GameManager.instance may not be initialised yet and result in incorrect values used.
//       - This script will manage any variables used between multiple scenes like lives, money, playerNames.
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class _GameManager : MonoBehaviour
{
    // ---------------------------- CREATE AS INSTANCE ---------------------------- \\
    // - This section checks if a current _instance of GameManager exists, if it does it returns the _instance,
    //   otherwise it creates a new GameManger in the current scene called "GameManager" 
    //   sets it to the _instance of Gamamanger and adds this _GameManager script to it, 
    //   essentailly making it the _instance.
    private static _GameManager _instance;                                  // - Internal GameManager reference.
    public static _GameManager instance                                     // - External GameManager reference for all other scenes.
    #region GameManager instance.       
    {
        get
        {
            if(_instance != null)                                           // - If _instance not null.
            {
                return _instance;                                           // - Return current _instance.
            }
            else                                                            // - Else (if _instance not null).
            {
                GameObject gameManager = new GameObject("GameManager");     // - Set gameMnager as a new GameObject called GameManager.
                _instance = gameManager.AddComponent<_GameManager>();       // - Add GameManager script to that new GameObject _instance.
                return _instance;                                           // - Return that new instance of GameManager as _instance.
            }
        }
    }
    #endregion  
    // ---------------------------- VARIABLES ---------------------------- \\
    // - THis is where we store the Variables that are needed game wide, 
    //   being in the GameManager allows them to live for the entire life of the current play session.
    // Player Variables.
    public string playerName;
    public int currentLives;
    public int cashCurrency;
    public string lastLevelPlayed;                                // - Stores last level played.  

    public int noUnlockedLevels;

    // Audio Variables. 
    public bool soundOn = true;
    public bool musicOn = true;

    // Unlocked Levels Variables 
    public int[] unlockedLevels = new int[]                     // - Stores which levels are currently unlocked. 1 = unlocked, 0 = locked. used ints rathen than bool as easier to save and load imo.
    {
        0,  // [0] Currently unused to keep code readable and understandable.
        1,  // [1] level 1
        0,  // [2] level 2
        0,  // [3] level 3
        0,  // [4] level 4
        0,  // [5] level 5
        0,  // [6] level 6
        0,  // [7] level 7 
        0,  // [8] level 8
        0,  // [9] level 9
        0  // [10] level 10

        // NOTE: ADD NEW INDEXES AS WE ADD LEVELS.
    };
        
    // ---------------------------- START ----------------------------
    void Awake () 
    {
        // Set _instance as this script.
        if(_instance != this)                                   // - If _instance isnt this GameManager.
        {
            if(_instance == null)                               // - If no _instance of GameManager.
            {
                _instance = this;                               // - Set _instance to this GameManager.
            }
            else                                                // - If one already exists.
            {
                Destroy(gameObject);                            // - Destory this extra one.
            }
        }
        LoadGame();                                             // - Call LoadGame to Load Save Data.
        DontDestroyOnLoad(gameObject);                          // - Prevents this object being destoried between scenes.

        // For Testing
        playerName = "Michael";                                 
        currentLives = 10;

        noUnlockedLevels = 1;

    }
    // ---------------------------- UPDATE ----------------------------
    void Update () 
    {
    // NOTE: TEMP WAY TO SAVE GAME FOR NOW! 
    //       DELETE THIS FUNCTION LATER!
        if(Input.GetKeyDown(KeyCode.S))                         // - If 'S' key is pressed down.
        {
            SaveGame();                                         // - Call SaveGame to save data.
        }
    }
    // ---------------------------- SAVE GAME ----------------------------
    // - Saves all important variables to playerPrefs.  Not accesable directly by outside scripts.
    void SaveGame()
    {
        // Save Current Player Variables.
        PlayerPrefs.SetString("Player Name", playerName);                                           // - Set playerPrefs Player Name to playerName as a string.
        PlayerPrefs.SetInt("CurrentLives", currentLives);                                           // - Set playerPrefs Current Lives to currentLives as an int.
        PlayerPrefs.SetInt("Cash Currency", cashCurrency);                                          // - Set playerPrefs Cash Currency to cashCurrency as an int.
        PlayerPrefs.SetString("Last Level Played", lastLevelPlayed);                                // - Set playerPrefs Last Level Played to lastLevelplayed as a string.
        // Save Current Unlocked Levels.
        for (int i = 0; i < unlockedLevels.Length; i++)                                             // - For loop through unlockedLevels.
        {
            PlayerPrefs.SetInt("Unlocked Levels" + i.ToString(), unlockedLevels[i]);                // - Set playerPrefs UnlockedLevel 'i' as unlockedLevels index 'i'.
        }
    }
    // ---------------------------- LOAD GAME ----------------------------
    // - Loads all important variables from playerPrefs. Not accesable directly by outside scripts.
    void LoadGame()
    {
        // Load Saved Player Variables.
        playerName = PlayerPrefs.GetString("Player Name");                                          // - Set playerName to playerPrefs Player Name as a string.
        currentLives = PlayerPrefs.GetInt("CurrentLives");                                          // - Set currentLives to playerPrefs Current Lives as an int.
        cashCurrency = PlayerPrefs.GetInt("Cash Currency");                                         // - Set cashCurrency to playerPrefs Cash Currency as an int.
        lastLevelPlayed = PlayerPrefs.GetString("Last Level Played");                               // - Set lastLevelplayed to playerPrefs Last Level Played as a string.
        // Load Saved Unlocked Levels.
        for (int i = 0; i < unlockedLevels.Length; i++)                                             // - For loop through unlockedLevels.
        {
            unlockedLevels[i] = PlayerPrefs.GetInt("Unlocked Levels" + i.ToString());               // - Set unlockedLevels 'i' as playerPrefs Unlocked Levels index 'i'.
        }
    }
    // ---------------------------- DELETE SAVE DATA ----------------------------
    // - Deletes all important variables from playerPrefs and sets the variables values to defaults 
    //   then saves over them again is playerPrefs.
    public void DeleteSaveData()
    {
        PlayerPrefs.DeleteAll();                                    // - Deletes all playerPrefs.
        playerName = " ";                                           // - Set playerName to " " so its empty as its a new game.
        currentLives = 3;                                           // - Set currentLives to 3 as its a new game.
        cashCurrency = 0;                                           // - Set cashCurrency to 0 as its a new game.
        lastLevelPlayed = " "; //"first levels name here";          // - Set lastlevelPlayed to 1st level in the game so new game button runs correct level on new game.
        for(int i = 0; i < unlockedLevels.Length; i++)              // - For loop through unlockedLevels.
        {
            unlockedLevels[i]=0;                                    // - Reset each index of unlocked levels to 0 to re-lock them all.
        }      
        unlockedLevels[1] = 1;                                      // - Set index 1 of unlockedlevels to 1, so first level of game is unlocked when we clear our save.

        SaveGame();                                                 // - Call SaveGame to save new values as the saved values.
        SceneManager.LoadScene("MenuMain");                         // - Load up MainMenu scene.
    }   
    // ---------------------------- RESET GAME ----------------------------
    // - This will be called when the level is reset to reset all the values
    //   for each level back to a clean default for the next run of it.
    public void ResetGame()
    {
        // NOTE: ADD - add any variable that need to be reset at end of level
    }

    // This Function handles the deletion of the life internally to the game manager and will return True if Successful, otherwise will return false, indicating
    // that the player has run out of lives
    public bool LoseLife()  
    {
        if (currentLives > 0)
        {
            currentLives--;
            return true;
        }
        else
        {
            currentLives--;
            return false;
        }
    }
}
