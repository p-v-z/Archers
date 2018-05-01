using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour {
    public static GameManager GM;

    public CameraScript cameraScript;

    public GameObject archer;

	public List<Player> players;
    public Player currentPlayer { get; set; }

    private void Awake() {
        MakeThisTheOnlyGameManager();

		DOTween.Init(true, true, null);

        this.players = new List<Player>();

        //  instantiate players;
        AddPlayer("stokovitch", new Vector2(3f, 3f));
        AddPlayer("janaz", new Vector2(6f, 4f));
        AddPlayer("kov gul", new Vector2(8f, 5f));

        currentPlayer = players[0];
        cameraScript.followUnit = currentPlayer.archer;
        Debug.Log(currentPlayer.name);
    }
	
	void MakeThisTheOnlyGameManager() {
        if (GM == null) {
            DontDestroyOnLoad(gameObject);
            GM = this;
        } else {
            if (GM != this) {
                Destroy(gameObject);
            }
        }
    }

	void Update () {
		
	}

	void AddPlayer(string name, Vector2 position) {
        // Create player
        Player currentPlayer = new Player(name);

        // Add player to list
        this.players.Add(currentPlayer);

        // Instantiate archer for player
        GameObject currentArcher = Instantiate(archer, new Vector3(position.x, position.y, 5f), this.transform.rotation);
        currentArcher.GetComponent<ArcherScript>().init(currentPlayer);
        currentPlayer.archer = currentArcher;
	}
}
