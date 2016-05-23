using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Battle_Rock_Player : Base_Rock {

    [SerializeField]
    public GameObject enemyRock;

    public Text playerHealthText;

	public float SlotDefence{ get { return slotDefence; }}
	public float SlotSpeed { get { return slotSpeed; }}

    // Use this for initialization
    public void Start ()
    {
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

	
	// Update is called once per frame
	void Update ()
    {
        
    }

//Calculates total damage to enemy based on Stats
public void Combat()
    {
        System.Random battleRandomizer = new System.Random();
        float battleRan = battleRandomizer.Next(-10, 10);

		finalDamageOutput = (attack - enemyRock.GetComponent<Battle_Rock_Enemy>().Defense) * 
			(speed / enemyRock.GetComponent<Battle_Rock_Enemy>().Speed) + luck + battleRan;
    }

    //Subtracts Enemies Total Damage from current health
    public void ResolveCombat()
    {
        this.CurrentHealth -= enemyRock.GetComponent<Battle_Rock_Enemy>().Damage;
    }

	private void GetSlotStatTotals()
	{
		slotAttack = 0;
		slotDefence = 0;
		slotSpeed =  0;
		slotLuck = 0;
		slotHealth = 0;

		for(int i = 0; i < 5; ++i)
		{
			InventoryItem currentSlot = GameItems.gameItems[Inventory.instance.GetSlotItem ((Inventory.SlotTypes)i)];

			slotAttack += currentSlot.Attack;
			slotDefence += currentSlot.Defence;
			slotSpeed += currentSlot.Speed;
			slotLuck += currentSlot.Luck;
			slotHealth += currentSlot.Health;
		}
	}
}
