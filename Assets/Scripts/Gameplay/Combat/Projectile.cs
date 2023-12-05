using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D bc;

    public void Start()
    {

    }

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
    }

    public abstract void CreateProjectile(Vector2 charPos, Vector2 target);
}


public class Arrow : Projectile
{
    public void Start()
    {
        base.Start();
        gameObject.AddComponent<Image>();
    }

    public override void CreateProjectile(Vector2 charPos, Vector2 target)
    {
        init();
        Vector2 direction = (target - charPos).normalized;
        rb.velocity = direction * 400;
    }
}