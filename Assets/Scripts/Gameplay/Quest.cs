using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string target = "Human"; //Set to X if its not a hunting quest
    public int count = 5;


    public Quest(int questID)
    {
        switch (questID)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                return;
        }
    }
    
    
}



