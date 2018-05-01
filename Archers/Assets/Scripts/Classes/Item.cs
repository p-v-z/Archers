using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour {
    public virtual string itemName {get; protected set;}

    // Is the item being sold (true), or can you just pick it up (false)
    public bool isForSale;
    // Base price of tiem
    public int value;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        itemName = "none";
        
    }

    private void Start() {
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Archer") {
            ArcherScript archer =  other.GetComponent<ArcherScript>();
            archer.SetCurrentItem(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Archer") {
            ArcherScript archer =  other.GetComponent<ArcherScript>();
            archer.DeselectItem(this.gameObject);
        }
    }
}
