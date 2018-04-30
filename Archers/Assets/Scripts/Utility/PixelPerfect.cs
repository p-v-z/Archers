using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfect : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		// height / ppu / 2 for forula / 2 for zoom
		GetComponent<Camera>().orthographicSize = Screen.height / 32 / 2 / 2;
	}
}
