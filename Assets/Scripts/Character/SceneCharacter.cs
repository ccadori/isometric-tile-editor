using UnityEngine;
using System.Collections;

public class SceneCharacter : MonoBehaviour {

	public static SceneCharacter sceneCharacter;

	public GameObject player;
	public GameObject[] items;

	void Awake(){
		sceneCharacter = this;
	}

	public void ClearCharacter(){
		Destroy(GameObject.FindGameObjectWithTag("Player"));
	}

	public void NewCharacter(string charName){
		if (charName == ""){
			UICharController.uiCharController.ActiveInstantAlert("Informe um nome.");
			return;
		}
		ClearCharacter();
		Char character = Character.GenerateCharacter(charName);
		GameObject newCharacter = Character.InstanceCharacter(character, player);
		Character.SetCharacterItems(newCharacter, items);
		UICharController.uiCharController.SetCharName(character.charName + " (" + character.level + ")");
		UICharController.uiCharController.SetStats(newCharacter.GetComponent<Stats>());
	}

	public void SaveCharacter(){
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Char character = player.GetComponent<CharController>().character;
		if (Character.SaveCharacter(character))
			UICharController.uiCharController.ActiveInstantAlert("Personagem Salvo.");
		else 
			UICharController.uiCharController.ActiveInstantAlert("Problema ao Salvar personagem.");
	}

	public void LoadCharacter(string charName){
		ClearCharacter();
		Char character = Character.LoadCharacter(charName);
		if (character != null) {
			UICharController.uiCharController.ActiveInstantAlert("Personagem carregado.");
			GameObject newCharacter = Character.InstanceCharacter(character, player);
			Character.SetCharacterItems(newCharacter, items);
			UICharController.uiCharController.SetCharName(character.charName + " (" + character.level + ")");
			UICharController.uiCharController.SetStats(newCharacter.GetComponent<Stats>());
		} else {
			UICharController.uiCharController.ActiveInstantAlert("Personagem nao encontrado.");
			UICharController.uiCharController.ActiveCharObjects(false);
		}
	}
}