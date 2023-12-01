using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Vector2 TargetPoint;
    private bool moving;
    private Rigidbody2D rb;
    private int speed;
    private GameObject PauseMenu;
    public bool paused;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 140;
        PauseMenu = GameObject.Find("MainCanvas").transform.Find("PauseMenu").gameObject;
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf);

        }
        if (PauseMenu.activeSelf)
        {
            Time.timeScale = 0;
            paused = true;
        }
        else
        {
            Time.timeScale = 1;
            paused = false;
        }
        
        if (!paused)
        {
            if (Input.GetMouseButtonDown(0))
            {
                TargetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                moving = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!paused)
        {
            // Handle movement
            if (moving && Vector2.Distance(TargetPoint, transform.position) < 10)
            {
                moving = false;
                rb.velocity = Vector2.zero;
            }
            else if(moving)
            {
                Vector2 moveDirection = TargetPoint - (Vector2)transform.position;
                moveDirection.Normalize();
                rb.velocity = speed * moveDirection;
            }
        }
    }
    
    
}
