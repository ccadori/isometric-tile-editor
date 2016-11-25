using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICharController : MonoBehaviour {

	public static UICharController uiCharController;

	public Text alertText;
	public GameObject alert;

	public Text statsText;
	public GameObject statsObject;

	public Text nameText;
	public GameObject nameObject;

	public Text InputText;
	public GameObject input;
	private string InputType;

	void Awake(){
		uiCharController = this;
	}

	public void SetCharName(string charName){
		nameText.text = charName;
		ActiveCharObjects(true);
	}

	public void InputAccept(){
		if (InputType == "add"){
			SceneCharacter.sceneCharacter.NewCharacter(InputText.text);
		} else if (InputType == "load"){
			SceneCharacter.sceneCharacter.LoadCharacter(InputText.text);
		}
		input.SetActive(false);
	}

	public void InputCancel(){
		input.SetActive(false);
	}

	public void AddButton (){
		
		input.SetActive(true);
		InputType = "add";
	}

	public void SaveButton(){
		SceneCharacter.sceneCharacter.SaveCharacter();
	}

	public void LoadButton (){
		
		input.SetActive(true);
		InputType = "load";
	}

	public void ActiveInstantAlert(string message){
		alertText.text = message;
		alert.gameObject.SetActive(true);
		Invoke("InactiveAlert", 2f);
	}

	public void InactiveAlert(){
		alert.gameObject.SetActive(false);
	}

	public void BackButton (){
		
		Application.LoadLevel("Menu");
	}

	public void ActiveCharObjects(bool active){
		nameObject.SetActive(active);
		statsObject.SetActive(active);
	}

	public void SetStats(Stats stats){
		statsText.text = stats.defense.ToString() + "\n" + stats.speedAttack + "\n" + stats.speedMovement + "\n" + stats.hp + "\n" + stats.basicDamage;
	}
}