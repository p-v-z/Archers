using UnityEngine;
using System.Collections;

public class PlayerController2D : MonoBehaviour {
	public GameObject containerObject;
	private ArcherScript archer;
	private Animator anim; //The parent animator.

	private bool moveEnabled = true; //Bool used to check if movement is enabled or not. Use ToggleMovement() to set.

	private float baseSpeed = 5f; //Base movement speed
	public float speedModifier = 0f;

	private Vector2 moveVector; //The vector used to apply movement to the controller.
	private float moveSense = 0.2f; //An axis value above this is considered movement.
	private enum MoveState { Stand, Walk, Run } //States for standing, walking and running.
	private MoveState moveState	= MoveState.Stand; //Create and set a MoveState variable for the controller.

	void Start(){
		anim = GetComponent<Animator>();
		archer = containerObject.GetComponent<ArcherScript>();
	}

	public void ToggleMovement(bool enable){
		moveEnabled = enable;
	}

	void FixedUpdate(){
		if (moveEnabled == true){
			//If movement is enabled and any movement above the threshold (sense) is detected, move controller.
			if (moveVector.x > moveSense || moveVector.x < -moveSense || moveVector.y > moveSense || moveVector.y < -moveSense) {
				containerObject.transform.Translate(moveVector * ((baseSpeed + speedModifier) / 100)); 
			}
		}
	}

	void Update(){
		// Only take input from currentPlayer
		if (GameManager.GM.currentPlayer != archer.owner) return;
		// Dont move if you busy launching
		if (archer.state == "launching") return;

		if (moveEnabled == true){
			moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

			//If horizontal or vertical axis is above the threshold value (moveSense), set the move state to Walk.
			if (Input.GetAxisRaw("Horizontal") > moveSense || Input.GetAxisRaw("Horizontal") < (-moveSense) || Input.GetAxisRaw("Vertical") > moveSense || Input.GetAxisRaw("Vertical") < (-moveSense)){
				moveState = MoveState.Walk;

				//Pass the moveVector axes to the animators move variables and set animator's isMoving to true.
				anim.SetFloat("moveX", moveVector.x);
				anim.SetFloat("moveY", moveVector.y);
				anim.SetBool("isMoving", true);
			} else {
				//If there's no input, set the state to stand again and change Animator's isMoving to false.
				moveState = MoveState.Stand;
				anim.SetBool("isMoving", false);
			}

			if (Input.GetButton("useBoots") && moveState == MoveState.Walk) {
				//If the controller is moving and we're holding the run button, double the moveSpeed and change to Run state. Also tell animator to display running animation.
				speedModifier = 2f;
				moveState = MoveState.Run;
				anim.SetBool("isRunning", true);
			} else if (Input.GetButtonUp("useBoots") || moveState == MoveState.Stand) {
				//Set the speed and Animator running bool back when we're not running.
				speedModifier = 0f;
				anim.SetBool("isRunning", false);
			}
		}
	}

	public void ModifySpeed() {

	}

	public void PlayAnimation(string triggerName) {
		anim.SetTrigger(triggerName);
	}
}
