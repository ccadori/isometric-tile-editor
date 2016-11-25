using UnityEngine;
using System.Collections;

public class PlayStatements : MonoBehaviour {

	public string character;
	public int typePlay;

	void Awake(){
		DontDestroyOnLoad(gameObject);
	}

}
