using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{

	public static Inventory instance = null;

	public enum SlotTypes : int
	{
		WEAPON_LEFT = 0,
		WEAPON_RIGHT = 1,
		ARMOUR_HEAD = 2,
		ARMOUR_MOUTH = 3,
		ARMOUR_EYE = 4
	}
			
	// An array for the 5 different item slots
	private int[] itemSlot = new int[5];

	// A list for containing the player's inventory items
	// Each element will contain an index value that points to the item stored in the GameItems.gameItems[] array
	private List<int> itemList = new List<int>();

	// Return the number of elements in the inventory list
	public int ItemListCount{get{return itemList.Count;}}

	//array of number of items in stock/equipped. Array index is based on GameItems array, not InventoryItems array!
	private int[] numStock;
	private int[] numEquipped;

	// numStock/numEquipped Getter Methods
	public int NumStock(int index) {return numStock[index];}
	public int NumEquipped(int index) {return numEquipped[index];}


	void Awake()
	{
		// create a static instance of this class if null, otherwise destroy this gameObject 
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy (this.gameObject);
		}

		// Don't destroy this gameObject between scenes
		DontDestroyOnLoad(gameObject);

		// initialise the stock/equipped arrays to the size of the gameItems array
		numStock = new int[GameItems.gameItems.Length];
		numEquipped = new int[GameItems.gameItems.Length];

		// Load the previously saved inventory
		LoadInventory ();
	}

	// Use this for initialization
	void Start ()
	{
		
	}


	// Looks for an item in the inventory list based on the given game item's index location,
	// and returns the inventory list index to that item if found,  otherwise returns -1
	public int FindItem(int gameItemIndex)
	{

		for (int i = 0; i < itemList.Count; ++i)
		{
			if (gameItemIndex == itemList[i])
			{
				return i;
			}
		}

		return -1;
	}


	// Adds an item to the inventory list and sorts it, or increases it's stock by 1 if the item type is already in the inventory.
	public void AddItem(int gameItemIndex)
	{

		if (gameItemIndex < 0 || gameItemIndex >= GameItems.gameItems.Length)
		{
			return;
		}

		int inventoryListIndex = FindItem (gameItemIndex);
	
		if (inventoryListIndex == -1)
		{
			// Add new item to item list, and re-sort list
			itemList.Add (gameItemIndex);
			itemList.Sort ();
		}

		numStock[gameItemIndex]++;
			
	}


	// Reduces the item stock amount by 1, and removes the item from the inventory if stock is fully depleted.
	public void DepleteItem(int inventoryListIndex)
	{
		if (inventoryListIndex < 0 || inventoryListIndex >= itemList.Count)
		{
			return;
		}

		numStock[ itemList [inventoryListIndex] ]--;

		// Remove item if stock is fully depleted
		if (numStock[ itemList [inventoryListIndex] ] <= 0)
		{
			itemList.RemoveAt (inventoryListIndex);
		}
	}


	// Removes an item from the inventory, and re-sorts the list of inventory items
	public void RemoveItem(int inventoryListIndex)
	{
		if (inventoryListIndex < 0 || inventoryListIndex >= itemList.Count)
		{
			return;
		}

		itemList.RemoveAt (inventoryListIndex);

		itemList.Sort ();
	}

	// Gets the item location stored in the specified index of the inventory itemList array.
	// Returns the index value for the item that is contained in the GameItems.gameItems[] array.
	public int GetInventoryItem(int inventoryListIndex)
	{
		if (inventoryListIndex < 0 || inventoryListIndex >= itemList.Count)
		{
			return -1;
		}

		return itemList [inventoryListIndex];
	}
		

	// Gets the item location stored in the specified index of the itemSlot array.
	// Returns the index value for the item that is contained in the GameItems.gameItems[] array.
	public int GetSlotItem(SlotTypes slotType)
	{
		return itemSlot [(int)slotType];
	}


	// Equips an item from the inventory to the specified slot
	public void EquipSlot(SlotTypes slotType, int inventoryListIndex) 
	{
		if (inventoryListIndex < 0 || inventoryListIndex >= itemList.Count)
		{
			return;
		}

		// Get an item reference from the gameItems[] array
		InventoryItem item = GameItems.gameItems [itemList [inventoryListIndex]];

		// Exit the function if item is not an equippable type, if number of item equipped >= item stock,
		// or if the same item is already equipped in the specified slot
		if (item.ItemType == InventoryItem.ItemTypes.POTION ||
			item.ItemType == InventoryItem.ItemTypes.NONE ||
			numEquipped[itemList [inventoryListIndex]] >= numStock[itemList [inventoryListIndex]] || 
			itemSlot [(int)slotType] == itemList [inventoryListIndex])
		{
			return;
		}


		//Check whether the slot selected is a weapon slot (Hand)
		if (slotType == SlotTypes.WEAPON_LEFT || slotType == SlotTypes.WEAPON_RIGHT)
		{
			if (item.ItemType == InventoryItem.ItemTypes.WEAPON_ONE_HANDED)
			{
				// Unequip current slot and Equip slot with a new single handed weapon
				UnEquipSlot(slotType);
				itemSlot [(int)slotType] = itemList [inventoryListIndex];
				numEquipped[itemList [inventoryListIndex]]++;
			}
			else if (item.ItemType == InventoryItem.ItemTypes.WEAPON_TWO_HANDED)
			{
				// Unequip both hand slots if item is a 2 handed weapon,
				UnEquipSlot(SlotTypes.WEAPON_LEFT);
				UnEquipSlot(SlotTypes.WEAPON_RIGHT);

				// Equip both hands with the new weapon 2 handed weapon
				itemSlot [(int)SlotTypes.WEAPON_LEFT] = itemList [inventoryListIndex];
				itemSlot [(int)SlotTypes.WEAPON_RIGHT] = itemList [inventoryListIndex];

				numEquipped[itemList [inventoryListIndex]]++;
			}
		}
		else
		{
			// Equip the armour if it's compatable with the head/body/misc slot
			if (slotType == SlotTypes.ARMOUR_HEAD && item.ItemType == InventoryItem.ItemTypes.ARMOUR_HEAD)
			{
				UnEquipSlot(slotType);
				itemSlot [(int)slotType] = itemList [inventoryListIndex];
				numEquipped[itemList [inventoryListIndex]]++;
			}
			else if (slotType == SlotTypes.ARMOUR_MOUTH && item.ItemType == InventoryItem.ItemTypes.ARMOUR_MOUTH)
			{
				UnEquipSlot(slotType);
				itemSlot [(int)slotType] = itemList [inventoryListIndex];
				numEquipped[itemList [inventoryListIndex]]++;
			}
			else if (slotType == SlotTypes.ARMOUR_EYE && item.ItemType == InventoryItem.ItemTypes.ARMOUR_EYE)
			{
				UnEquipSlot(slotType);
				itemSlot [(int)slotType] = itemList [inventoryListIndex];
				numEquipped[itemList [inventoryListIndex]]++;
			}
		}

	}


	// Removes the item from the specified slot.
	public void UnEquipSlot(SlotTypes slotType)
	{

		// if current slot item != None
		if (itemSlot [(int)slotType] != 0)
		{
			// decrement the equipped number
			numEquipped[itemSlot [(int)slotType]]--;

			// Unequip both hand slots if item is a 2 handed weapon, else unequip specified slot
			if (GameItems.gameItems[itemSlot [(int)slotType]].ItemType == InventoryItem.ItemTypes.WEAPON_TWO_HANDED)
			{
				// Set both hand slots to item "none"
				itemSlot [(int)SlotTypes.WEAPON_LEFT] = 0;
				itemSlot [(int)SlotTypes.WEAPON_RIGHT] = 0;
			}
			else
			{
				// Set selected slot to item "none"
				itemSlot [(int)slotType] = 0;
			}
		}
	}

	// Load previously saved inventory/slot data
	public void LoadInventory()
	{
		int inventoryCount = 0;

		//Check how many inventory item elements that were saved
		if(PlayerPrefs.HasKey("InventoryCount") == true)
		{
			inventoryCount = PlayerPrefs.GetInt ("InventoryCount");
		}

		// Load the saved inventory items and add them back to the item list
		for (int i = 0; i < inventoryCount; ++i)
		{
			int gameItemIndex = PlayerPrefs.GetInt ("ItemType" + i);

			AddItem (gameItemIndex);
		}

		//Load the array of inventory stock values
		for (int i = 0; i < numStock.Length; ++i)
		{
			numStock[i] = PlayerPrefs.GetInt ("ItemStock" + i);
		}

		// Load slot data and re-equip the items
		for (int i = 0; i < itemSlot.Length; ++i)
		{
			int gameItemIndex = PlayerPrefs.GetInt ("SlotItem" + i);

			EquipSlot ((SlotTypes)i, FindItem(gameItemIndex));
		}

	}

	public void SaveInventory(bool saveToDisk = false)
	{

		//Save how many inventory items there are in the item list
		PlayerPrefs.SetInt ("InventoryCount", itemList.Count);

		//Save the list of inventory items
		for (int i = 0; i < itemList.Count; ++i)
		{
			PlayerPrefs.SetInt ("ItemType" + i, itemList [i]);
		}

		//Save the array of inventory stock values
		for (int i = 0; i < numStock.Length; ++i)
		{
			PlayerPrefs.SetInt ("ItemStock" + i, numStock [i]);
		}

		//Save the array of slot items
		for (int i = 0; i < itemSlot.Length; ++i)
		{
			PlayerPrefs.SetInt ("SlotItem" + i, itemSlot [i]);
		}

		//If true, the data will be saved permanently to the disk, rather than in memory
		if (saveToDisk == true)
		{
			PlayerPrefs.Save ();
		}

	}

    public InventoryItem GetItemInfo(int ItemIndex)
    {
        if (ItemIndex >= 0 && ItemIndex < numStock.Length)
        {
            return GameItems.gameItems[ItemIndex];
        }
        else
        {
            return GameItems.gameItems[0];
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
