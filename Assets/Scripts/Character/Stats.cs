using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

	public float defense;
	public float speedAttack;
	public float speedMovement;
	public float hp;
	public float basicDamage;

	public void SetDefault(){
		defense = 1;
		speedAttack = 1;
		speedMovement = 1;
		hp = 100;
		basicDamage = 10;
	}
}
