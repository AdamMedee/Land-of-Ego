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
    private GameObject model;
    private Animator animator;
    public bool paused;
    public bool myTurn;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 140;
        PauseMenu = GameObject.Find("MainCanvas").transform.Find("PauseMenu").gameObject;
        paused = false;
        model = transform.Find("Model").gameObject;
        animator = model.GetComponent<Animator>();
        myTurn = false;
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
                // Get the direction to the target
                Vector2 direction = TargetPoint - new Vector2(transform.position.x, transform.position.y);

                // Calculate the rotation angle in degrees
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

                // Apply the rotation to the GameObject
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                moving = true;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                //Card c = new ShootArrow();
                //c.UseCard(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));;
            }

            animator.SetBool("IsIdle", !moving);
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
