using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ItemButton : MonoBehaviour {

    Image ItemImage;
    Button SelfButton;
    private int ItemIdentifier;
    InventoryItem MyItemInfo;
    private string ItemTypeDescription;
    private Vector2 SpritePopupSize;

    public void Init(Sprite Item, string StoreItemImage, int ItemNumber, InventoryItem MyInfo)
    {
        if(ItemImage == null)
        {
            ItemImage = GetComponent<Image>();
        }

        if(SelfButton == null)
        {
            SelfButton = GetComponent<Button>();
        }

        MyItemInfo = MyInfo;
        ItemIdentifier = ItemNumber;
        ItemImage.sprite = Item;
        SelfButton.onClick.AddListener(delegate { InformationPopup(); });
        SpritePopupSize = new Vector2(Item.rect.width, Item.rect.height);

        switch (MyItemInfo.ItemType)
        {
            case InventoryItem.ItemTypes.ARMOUR_EYE:
                ItemTypeDescription = "Eye Armour";
                break;

            case InventoryItem.ItemTypes.ARMOUR_HEAD:
                ItemTypeDescription = "Head Armour";
                break;

            case InventoryItem.ItemTypes.ARMOUR_MOUTH:
                ItemTypeDescription = "Mouth Armour";
                break;

            case InventoryItem.ItemTypes.NONE:
                ItemTypeDescription = "Blank Button";
                break;

            case InventoryItem.ItemTypes.POTION:
                ItemTypeDescription = "Potion";
                break;

            case InventoryItem.ItemTypes.WEAPON_ONE_HANDED:
                ItemTypeDescription = "1H Weapon";
                break;

            case InventoryItem.ItemTypes.WEAPON_TWO_HANDED:
                ItemTypeDescription = "2H Weapon";
                break;

        };
    }

    public void InformationPopup()
    {
        Image TempImage = this.transform.parent.parent.Find("InfoPanel").Find("ItemSprite").GetComponent<Image>();
        TempImage.sprite = ItemImage.sprite;
        RectTransform SpriteSize = this.transform.parent.parent.Find("InfoPanel").Find("ItemSprite").GetComponent<RectTransform>();
        SpriteSize.sizeDelta = SpritePopupSize;
                
        this.transform.parent.parent.Find("InfoPanel").Find("Name").GetComponent<Text>().text = MyItemInfo.ItemName;
        this.transform.parent.parent.Find("InfoPanel").Find("Description").GetComponent<Text>().text = MyItemInfo.ItemDescription;
        this.transform.parent.parent.Find("InfoPanel").Find("Type").GetComponent<Text>().text = ItemTypeDescription;
        this.transform.parent.parent.Find("InfoPanel").Find("Cost").GetComponent<Text>().text = "Game Dollars: " + MyItemInfo.ItemCost.ToString();
        this.transform.parent.parent.Find("InfoPanel").Find("Attack").GetComponent<Text>().text = "Attack: " + MyItemInfo.Attack.ToString();
        this.transform.parent.parent.Find("InfoPanel").Find("Defence").GetComponent<Text>().text = "Defence: " + MyItemInfo.Defence.ToString();
        this.transform.parent.parent.Find("InfoPanel").Find("Speed").GetComponent<Text>().text = "Speed: " + MyItemInfo.Speed.ToString();
        this.transform.parent.parent.Find("InfoPanel").Find("Luck").GetComponent<Text>().text = "Luck: " + MyItemInfo.Luck.ToString();
        this.transform.parent.parent.Find("InfoPanel").Find("Health").GetComponent<Text>().text = "Health: " + MyItemInfo.Health.ToString();
        this.transform.parent.parent.Find("InfoPanel").Find("Balance").GetComponent<Text>().text = "$ " + _GameManager.instance.cashCurrency.ToString();
        this.transform.parent.parent.Find("InfoPanel").Find("ItemID").GetComponent<Text>().text = this.ItemIdentifier.ToString();
        this.transform.parent.parent.Find("InfoPanel").gameObject.SetActive(true);

    }
}
