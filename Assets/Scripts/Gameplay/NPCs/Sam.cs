using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamNPC : NPC
{
    public override void Init()
    {
        Init();
        race = "Human";
        Debug.Log("Sam NPC initialized.");
    }
    

    public override void DetermineDialogue()
    {
        
    }
    
}