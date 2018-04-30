using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	public GameObject followUnit;

	private float startZ;
	// Use this for initialization
	void Start () {
		this.startZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(followUnit.transform.position.x, followUnit.transform.position.y, this.startZ);
	}
}
