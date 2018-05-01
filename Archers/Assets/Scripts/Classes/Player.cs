using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {
    public GameObject archer;
    public int gold;
    public string name;


    public Player(string name) {
        this.name = name;
        this.gold = 0;
    }

}