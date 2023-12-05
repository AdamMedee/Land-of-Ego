using System;
using UnityEngine;



// Common implementation for NPC behaviors
public abstract class NPC : MonoBehaviour
{
    protected int reputation = 50; // Is out of 100, 0 they hate you 100 they love you
    protected int state = 0;
    private GameObject hero = null;
    protected string race = "None";

    private bool targetted = false;
    // An abstract "state" idea, lets say you've never spoken to someone, state would be 0, you talk to them it becomes 1, you
    //do a quest it becomes 2. It helps in determining the dialogue tree

    void Start()
    {
        hero = GameObject.Find("Hero");
        Init();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 TargetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            if ((TargetPoint - new Vector2(transform.position.x, transform.position.y)).magnitude < 100)
            {
                targetted = true;
            }
            else
            {
                targetted = false;
            }
        }

        if (targetted)
        {
            if ((hero.transform.position - transform.position).magnitude < 100)
            {
                DetermineDialogue();
                targetted = false;
            }
        }
        
    }

    public abstract void DetermineDialogue();

    public void DoDialogue(string message)
    {
        // This needs to be changed to displaying the message on the actual screen
        print(message);
    }

    public abstract void Init();

}

// Implement specific NPC types


