using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIEditorController : MonoBehaviour {

	//static script
	public static UIEditorController uiController;

	//Edit Button-----------------------------------------
	public Text editButtonText;
	public Image editButtonImage;

	public int EditMode;

	public Sprite change;
	public string changeText;
	public Sprite remove;
	public string removeText;
	public Sprite add;
	public string addText;
	public Sprite start;
	public string startText;
	//----------------------------------------------------

	//Alert-----------------------------------------------
	public Text alertText;
	public GameObject alert;

	//Input-----------------------------------------------
	public Text InputText;
	public GameObject input;
	private string InputType;
	//----------------------------------------------------

	public void Awake(){
		
		uiController = this;
	}

	//Input system--------------------------------------------------
	//Accept
	public void InputAccept(){
		if (InputType == "save"){
			EditorController.editorController.SaveLand(InputText.text);
		} else if (InputType == "load"){
			EditorController.editorController.LoadLand(InputText.text);
		}
		input.SetActive(false);
	}
	//Cancel
	public void InputCancel(){
		input.SetActive(false);
	}
	//Save
	public void SaveButton (){
		
		input.SetActive(true);
		InputType = "save";
	}
	//Load
	public void LoadButton (){
		
		input.SetActive(true);
		InputType = "load";
	}

	//Menu Buttons--------------------------------------------------
	public void BackButton (){
		
		Application.LoadLevel("Menu");
	}
	//AddLand
	public void AddLandButton(){

		EditorController.editorController.NewLand(1200);
	}
	//Move
	public void MoveHorizontalButton(int direction){
		EditorController.editorController.Move(new Vector3(direction, 0, 0));
	}
	//Move
	public void MoveVerticalButton(int direction){
		EditorController.editorController.Move(new Vector3(0, direction, 0));
	}

	//Change Choosen Tile Type---------------------------------------------------------------
	public void ChoseTileType (GameObject self){
		self.GetComponent<RectTransform>().sizeDelta = new Vector2(60,60);
		GameObject[] tileTypeButton = GameObject.FindGameObjectsWithTag("TileButtonSelect");
		foreach (GameObject temp in tileTypeButton){
			if (self == temp)
				continue;
			temp.GetComponent<RectTransform>().sizeDelta = new Vector2(50,50);
		}
	}

	//Alert-----------------------------------------------------------------------------------
	public void ActiveInstantAlert(string message){
		alertText.text = message;
		alert.gameObject.SetActive(true);
		Invoke("InactiveAlert", 2f);
	}
	//Inactive Alert
	public void InactiveAlert(){
		alert.gameObject.SetActive(false);
	}

	//Change Edit Mode------------------------------------------------------------------------
	public void ChangeEditButton (){
		int i = EditorController.editorController.ChangeEditMode();
		if ( i == 0 ){
			editButtonImage.sprite = add;
			editButtonText.text = addText;
		} else if ( i == 1 ){
			editButtonImage.sprite = change;
			editButtonText.text = changeText;
		} else if ( i == 2 ){
			editButtonImage.sprite = remove;
			editButtonText.text = removeText;
		} else if ( i == 3){
			editButtonImage.sprite = start;
			editButtonText.text = startText;
		}
	}
}