﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleManager : MonoBehaviour {

    [SerializeField]
    GameObject playerRock;
    [SerializeField]
    GameObject enemyRock;

    [SerializeField]
    Text leftSideText;
    [SerializeField]
    Text rightSideText;

    //Variables to determine Win/Lose/Tie 
    private bool GameOn;
    private bool hasWon;
    private bool hasLost;
    private bool isTie;

    // Use this for initialization
    void Start ()
    {
        //Battle Scene Starts and all variables are initialised
        GameOn = true;
        hasWon = false;
        hasLost = false;
        isTie = false;
         
        playerRock.GetComponent<Battle_Rock_Player>().Start();
        enemyRock.GetComponent<Battle_Rock_Enemy>().Start();

        _MusicManager.FindObjectOfType<_MusicManager>().ChangeMusic(_MusicManager.TypesOfMusic.BATTLE_SCENE);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameOn == false)
        {
            BattleResults();
        }
	}

  

    //When the Battle button is clicked, this Function is called
    public void BeginFight()
    {

        if (GameOn == false)
        {
            return;
        }
        //Checks for Win/Lose/Tie, if none, Initiate Combat
        playerRock.GetComponent<Battle_Rock_Player>().Combat();
        enemyRock.GetComponent<Battle_Rock_Enemy>().Combat();

        playerRock.GetComponent<Battle_Rock_Player>().ResolveCombat();
        enemyRock.GetComponent<Battle_Rock_Enemy>().ResolveCombat();
        
        //If the Player has below 0 Health
        if (playerRock.GetComponent<Battle_Rock_Player>().CurrentHealth <= 0 && enemyRock.GetComponent<Battle_Rock_Enemy>().CurrentHealth > 0)
        {
            hasLost = true;
            GameOn = false;
        }
        //If the Enemy is below 0 Health
        else if (enemyRock.GetComponent<Battle_Rock_Enemy>().CurrentHealth <= 0 && playerRock.GetComponent<Battle_Rock_Player>().CurrentHealth > 0)
        {
            hasWon = true;
            GameOn = false;
        }
        //If both Enemy and Player reached 0 health or below at the same time
        else if(playerRock.GetComponent<Battle_Rock_Player>().CurrentHealth <= 0 && enemyRock.GetComponent<Battle_Rock_Enemy>().CurrentHealth <= 0)
        {
            isTie = true;
            GameOn = false;
        }
    }

    //Displays Text on Screen depending on the Winner
    //Changes Scene
    public void BattleResults()
    {
        if (hasWon == true)
        {
            leftSideText.text = "Winner!";
            rightSideText.text = "Loser!";

        }
        else if (hasLost == true)
        {
            leftSideText.text = "Loser!";
            rightSideText.text = "Winner!";
        }
        else if (isTie == true)
        {
            leftSideText.text = "Tie!";
            rightSideText.text = "Tie!";
        }

        
        GameObject.Find("FadeScreen").GetComponent<SceneFadeInAndOut>().LoadNewScene("MainMenu");
    }

    public void ChooseSpecial()
    {

    }

    public void ChooseDefense()
    {

    }

    //Takes user back to Main Menu
    public void ChooseForfeit()
    {
        hasLost = true;
        GameOn = false;
        
    }
}
