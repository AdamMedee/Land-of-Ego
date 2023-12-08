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

    public abstract void CreateProjectile(Vector2 charPos, Vector2 target, int t);

    public void KillSelf()
    {
        Destroy(gameObject);
    }
}


public class Arrow : Projectile
{
    public void Start()
    {

    }

    public override void CreateProjectile(Vector2 charPos, Vector2 target, int t)
    {
        init();
        Vector2 direction = (target - charPos).normalized;
        rb.velocity = direction * 400;
        team = t;
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



public class Net : Projectile
{
    public void Start()
    {

    }

    public override void CreateProjectile(Vector2 charPos, Vector2 target, int t)
    {
        init();
        Vector2 direction = (target - charPos).normalized;
        rb.velocity = direction * 400;
        rb.angularVelocity = 150;
        team = t;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Initialize the System.Random object

        // Generate a random integer within the specified range
        if (other.gameObject.name.Equals("Hero") && team == 1)
        {
            Character a = other.gameObject.GetComponent<Character>();
            a.rooted = 2;
            KillSelf();
        }
        else if (other.gameObject.transform.parent.gameObject.name.Equals("NPCs") && team == 0)
        {
            NPC a = other.gameObject.GetComponent<NPC>();
            a.rooted = 2;
            KillSelf();
        }
    }
}



public class MinorHealShot : Projectile
{
    public void Start()
    {

    }

    public override void CreateProjectile(Vector2 charPos, Vector2 target, int t)
    {
        init();
        Vector2 direction = (target - charPos).normalized;
        rb.velocity = direction * 400;
        rb.angularVelocity = 150;
        team = t;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Initialize the System.Random object
        System.Random random = new System.Random();

        // Generate a random integer within the specified range
        int damage = random.Next(2, 5 + 1);
        // Generate a random integer within the specified range
        if (other.gameObject.name.Equals("Hero") && team == 0)
        {
            Character a = other.gameObject.GetComponent<Character>();
            a.ChangeHealth(damage);
            KillSelf();
        }
        else if (other.gameObject.transform.parent.gameObject.name.Equals("NPCs") && team == 1)
        {
            NPC a = other.gameObject.GetComponent<NPC>();
            a.ChangeHealth(damage);
            KillSelf();
        }
    }
}



public class MajorHealShot : Projectile
{
    public void Start()
    {

    }

    public override void CreateProjectile(Vector2 charPos, Vector2 target, int t)
    {
        init();
        Vector2 direction = (target - charPos).normalized;
        rb.velocity = direction * 400;
        rb.angularVelocity = 150;
        team = t;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Initialize the System.Random object
        System.Random random = new System.Random();

        // Generate a random integer within the specified range
        int damage = random.Next(5, 11 + 1);
        // Generate a random integer within the specified range
        if (other.gameObject.name.Equals("Hero") && team == 0)
        {
            Character a = other.gameObject.GetComponent<Character>();
            a.ChangeHealth(damage);
            KillSelf();
        }
        else if (other.gameObject.transform.parent.gameObject.name.Equals("NPCs") && team == 1)
        {
            NPC a = other.gameObject.GetComponent<NPC>();
            a.ChangeHealth(damage);
            KillSelf();
        }
    }
}

