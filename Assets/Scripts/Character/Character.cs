using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class Character : MonoBehaviour {

	public static Char LoadCharacter(string name){
		XmlSerializer serializer = new XmlSerializer (typeof(Char));
		Char character;
		try {
			FileStream stream;
			if (Application.isEditor)
				stream = new FileStream ("Assets/DataBase/" + name + ".xml", FileMode.Open);
			else 
				stream = new FileStream (Application.dataPath + "/" + name + ".xml", FileMode.Open);
			character = serializer.Deserialize (stream) as Char;
			stream.Close ();
		} catch {
			return null;
		}
		return character;
	}

	public static Char GenerateCharacter(string name){
		Char character;
		character = new Char(){
			faseLevel = 0,
			charName = name,
			head = 0,
			robe = 1,
			weapon1 = 2,
			weapon2 = 0,
			level = 1
		};
		return character;
	}

	public static bool SaveCharacter(Char character){
		XmlSerializer serializer = new XmlSerializer (typeof(Char));
		try{
			FileStream stream;
			if (Application.isEditor)
				stream = new FileStream ("Assets/DataBase/" + character.charName + ".xml", FileMode.Create);
			else 
				stream = new FileStream (Application.dataPath + "/" + character.charName + ".xml", FileMode.Create);
			serializer.Serialize (stream, character);
			stream.Close ();
		} catch {
			return false;
		}
		return true;
	}

	public static GameObject InstanceCharacter(Char character, GameObject player){
		GameObject newCharacter = Instantiate(player, new Vector3(0,0,0), Quaternion.Euler(0,0,0)) as GameObject;
		newCharacter.GetComponent<CharController>().character = character;
		newCharacter.GetComponent<Stats>().SetDefault();
		return newCharacter;
	}

	public static void SetCharacterItems(GameObject player, GameObject[] items){
		CharController charController = player.GetComponent<CharController>();
		Char character = charController.character;
		GameObject[] poses = charController.poses;
		Stats playerStats = player.GetComponent<Stats>();
		SetStats(playerStats, items);
		charController.weapon1 = items[character.weapon1];
		charController.weapon2 = items[character.weapon2];
		charController.level = character.level;
		for (int i = 0; i < poses.Length; i++){
			foreach(Transform temp in poses[i].transform){
				if (temp.name == "Weapon1"){
					if (character.weapon1 != 0)
						temp.gameObject.GetComponent<SpriteRenderer>().sprite = items[character.weapon1].GetComponent<ItemController>().poses[i];
				}
				if (temp.name == "Weapon2"){
					if (character.weapon2 != 0)
						temp.gameObject.GetComponent<SpriteRenderer>().sprite = items[character.weapon2].GetComponent<ItemController>().poses[i];
				}
				if (temp.name == "Head"){
					temp.gameObject.GetComponent<SpriteRenderer>().sprite = items[character.head].GetComponent<ItemController>().poses[i];
				}
				if (temp.name == "Robe"){
					temp.gameObject.GetComponent<SpriteRenderer>().sprite = items[character.robe].GetComponent<ItemController>().poses[i];
				}
			}
		}
	}

	public static void SetStats(Stats playerStats, GameObject[] items){
		foreach (GameObject item in items){
			Stats itemStats = item.GetComponent<Stats>() ;
			playerStats.defense += itemStats.defense;
			playerStats.speedAttack += itemStats.speedAttack;
			playerStats.speedMovement += itemStats.speedMovement;
			playerStats.hp += itemStats.hp;
			playerStats.basicDamage += itemStats.basicDamage;
		}
	}
}