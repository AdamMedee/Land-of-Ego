using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public abstract class Card {
    public enum Rarity {
        Common,
        Rare,
        Legendary
    }
    
    public int id;
    public int cost;
    public Rarity rarity;
    protected GameObject models;
    
    // Constructor
    public Card() { }
    
    public abstract void UseCard(Vector2 charPos, Vector2 target, int team);
    
    // 0 is team hero, 1 is team enemy
}


public class ShootArrow : Card {
    public ShootArrow() : base() {
        id = 5;
        rarity = Rarity.Common;
        cost = 1;
    }
    
    public override void UseCard(Vector2 charPos, Vector2 target, int team) {
        models = GameObject.Find("AttackModels");
        GameObject arrowModel = models.transform.Find("ShootArrow").gameObject;
        GameObject arrowObject = new GameObject("ArrowObject");
        
        Image originalImage = arrowModel.GetComponent<Image>();
        Image copiedImage = arrowObject.AddComponent<Image>();
        copiedImage.sprite = originalImage.sprite;
        copiedImage.color = originalImage.color;
        
        arrowObject.transform.position = arrowModel.transform.position;
        arrowObject.transform.rotation = arrowModel.transform.rotation;
        arrowObject.transform.localScale = arrowModel.transform.localScale;
        RectTransform modelTransform = arrowModel.GetComponent<RectTransform>();
        RectTransform objectTransform = arrowObject.GetComponent<RectTransform>();
        objectTransform.sizeDelta = modelTransform.sizeDelta;
        
        Arrow arrowInstance = arrowObject.AddComponent<Arrow>();
        arrowObject.transform.SetParent(GameObject.Find("MainCanvas/Projectiles").transform);
        
        arrowInstance.transform.position = charPos;
        Vector3 direction = (charPos - target).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        arrowInstance.CreateProjectile(charPos, target, team);
    }
    
}