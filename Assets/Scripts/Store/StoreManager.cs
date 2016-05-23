using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour {

    public ItemButton StoreIconPrefab;

    public Sprite[] ButtonSprites;

    public InventoryItem[] StoreItems;
    public int[] StoreItemsIndex;

    public struct StoreObjectsStruct
    {
        public int StoreItemIndex;
        public string StoreItemImage;
    };

    public StoreObjectsStruct[] StoreObjects;

    // Use this for initialization
    void Start ()
    {
        
        StoreObjects = new StoreObjectsStruct[ButtonSprites.Length];
        StoreItemsIndex = new int[ButtonSprites.Length];

        // This function initializes the array of Store Buttons 
        for (int i = 0; i < ButtonSprites.Length; i++)
        {
            GameObject Temp = Instantiate(StoreIconPrefab.gameObject, transform.position, transform.rotation) as GameObject;
            Temp.transform.SetParent(transform);

            ItemButton IbTemp = Temp.GetComponent<ItemButton>();

            IbTemp.Init(ButtonSprites[i], StoreObjects[i].StoreItemImage, i, Inventory.instance.GetItemInfo(i));
            StoreItemsIndex[i] = i;

            float m = GameItems.gameItems[i].Attack;
        }

        // This statement sets the width of the display port based on the number of objects
        // Tests for odd number of objects and autoadjusts size to compensate
        RectTransform TempRect =  GetComponent<RectTransform>();

        if (ButtonSprites.Length % 2 == 0)  
        {
            TempRect.sizeDelta = new Vector2((ButtonSprites[0].rect.width) * (ButtonSprites.Length / 2), TempRect.rect.height);
        }
        else
        {
            TempRect.sizeDelta = new Vector2((ButtonSprites[0].rect.width) * ((ButtonSprites.Length + 1) / 2), TempRect.rect.height);
        }


        GridLayoutGroup TempGrid = GetComponent<GridLayoutGroup>();
        TempGrid.cellSize = new Vector2((float)(ButtonSprites[0].rect.width*0.6), (float)(ButtonSprites[0].rect.height*0.6));


    }

	// Update is called once per frame
	void Update () {
	
	}

    public void BuyItem()
    {
        int ItemIndex;
        string ItemIndexAsString = transform.parent.Find("InfoPanel").Find("ItemID").GetComponent<Text>().text;
        Int32.TryParse(transform.parent.Find("InfoPanel").Find("ItemID").GetComponent<Text>().text, out ItemIndex);
        Text SuccessText = transform.parent.Find("FadePanelConfirmation").Find("FadePanelBoughtOrNot").Find("BoughtItemPanel").Find("BuySuccess").Find("Dialogue").GetComponent<Text>();
        string Tester = transform.parent.Find("InfoPanel").Find("Name").GetComponent<Text>().text;

        if (_GameManager.instance.cashCurrency < GameItems.gameItems[ItemIndex].ItemCost)
        {
            SuccessText.text = "Oops!\n" + "You do not have enough Money!";
        }
        else
        {
            
            SuccessText.text = "Congratulations!\n" + "You bought a: \n" + Tester;
            Inventory.instance.AddItem(ItemIndex);
            _GameManager.instance.cashCurrency -= GameItems.gameItems[ItemIndex].ItemCost;
            Inventory.instance.SaveInventory();
        }
    }

}
