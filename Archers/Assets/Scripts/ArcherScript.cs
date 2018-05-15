using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherScript : MonoBehaviour {
	public GameObject archerObject;


	public int maxArrows;
	public List<Arrow> arrows;

	public Quiver quiver{ get; set; }
	public Bow bow{ get; set; }
	public Boots boots{ get; set; }
	public Dagger dagger{ get; set; }

	public bool isDead;
	// public bool isAiming;
	// public float moveSpeed;

	[HideInInspector]
	public PlayerController2D controller;
	public Player owner; // { get; set; 
	public string state;

	private bool handleInput;

	// Input actions: "fire", "interact", "useBoots", "useItem"
	
	public void init (Player owner) {
		this.owner = owner;

		this.controller = archerObject.GetComponent<PlayerController2D>();
		
		this.maxArrows = 3;
		this.arrows = new List<Arrow>();
		
		this.quiver = null;
		this.bow = null;
		this.boots = null;
		this.dagger = null;

		this.isDead = false;
		// this.moveSpeed = 0;		
		// this.isAiming = false;

		this.handleInput = true;

		this.state = "normal";
	}

	void Start () {
		SetState("normal");
	}

	void Update () {
		// Handle player controls
		if (GameManager.GM.currentPlayer != owner) return;

        if (!isDead) {
			if (this.state == "launching") return;


			if (Input.GetButtonDown("interact")) {
				Debug.Log("Press Interact");
				PickupItem();
			}


			// if (Input.GetKeyDown(KeyCode.M)) { // get bow modifiers
			// 	if (this.bow != null) {
			// 		Debug.Log("speedModifier: " + this.bow.speedModifier.ToString());
			// 		Debug.Log("rangeModifier: " + this.bow.rangeModifier.ToString());
			// 	} else {
			// 		Debug.Log("now bow equipped");
			// 	}
			// }

			switch (this.state) {
				case "normal":
					
					HandleAim();
					break;
				case "aiming":
					HandleRelease();
					break;
				case "launching":
					break;
			}
		}
	}

	/*/////////////////////////////////////////////////////
					State Management
	 //////////////////////////////////////////////////////*/

	private void SetState(string newState) {
		if (this.state == newState) return;

		Debug.Log("state change: " + this.state + " => " + newState);
		this.state = newState;
		UI_Manager.UIM.SetText("State", newState);
	}


	/*/////////////////////////////////////////////////////
							Aim
	 //////////////////////////////////////////////////////*/
	void HandleAim() {
		if (Input.GetButtonDown("fire")) {
			// check if have arrows
			if (arrows.Count > 0) {
				this.SetState("aiming");
			} else {
				Debug.Log("No arrows");
			}
		} 

	}

	/*/////////////////////////////////////////////////////
							Release
	 //////////////////////////////////////////////////////*/
	void HandleRelease() {
		if (Input.GetButtonUp("fire")) {

			float rangeModifier = 0f;
			float speedModifier = 0f;
			if (this.bow != null) {
				rangeModifier = this.bow.rangeModifier;
				speedModifier = this.bow.speedModifier;
			} 
			Debug.Log("rangeModifier: " + rangeModifier.ToString());
			Debug.Log("speedModifier: " + speedModifier.ToString());
			

			Arrow currentArrow = arrows[0];
			
			// Calc input direction
			float x = Input.GetAxisRaw("Horizontal");
			float y = -Input.GetAxisRaw("Vertical");
			float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg + currentArrow.AngleAdjust;
			Quaternion direction = Quaternion.AngleAxis(angle , Vector3.back);

			// Release arrow
			arrows[0].Launch(this.owner, this.transform, direction, speedModifier, rangeModifier);
			arrows.RemoveAt(0);
			Debug.Log("angle:  " + angle);
			this.SetState("launching");
			// controller.PlayAnimation("shoot");

			StartCoroutine(ReleaseRoutine());
		}
	}

	private IEnumerator ReleaseRoutine() {
        yield return new WaitForSeconds(1000f);
		SetState("normal");
    }

	/*/////////////////////////////////////////////////////
					Select items in world
	 //////////////////////////////////////////////////////*/
	public List<GameObject> selectedItems;

	public void SetCurrentItem(GameObject item) {
		selectedItems.Add(item);
		UI_Manager.UIM.SetText("Interact", "(" + selectedItems.Count.ToString() + ")");
	}

	public void DeselectItem(GameObject item) {
		selectedItems.Remove(item);
        UI_Manager.UIM.SetText("Interact", "(" + selectedItems.Count.ToString() + ")");

	}


	/*/////////////////////////////////////////////////////
					Attempt to buy / pick item up
	 //////////////////////////////////////////////////////*/
	public void PickupItem() {
		if (selectedItems.Count > 0) {
			GameObject itemObject = selectedItems[0];
			Item itemScript = itemObject.GetComponent<Item>();
			
			if (itemScript.isForSale) {
				// Item needs to be purchased for cost
				int cost = itemScript.value;

				if (owner.gold >= cost) {
					// Buy
					Debug.Log("Pay " + cost);
					owner.gold -= cost;
					itemScript.isForSale = false;
					UI_Manager.UIM.SetText("Gold", GameManager.GM.currentPlayer.gold.ToString());

					AcquireItem(itemObject, itemScript);
				} else {
					// Fail
					Debug.Log("Not enough gold");
					return;
				}
			} else {
				// You can just pick item up
				AcquireItem(itemObject, itemScript);
			}
		}
	}

	public void AcquireItem(GameObject itemObject, Item itemScript) {
		/* Arrow (Carry multiple) */
		Arrow arrow = itemObject.GetComponent<Arrow>();
		if (itemObject.GetComponent<Arrow>() != null) {
			if (this.arrows.Count < this.maxArrows) {
				// Set current arrow to arrow in array
				this.arrows.Add(arrow);
				// Disable
				arrow.gameObject.SetActive(false);
				Debug.Log("Arrow Added");
			} else {
				Debug.Log("Arrows FULL");
			}

			// Update UI
			UI_Manager.UIM.SetText("Arrows", this.arrows.Count.ToString());
		}

		/* Bow (Single item) */
		Bow bow = itemObject.GetComponent<Bow>();
		if (itemObject.GetComponent<Bow>() != null) {
			// Recreate current bow in world
			if (this.bow) {
				// Set position to archer position
				this.bow.transform.position = this.transform.position;
				// Activate
				this.bow.gameObject.SetActive(true);
				// Clean
				this.bow = null;
			}

			// Set current bow to picked up bow
			this.bow = bow;
			// Disable
			this.bow.gameObject.SetActive(false);

			// Update UI
			UI_Manager.UIM.SetText("Bow", this.bow.bowType);
		}

		Boots boots = itemObject.GetComponent<Boots>();
		if (itemObject.GetComponent<Boots>() != null) {
			if (this.boots) {
				this.boots.transform.position = this.transform.position;
				this.boots.gameObject.SetActive(true);
				this.boots = null;
			}
			this.boots = boots;
			this.boots.gameObject.SetActive(false);
			UI_Manager.UIM.SetText("Boots", this.boots.bootsType);
		}
		Dagger dagger = itemObject.GetComponent<Dagger>();
		if (itemObject.GetComponent<Dagger>() != null) {
			if (this.dagger) {
				this.dagger.transform.position = this.transform.position;
				this.dagger.gameObject.SetActive(true);
				this.dagger = null;
			}
			this.dagger = dagger;
			this.dagger.gameObject.SetActive(false);
			UI_Manager.UIM.SetText("Dagger", this.dagger.daggerType);
		}
		Quiver quiver = itemObject.GetComponent<Quiver>();
		if (itemObject.GetComponent<Quiver>() != null) {
			if (this.quiver) {
				this.quiver.transform.position = this.transform.position;
				this.quiver.gameObject.SetActive(true);
				this.quiver = null;
			}
			this.quiver = quiver;
			this.quiver.gameObject.SetActive(false);
			UI_Manager.UIM.SetText("Quiver", "+" + this.quiver.extraArrows.ToString());
		}
	}

	public void Kill(Player killer) {
		this.isDead = true;
		Debug.Log(owner.name + " has been killed");
		UI_Manager.UIM.UpdateArcher(gameObject);
		SetState("dead");
	}

	public void DropItem(GameObject item) {

	}

}
