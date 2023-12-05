using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public abstract class Card {
    public enum Rarity {
        Common,
        Rare,
        Legendary
    }
    
    public int id;
    public int cost;
    public Rarity rarity;
    
    // Constructor
    public Card() { }
    
    public abstract void UseCard(Vector2 charPos, Vector2 target);
}


public class ShootArrow : Card {
    public ShootArrow() : base() {
        id = 5;
        rarity = Rarity.Common;
        cost = 1;
    }
    
    public override void UseCard(Vector2 charPos, Vector2 target) {
        GameObject arrowObject = new GameObject("ArrowObject");
        Arrow arrowInstance = arrowObject.AddComponent<Arrow>();
        arrowObject.transform.SetParent(GameObject.Find("MainCanvas/Projectiles").transform);
        arrowInstance.transform.position = charPos;
        arrowInstance.CreateProjectile(charPos, target);
    }
}