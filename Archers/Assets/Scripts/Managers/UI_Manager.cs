using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
    public static UI_Manager UIM;

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

    private void Awake() {
        MakeThisTheOnlyUIManager();
	}

	void MakeThisTheOnlyUIManager() {
        if (UIM == null) {
            DontDestroyOnLoad(gameObject);
            UIM = this;
        } else {
            if (UIM != this) {
                Destroy(gameObject);
            }
        }
    }

	public void UpdateArcher(GameObject archer) {		
		ArcherScript archerScript = archer.GetComponent<ArcherScript>();
		
		// only update if current player
		if (archerScript.owner != GameManager.GM.currentPlayer) return;

		// update fields
		SetText("State", "-");
		SetText("MoveSpeed", archerScript.controller.speedModifier.ToString());
		SetText("Gold", archerScript.owner.gold.ToString());
		SetText("Arrows", archerScript.arrows.Count.ToString());
		SetText("Dagger", "-");
		SetText("Quiver", "-");
		SetText("Bow", "-");
		SetText("Boots", "-");
		SetText("Interact", "(" + archerScript.selectedItems.Count.ToString() + ")");
	}

	public void SetText(string field, string newText) {
		switch(field) {
			case "State":
				txtState.text = "State: " + newText;
				break;
			case "MoveSpeed":
				txtMoveSpeed.text = "MoveSpeed: " + newText;
				break;
			case "Gold":
				txtGold.text = "Gold: " + newText;
				break;
			case "Arrows":
				txtArrows.text = "Arrows: " + newText;
				break;
			case "Dagger":
				txtDagger.text = "Dagger: {" + newText + "}";
				break;
			case "Quiver":
				txtQuiver.text = "Quiver: {" + newText + "}";
				break;
			case "Bow":
				txtBow.text = "Bow: {" + newText + "}";
				break;
			case "Boots":
				txtBoots.text = "Boots: {" + newText + "}";
				break;
			case "Interact":
				txtInteract.text = "Interact: " + newText;
				break;
		}
	}

	// Update is called once per frame
	void Update () {
		txtResolution.text = "(" + Screen.width + "x" + Screen.height + ")";		
	}
}
