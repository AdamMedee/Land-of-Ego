using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.UI;

public abstract class Card {
    public enum Rarity {
        Common,
        Rare,
        Legendary
    }
    
    public int cost;
    public Rarity rarity;
    protected GameObject models;
    public Image cardModel;
    
    // Constructor
    public Card() { }
    
    public abstract bool UseCard(Vector2 charPos, Vector2 target, int team);
    
    // 0 is team hero, 1 is team enemy
}


public class ShootArrow : Card {
    public ShootArrow() : base() {
        rarity = Rarity.Common;
        cost = 1;
        GameObject cardModels = GameObject.Find("CardModels");
        cardModel = cardModels.transform.Find("ShootArrow").gameObject.GetComponent<Image>();
    }
    
    public override bool UseCard(Vector2 charPos, Vector2 target, int team)
    {

        if (team == 0)
        {
            Character hero = GameObject.Find("Hero").GetComponent<Character>();
            if (hero.mana < cost)
            {
                return false;
            }
            hero.ChangeMana(-cost);
        }
        
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

        return true;
    }
}


public class RangersNet : Card {
    public RangersNet() : base() {
        rarity = Rarity.Common;
        cost = 2;
        GameObject cardModels = GameObject.Find("CardModels");
        cardModel = cardModels.transform.Find("RangersNet").gameObject.GetComponent<Image>();
    }
    
    public override bool UseCard(Vector2 charPos, Vector2 target, int team)
    {

        if (team == 0)
        {
            Character hero = GameObject.Find("Hero").GetComponent<Character>();
            if (hero.mana < cost)
            {
                return false;
            }
            hero.ChangeMana(-cost);
        }
        
        models = GameObject.Find("AttackModels");
        GameObject arrowModel = models.transform.Find("RangersNet").gameObject;
        GameObject arrowObject = new GameObject("Net");
        
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
        
        Net arrowInstance = arrowObject.AddComponent<Net>();
        arrowObject.transform.SetParent(GameObject.Find("MainCanvas/Projectiles").transform);
        
        arrowInstance.transform.position = charPos;
        Vector3 direction = (charPos - target).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        arrowInstance.CreateProjectile(charPos, target, team);

        return true;
    }
}






public class MinorHeal : Card {
    public MinorHeal() : base() {
        rarity = Rarity.Common;
        cost = 2;
        GameObject cardModels = GameObject.Find("CardModels");
        cardModel = cardModels.transform.Find("MinorHeal").gameObject.GetComponent<Image>();
    }
    
    public override bool UseCard(Vector2 charPos, Vector2 target, int team)
    {

        if (team == 0)
        {
            Character hero = GameObject.Find("Hero").GetComponent<Character>();
            if (hero.mana < cost)
            {
                return false;
            }
            hero.ChangeMana(-cost);
        }
        
        models = GameObject.Find("AttackModels");
        GameObject arrowModel = models.transform.Find("MinorHeal").gameObject;
        GameObject arrowObject = new GameObject("Net");
        
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
        
        MinorHealShot arrowInstance = arrowObject.AddComponent<MinorHealShot>();
        arrowObject.transform.SetParent(GameObject.Find("MainCanvas/Projectiles").transform);
        
        arrowInstance.transform.position = charPos;
        Vector3 direction = (charPos - target).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        arrowInstance.CreateProjectile(charPos, target, team);

        return true;
    }
}




public class MajorHeal : Card {
    public MajorHeal() : base() {
        rarity = Rarity.Common;
        cost = 4;
        GameObject cardModels = GameObject.Find("CardModels");
        cardModel = cardModels.transform.Find("MajorHeal").gameObject.GetComponent<Image>();
    }
    
    public override bool UseCard(Vector2 charPos, Vector2 target, int team)
    {

        if (team == 0)
        {
            Character hero = GameObject.Find("Hero").GetComponent<Character>();
            if (hero.mana < cost)
            {
                return false;
            }
            hero.ChangeMana(-cost);
        }
        
        models = GameObject.Find("AttackModels");
        GameObject arrowModel = models.transform.Find("MajorHeal").gameObject;
        GameObject arrowObject = new GameObject("MajorHeal");
        
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
        
        MajorHealShot arrowInstance = arrowObject.AddComponent<MajorHealShot>();
        arrowObject.transform.SetParent(GameObject.Find("MainCanvas/Projectiles").transform);
        
        arrowInstance.transform.position = charPos;
        Vector3 direction = (charPos - target).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        arrowInstance.CreateProjectile(charPos, target, team);

        return true;
    }
}



