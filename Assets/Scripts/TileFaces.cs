using UnityEngine;
using System.Collections;

public class TileFaces : MonoBehaviour {

	public int face;

	void OnMouseDown(){
		EditorController.editorController.AddTile( gameObject.transform.parent.gameObject, face );
	}

}
