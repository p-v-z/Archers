using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
	public Text txtResolution;

	public Text txtState;
	public Text txtMoveSpeed;
	public Text txtGold;
	public Text txtArrows;

	public Text txtDagger;
	public Text txtQuiver;
	public Text txtBow;
	public Text txtBoots;
	public Text txtInteract;

	// Update is called once per frame
	void Update () {
		txtResolution.text = "(" + Screen.width + "x" + Screen.height + ")";		
	}
}