public class SwordSlash : Card {
    public SwordSlash() : base() {
        rarity = Rarity.Common;
        cost = 1;
        GameObject cardModels = GameObject.Find("CardModels");
        cardModel = cardModels.transform.Find("SwordSlash").gameObject.GetComponent<Image>();
    }
    
    
    public override bool UseCard(Vector2 charPos, Vector2 target, int team)
    {
        Character hero = GameObject.Find("Hero").GetComponent<Character>();
        if (team == 0)
        {
            
            if (hero.mana < cost)
            {
                return false;
            }
            hero.ChangeMana(-cost);
        }
        
        models = GameObject.Find("AttackModels");
        GameObject arrowModel = models.transform.Find("SwordSlash").gameObject;
        GameObject arrowObject = new GameObject("SwordSlash");
        
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
        
        
        Vector2 pivotOffset = modelTransform.pivot - objectTransform.pivot;
        Vector2 newPosition = objectTransform.anchoredPosition - new Vector2(
                                  pivotOffset.x * objectTransform.rect.width,
                                  pivotOffset.y * objectTransform.rect.height
                              );
        objectTransform.anchoredPosition = newPosition;

        // Set the new pivot
        objectTransform.pivot = modelTransform.pivot;
        
        arrowObject.transform.SetParent(GameObject.Find("MainCanvas/TempVisuals").transform);
        
        arrowObject.transform.position = charPos;
        
        Vector3 direction = (charPos - target).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (team == 0)
        {
            GameObject NPCs = GameObject.Find("NPCs");
            foreach (Transform child in NPCs.transform)
            {
                if (Vector2.SqrMagnitude(child.position - hero.transform.position) < 10000)
                {
                    System.Random random = new System.Random();
                    int damage = random.Next(12, 15 + 1) * -1;
                    child.gameObject.GetComponent<NPC>().ChangeHealth(damage);
                }
            }
        }

        Object.Destroy(arrowObject, 0.5f);
        
        return true;
    }
}




public class MightySlash : Card {
    public MightySlash() : base() {
        rarity = Rarity.Common;
        cost = 3;
        GameObject cardModels = GameObject.Find("CardModels");
        cardModel = cardModels.transform.Find("MightySlash").gameObject.GetComponent<Image>();
    }
    
    
    public override bool UseCard(Vector2 charPos, Vector2 target, int team)
    {
        Character hero = GameObject.Find("Hero").GetComponent<Character>();
        if (team == 0)
        {
            
            if (hero.mana < cost)
            {
                return false;
            }
            hero.ChangeMana(-cost);
        }
        
        models = GameObject.Find("AttackModels");
        GameObject arrowModel = models.transform.Find("MightySlash").gameObject;
        GameObject arrowObject = new GameObject("MightySlash");
        
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
        
        
        Vector2 pivotOffset = modelTransform.pivot - objectTransform.pivot;
        Vector2 newPosition = objectTransform.anchoredPosition - new Vector2(
                                  pivotOffset.x * objectTransform.rect.width,
                                  pivotOffset.y * objectTransform.rect.height
                              );
        objectTransform.anchoredPosition = newPosition;

        // Set the new pivot
        objectTransform.pivot = modelTransform.pivot;
        
        arrowObject.transform.SetParent(GameObject.Find("MainCanvas/TempVisuals").transform);
        
        arrowObject.transform.position = charPos;
        
        Vector3 direction = (charPos - target).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (team == 0)
        {
            GameObject NPCs = GameObject.Find("NPCs");
            foreach (Transform child in NPCs.transform)
            {
                if (Vector2.SqrMagnitude(child.position - hero.transform.position) < 10000)
                {
                    System.Random random = new System.Random();
                    int damage = random.Next(7, 12 + 1) * -1;
                    child.gameObject.GetComponent<NPC>().ChangeHealth(damage);
                }
            }
        }

        Object.Destroy(arrowObject, 0.5f);
        
        return true;
    }
}




public class BlackHole : Card {
    public BlackHole() : base() {
        rarity = Rarity.Common;
        cost = 2;
        GameObject cardModels = GameObject.Find("CardModels");
        cardModel = cardModels.transform.Find("BlackHole").gameObject.GetComponent<Image>();
    }
    
    public override bool UseCard(Vector2 charPos, Vector2 target, int team)
    {

        if (team == 0)
        {
            Character hero = GameObject.Find("Hero").GetComponent<Character>();
            if (hero.mana < cost)
            {
                return false;
            }
            hero.ChangeMana(-cost);
        }
        
        models = GameObject.Find("AttackModels");
        GameObject arrowModel = models.transform.Find("BlackHole").gameObject;
        GameObject arrowObject = new GameObject("BlackHole");
        
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
        
        BlackHoleArea arrowInstance = arrowObject.AddComponent<BlackHoleArea>();
        arrowObject.transform.SetParent(GameObject.Find("MainCanvas/AreasOfEffect").transform);
        
        arrowInstance.transform.position = target;
        Vector3 direction = (charPos - target).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrowObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        
        arrowInstance.CreateArea(target, team);

        return true;
    }
}