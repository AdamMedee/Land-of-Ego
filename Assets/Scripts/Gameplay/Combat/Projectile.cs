using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    protected GameObject npcs;
    protected GameObject hero;
    protected int team;
    
    public void Update()
    {
        if (transform.position.x < -1000 || transform.position.x > 1000 || transform.position.y < -1000 ||
            transform.position.y > 1000)
        {
            Destroy(gameObject);
        }
        CommonUpdate();
    }
    
    public void init()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.isKinematic = true;
        bc = gameObject.AddComponent<BoxCollider2D>();
        bc.isTrigger = true;
        npcs = GameObject.Find("NPCs");
        hero = GameObject.Find("Hero");
    }

    public abstract void CreateProjectile(Vector2 charPos, Vector2 target, int team);

    public abstract void CommonUpdate();

    public void KillSelf()
    {
        Destroy(gameObject);
    }
}


public class Arrow : Projectile
{
    public void Start()
    {
        gameObject.AddComponent<Image>();
        
    }

    public override void CreateProjectile(Vector2 charPos, Vector2 target, int team)
    {
        init();
        Vector2 direction = (target - charPos).normalized;
        rb.velocity = direction * 400;
        this.team = team;
    }

    public override void CommonUpdate()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Initialize the System.Random object
        System.Random random = new System.Random();

        // Generate a random integer within the specified range
        int damage = random.Next(1, 4 + 1) * -1;
        if (other.gameObject.name.Equals("Hero") && team == 1)
        {
            Character a = other.gameObject.GetComponent<Character>();
            a.ChangeHealth(damage);
            KillSelf();
        }
        else if (other.gameObject.transform.parent.gameObject.name.Equals("NPCs") && team == 0)
        {
            NPC a = other.gameObject.GetComponent<NPC>();
            a.ChangeHealth(damage);
            KillSelf();
        }
    }
}