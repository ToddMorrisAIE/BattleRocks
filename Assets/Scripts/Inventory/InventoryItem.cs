    using System;
using System.Collections;


public class InventoryItem : System.Object
{
	// Enum of available item types
	public enum ItemTypes
	{
		NONE,
		POTION,
		WEAPON_ONE_HANDED,
		WEAPON_TWO_HANDED,
		ARMOUR_HEAD,
		ARMOUR_MOUTH,
		ARMOUR_EYE
	}
		
	// Name of this item
	protected string itemName;

	// Description of this item
	protected string itemDescription;

	// Description of this item
	protected string itemSprite;

	// What type of item this is (Potion/Weapon/Armour)
	protected ItemTypes itemType;

	// Store Purchase value of the item
	protected int itemCost;

	// Stat values for the item
	protected float attack, defence, speed, luck, health;


	// Read only properties for the above variables
	public string ItemName{ get { return itemName; } }
	public string ItemDescription{ get { return itemDescription; } }
	public String ItemSprite{ get { return itemSprite; } }
	public ItemTypes ItemType{ get {return itemType; } }
	public int ItemCost{ get { return itemCost; } }

	public float Attack{ get { return attack; } }
	public float Defence{ get { return defence; } }
	public float Speed{ get { return speed; } }
	public float Luck{ get { return luck; } }
	public float Health{ get { return health; } }


	// Item constructor
	public InventoryItem (string name, string description,string sprite, ItemTypes type, int cost,
		float attack, float defence, float speed, float luck, float health)
	{
		this.itemName = name;
		this.itemDescription = description;
		this.itemSprite = sprite;
		this.itemType = type;
		this.itemCost = cost;

		this.attack = attack;
		this.defence = defence;
		this.speed = speed;
		this.luck = luck;
		this.health = health;
	}


	// To use this item's special ability
	public virtual void SpecialAbility()
	{
		// Implement special ability here...
	}
}