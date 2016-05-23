using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Battle_Rock_Enemy : Base_Rock {

    [SerializeField]
    public GameObject playerRock;

    public Text enemyHealthText;

    private int enemyLevel = 1;


    // Use this for initialization
    public void Start ()
    {

        currencyWorth = 200;

        GetSlotStatTotals();

        maxHealth = 50 + slotHealth;
        currentHealth = maxHealth;
        attack = 15 + slotAttack;
        defense = 5 + slotDefence;
        speed = 2 + slotSpeed;
        luck = 1 + slotLuck;

        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    void Update()
    {
        
    }


    public void Combat()
    {
        System.Random battleRandomizer = new System.Random();
        float battleRan = battleRandomizer.Next(-10, 10);

		finalDamageOutput = (attack - (playerRock.GetComponent<Battle_Rock_Player>().Defense + playerRock.GetComponent<Battle_Rock_Player>().SlotDefence)) *
			(speed / (playerRock.GetComponent<Battle_Rock_Player>().Speed + playerRock.GetComponent<Battle_Rock_Player>().SlotSpeed)) + luck + battleRan;

    }

    public void ResolveCombat()
    {
        this.CurrentHealth -= playerRock.GetComponent<Battle_Rock_Player>().Damage;
    }

    private void GetSlotStatTotals()
    {
        

        //Weapon Left
        slotAttack =+ GameItems.gameItems[2].Attack;
        slotDefence =+ GameItems.gameItems[2].Defence;
        slotSpeed =+ GameItems.gameItems[2].Speed;
        slotLuck =+ GameItems.gameItems[2].Luck;
        slotHealth =+ GameItems.gameItems[2].Health;

        //Weapon Right

        //Eyes
        slotAttack = +GameItems.gameItems[25].Attack;
        slotDefence = +GameItems.gameItems[25].Defence;
        slotSpeed = +GameItems.gameItems[25].Speed;
        slotLuck = +GameItems.gameItems[25].Luck;
        slotHealth = +GameItems.gameItems[25].Health;

        //Mouth
        slotAttack = +GameItems.gameItems[20].Attack;
        slotDefence = +GameItems.gameItems[20].Defence;
        slotSpeed = +GameItems.gameItems[20].Speed;
        slotLuck = +GameItems.gameItems[20].Luck;
        slotHealth = +GameItems.gameItems[20].Health;

        //Head
        

    }
}
