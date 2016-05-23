using UnityEngine;
using System.Collections;

public class GameItems : MonoBehaviour
{

	// Static array of every available game object
	public static readonly InventoryItem[] gameItems = 
	{
		// Default item (Nothing)
        // Index 0
		new InventoryItem(		
			"None",
			"",
			"",
			InventoryItem.ItemTypes.NONE,
			0,0,0,0,0,0),


		//Single Handed Weapons

        // Index 1
		new InventoryItem(		
			"Paper Clip",
			"A common office paper clip.",
			"weapons/ass_wep_clippy",
			InventoryItem.ItemTypes.WEAPON_ONE_HANDED,
			350,4,1,0,0,0),

        // Index 2
		new InventoryItem(		
			"Daisy",
			"A sweet smelling daisy.",
			"weapons/ass_wep_daisy",
			InventoryItem.ItemTypes.WEAPON_ONE_HANDED,
			300,3,0,0,1,0),
        
        // Index 3
		new InventoryItem(		
			"Lolly Pop",
			"A strawberry flavoured lolly pop.",
			"weapons/ass_wep_lolliepop",
			InventoryItem.ItemTypes.WEAPON_ONE_HANDED,
			400,4,1,0,2,0),
        
        // Index 4
		new InventoryItem(		
			"Wooden Pencil",
			"A long wooden pencil with a pointed graphite tip.",
			"weapons/ass_wep_pencil",
			InventoryItem.ItemTypes.WEAPON_ONE_HANDED,
			500,6,1,0,1,0),
        
        // Index 5
		new InventoryItem(		
			"Olive Stick",
			"A tooth pick with an olive on top.",
			"weapons/ass_wep_olivestick",
			InventoryItem.ItemTypes.WEAPON_ONE_HANDED,
			200,3,0,1,0,0),
        
        // Index 6
		new InventoryItem(		
			"Tooth Brush",
			"A blue plastic tooth brush.",
			"weapons/ass_wep_toothbrush",
			InventoryItem.ItemTypes.WEAPON_ONE_HANDED,
			450,5,1,1,0,0),

		// Two Handed Weapons
        
        // Index 7
		new InventoryItem(		
			"Burrito",
			"A tasty burrito that's too big to eat with just one hand.",
			"weapons/ass_wep_burrito",
			InventoryItem.ItemTypes.WEAPON_TWO_HANDED,
			1000,10,2,0,0,0),
        
        // Index 8
		new InventoryItem(		
			"Paint Brush",
			"A basic artist's paint brush.",
			"weapons/ass_wep_paintbrush",
			InventoryItem.ItemTypes.WEAPON_ONE_HANDED,
			850,9,2,0,0,0),
        
        // Index 9
		new InventoryItem(		
			"Plastic Fork",
			"A large 3 pronged plastic table fork.",
			"weapons/ass_wep_plasticfork",
			InventoryItem.ItemTypes.WEAPON_TWO_HANDED,
			800,8,1,0,1,0),
        
        // Index 10
		new InventoryItem(		
			"Pop Stick Sword",
			"A sword fashioned from 2 pop sticks.",
			"weapons/ass_wep_popsticks",
			InventoryItem.ItemTypes.WEAPON_TWO_HANDED,
			1250,12,2,0,0,0),
        
        // Index 11
		new InventoryItem(		
			"Stick",
			"A stick from a tree's branch.",
			"weapons/ass_wep_stick",
			InventoryItem.ItemTypes.WEAPON_TWO_HANDED,
			900,8,2,0,1,0),


		// Armour Head
        
        // Index 12
		new InventoryItem(		
			"Bow Tie",
			"A pretty pink bow tie.",
			"cosmetics/ass_head_bowtie",
			InventoryItem.ItemTypes.ARMOUR_HEAD,
			320,0,2,1,1,0),
        
        // Index 13
		new InventoryItem(		
			"Broccoli",
			"A piece of fresh brocoli.",
			"cosmetics/ass_head_broccoli",
			InventoryItem.ItemTypes.ARMOUR_HEAD,
			350,1,2,0,1,0),
        
        // Index 14
		new InventoryItem(		
			"Egg",
			"A fried chicken egg. Sunny side up.",
			"cosmetics/ass_head_egg",
			InventoryItem.ItemTypes.ARMOUR_HEAD,
			300,0,3,0,1,0),
        
        // Index 15
		new InventoryItem(		
			"Blue Feather",
			"A decorative blue feather.",
			"cosmetics/ass_head_feather1",
			InventoryItem.ItemTypes.ARMOUR_HEAD,
			250,0,1,1,0,0),
        
        // Index 16
		new InventoryItem(		
			"Cyan Feather",
			"A decorative cyan feather.",
			"cosmetics/ass_head_feather2",
			InventoryItem.ItemTypes.ARMOUR_HEAD,
			250,0,1,0,1,0),
        
        // Index 17
		new InventoryItem(		
			"Violet Feather",
			"A decorative violet feather.",
			"cosmetics/ass_head_feather3",
			InventoryItem.ItemTypes.ARMOUR_HEAD,
			250,1,1,0,0,0),
        
        // Index 18
		new InventoryItem(		
			"Leaves",
			"A bunch of tree leaves.",
			"cosmetics/ass_head_leaf",
			InventoryItem.ItemTypes.ARMOUR_HEAD,
			300,1,2,0,0,0),
        
        // Index 19
		new InventoryItem(		
			"Pom Pom",
			"A puffy pink pom pom.",
			"cosmetics/ass_head_pompom",
			InventoryItem.ItemTypes.ARMOUR_HEAD,
			250,0,1,1,1,0),


		// Armour Mouth
        
        // Index 20
		new InventoryItem(		
			"Fake Mostache",
			"A thick and bushy moustache.",
			"cosmetics/ass_mouth_moustache",
			InventoryItem.ItemTypes.ARMOUR_MOUTH,
			300,0,3,0,1,0),
        
        // Index 21
		new InventoryItem(		
			"Stitched Mouth",
			"A row of surgical stitches.",
			"cosmetics/ass_mouth_stitches",
			InventoryItem.ItemTypes.ARMOUR_MOUTH,
			200,0,2,1,0,0),
        
        // Index 22
		new InventoryItem(		
			"Taped Mouth",
			"A mouth bound with sticky tape.",
			"cosmetics/ass_mouth_tape",
			InventoryItem.ItemTypes.ARMOUR_MOUTH,
			280,1,2,0,0,0),


		// Armour Eye

        // Index 23
		new InventoryItem(		
			"Eye Patch",
			"An eye patch to look more piratey.",
			"cosmetics/ass_eye_eyepatch",
			InventoryItem.ItemTypes.ARMOUR_EYE,
			400,1,2,0,0,0),

        // Index 24
		new InventoryItem(		
			"Google Eyes",
			"A pair of googly craft eyes.",
			"cosmetics/ass_eye_google",
			InventoryItem.ItemTypes.ARMOUR_EYE,
			300,0,2,0,1,0),

        // Index 25
		new InventoryItem(		
			"Marker Eyes",
			"Eyes drawn on with a black marker pen.",
			"cosmetics/ass_eye_marker",
			InventoryItem.ItemTypes.ARMOUR_EYE,
			250,0,1,1,0,0),
	};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
