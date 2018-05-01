using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Item {
	public Player launcher;
	public string type; // TODO

	private float speed;
	private float range;

	public bool isActive;

    // Texture
    public Sprite itemSprite;

	public void Drop() {

	}

	public void Launch(Player owner, string type, float speed, float range, bool isLaunched) {
		this.launcher = owner;
		this.type = type;
		this.speed = speed;
		this.range = range;

		this.isActive = isLaunched;
		if (isActive) { 
			// Instantiate()
		} else {
			// this.gameObject = null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			// Debug.Log(this.transform);
		} else {
		}
	}
}