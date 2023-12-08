using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;



public abstract class AreaOfEffect : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    protected GameObject npcs;
    protected GameObject hero;
    protected int team;
    public int lifespan;
    
    public void Update()
    {
        if (transform.position.x < -1000 || transform.position.x > 1000 || transform.position.y < -1000 ||
            transform.position.y > 1000 || lifespan <= 0)
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

    public abstract void CreateArea(Vector2 target, int t);

    public abstract void ProcArea();

    public void KillSelf()
    {
        Destroy(gameObject);
    }
}


public class BlackHoleArea : AreaOfEffect
{
    public void Start()
    {

    }

    public override void CreateArea(Vector2 target, int t)
    {
        init();
        team = t;
        lifespan = 3;
        rb.angularVelocity = 100;
    }

    public override void ProcArea()
    {
        if (team == 0)
        {
            foreach (Transform child in npcs.transform)
            {
                if (Vector2.SqrMagnitude(child.position - transform.position) < 62500)
                {
                    child.position = 0.25f*(3*transform.position + child.gameObject.transform.position);
                }
            }
        }
    }

}