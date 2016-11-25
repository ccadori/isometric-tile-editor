using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMenuController : MonoBehaviour {

	public GameObject playerStatement;

	//Play
	public GameObject play;

	//Input
	public Text inputText;
	public GameObject input;

	//Alert
	public Text alertText;
	public GameObject alert;

	//Start
	public void StartButton (){
		
		input.SetActive(true);
	}
	//Accept
	public void AcceptButton(){

		if (Character.LoadCharacter(inputText.text) != null){
			playerStatement.GetComponent<PlayStatements>().character = inputText.text;
			CancelButton();
			play.SetActive(true);
		} else {
			ActiveInstantAlert("Personagem nao encontrado.");
			CancelButton();
		}
	}
	//Cancel
	public void CancelButton (){

		input.SetActive(false);
	}
	//Choose Arcade
	public void Arcade (){

		playerStatement.GetComponent<PlayStatements>().typePlay = 0;
	}
	//Choose Carrer
	public void Carrer (){

		playerStatement.GetComponent<PlayStatements>().typePlay = 1;
	}
	//Exit
	public void ExitButton (){
		
		Application.Quit();
	}
	//Editor
	public void EditorButton (){
		
		Application.LoadLevel("Editor");
	}
	//Character
	public void CharacterButton(){
		Application.LoadLevel("Character");
	}
	//Alert Active
	public void ActiveInstantAlert(string message){
		alertText.text = message;
		alert.gameObject.SetActive(true);
		Invoke("InactiveAlert", 2f);
	}
	//Inactive Alert
	public void InactiveAlert(){
		alert.gameObject.SetActive(false);
	}

}
