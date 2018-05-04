using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Item {
	public Player launcher;
	public string type; // TODO

	private float speed;
	private float range;

	public bool isActive;

	private float distanceTraveled;

    // Texture
    public Sprite itemSprite;

	private void Awake() {
	}

	private void Start() {
		Launch("standard", 10f, 5f, true);

	}

	public void Launch(/*Player owner, */string type, float speed, float range, bool isLaunched) {
		// this.launcher = owner;
		this.type = type;
		this.speed = speed;
		this.range = range;

		this.isActive = isLaunched;



	}
	
	// Update is called once per frame
	void Update () {

		Vector2 inputVector = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));

/*  
Vector3 vectorToTarget = targetTransform.position - transform.position;
float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
*/


		// if (isActive) {
		// 	  if (distanceTraveled < range) {
		// 		Vector2 oldPosition = transform.position;
		// 		transform.Translate(0,0,1 * Time.deltaTime);
		// 		distanceTraveled += Vector2.Distance(oldPosition, transform.position);
		// 	}
		// } 
	}



	public void Drop() {

	}
}