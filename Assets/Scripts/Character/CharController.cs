using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {

	public Stats stats;
	public Char character;

	public GameObject weapon1;
	public GameObject weapon2;

	public GameObject[] poses;
	public int actualPose;
	public Vector3 actualDirection;
	public int top;
	public int bot;

	public int level;

	private void ChangePose(int newPose){
		foreach (GameObject pose in poses ){
			pose.SetActive(false);
		}
		actualPose = newPose;
		poses[actualPose].SetActive(true);
	}
}
