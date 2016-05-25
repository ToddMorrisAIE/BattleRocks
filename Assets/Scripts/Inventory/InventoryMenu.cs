using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

using UnityEngine.SceneManagement;

public class InventoryMenu : MonoBehaviour
{

	[SerializeField]
	private ToggleList slotMenu = null;

	[SerializeField]
	private ToggleList itemMenu = null;

	[SerializeField]
	private Text itemDescription = null;

	[SerializeField]
	private Text itemStats = null;


	private Inventory inventoryRef = null;


	// Use this for initialization
	void Start () 
	{
		inventoryRef = Inventory.instance;

		// For Testing only. If inventory is empty, Add some test items and save inventory
		if (inventoryRef.ItemListCount == 0)
		{
			for(int i = 1; i < GameItems.gameItems.Length; ++i)
			{
				inventoryRef.AddItem (i);
			}

			inventoryRef.SaveInventory (true);
		}

		LoadItemMenu ();

		OnItemSelect ();
		RefreshSlotText();
	}
		
	void LoadItemMenu()
	{
		GameObject toggleFirst = Resources.Load("Prefabs/UI_Toggle_Button") as GameObject;

		toggleFirst.GetComponentInChildren<Text> ().text = GameItems.gameItems[inventoryRef.GetInventoryItem (0)].ItemName;

		for (int i = 0; i < inventoryRef.ItemListCount; ++i)
		{
			GameObject toggleObject = Instantiate (toggleFirst);
			RectTransform toggleRect = toggleObject.transform as RectTransform;

			toggleObject.transform.SetParent (itemMenu.gameObject.transform, false);

			toggleRect.anchoredPosition += new Vector2 (0, -(i*25));

			toggleObject.GetComponentInChildren<Text> ().text = GameItems.gameItems[inventoryRef.GetInventoryItem (i)].ItemName;

			itemMenu.toggleButtons.Add (toggleObject.GetComponent<Toggle>());
		}
			
		itemMenu.ToggleInit ();
	}

	private string UpdateStats(InventoryItem item)
	{
		string text = "";

		if (inventoryRef.GetInventoryItem (itemMenu.SelectedToggle) != 0)
		{
			text += "Stock : " + inventoryRef.NumStock (inventoryRef.GetInventoryItem (itemMenu.SelectedToggle)) +
			"\t|\tEquipped : " + inventoryRef.NumEquipped (inventoryRef.GetInventoryItem (itemMenu.SelectedToggle)) + "\n";

			text += "\nAttack : \t\t+" + item.Attack;
			text += "\nDefence : \t\t+" + item.Defence;
			text += "\nSpeed : \t\t+" + item.Speed;
			text += "\nLuck : \t\t\t+" + item.Luck;
			text += "\nHealth : \t\t+" + item.Health;
		}

		return text;
	}

	public void OnItemSelect()
	{
		//Show selected Item stats on screen

		if (itemMenu.SelectedToggle < inventoryRef.ItemListCount && inventoryRef.GetInventoryItem (itemMenu.SelectedToggle) != 0)
		{
			InventoryItem item = GameItems.gameItems[inventoryRef.GetInventoryItem (itemMenu.SelectedToggle)];

			itemDescription.text = item.ItemDescription + "\n\n[" + item.ItemType.ToString ().ToLower() + "]";
		}
		else
		{
			itemDescription.text = "";
		}

		itemStats.text = UpdateStats (GameItems.gameItems[inventoryRef.GetInventoryItem(itemMenu.SelectedToggle)]);
	}

	public void RefreshSlotText()
	{

		for (int i = 0; i < slotMenu.toggleButtons.Count; ++i)
		{
			InventoryItem item = GameItems.gameItems[inventoryRef.GetSlotItem ((Inventory.SlotTypes)i)];

			if(item != null)
			{
				slotMenu.toggleButtons[i].gameObject.GetComponentInChildren<Text> ().text = (Inventory.SlotTypes)i + "\n" + " > " + item.ItemName;
			}
			else
			{
				slotMenu.toggleButtons[i].gameObject.GetComponentInChildren<Text> ().text = (Inventory.SlotTypes)i + "\n" + " > None";
			}

		}
			
	}

	public void OnEquipButtonPress()
	{
		inventoryRef.EquipSlot ((Inventory.SlotTypes)slotMenu.SelectedToggle, itemMenu.SelectedToggle);

		RefreshSlotText ();
	}

	public void OnUnEquipButtonPress()
	{
		inventoryRef.UnEquipSlot ((Inventory.SlotTypes)slotMenu.SelectedToggle);
			
		RefreshSlotText ();
	}

	public void OnMenuExit()
	{
		inventoryRef.SaveInventory (true);

		SceneManager.LoadScene("OptionsMenu");
    }

	// Update is called once per frame
	void Update () 
	{

	}

}
