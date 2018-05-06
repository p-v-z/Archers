using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Item {
	public string type; // TODO
	public float AngleAdjust;

	public Player launcher;
	public bool isActive;

	private float distanceTraveled;

	private float speed = 1;
	private float range = 1;

    // Texture
    public Sprite itemSprite;

	// private Quaternion m_desiredDirection;

	private void Awake() {
	}

	private void Start() {

		// Launch("standard", 10f, 5f, direction);



	}

	public void Launch(Player owner, Transform source, Quaternion direction, float speed, float range) {
		this.launcher = owner;
		this.speed += speed;
		this.range += range;
		this.transform.position = source.position;
		this.transform.rotation = direction;
		this.gameObject.SetActive(true);
		this.isActive = true;
		this.distanceTraveled = 0;
	}
	
	
	
	void Update () {
		if (isActive) {
			if (distanceTraveled < this.range) {
				// Move forward
				transform.Translate(Vector3.up * Time.deltaTime * this.speed);
				distanceTraveled += Time.deltaTime * this.speed;
				Debug.Log(distanceTraveled);
			} else {
				Disable();
			}
		}

	}

	void Disable() {
		this.speed = 1;
		this.range = 1;
		this.gameObject.SetActive(true);
		this.isActive = true;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (isActive) {
			// If the archer hit is not the launcher
			if (other.gameObject != launcher.archer) {
				ArcherScript archerScript = other.gameObject.GetComponent<ArcherScript>();
				archerScript.Kill(launcher);
			}
		}
	}

	public void Drop() {

	}
}