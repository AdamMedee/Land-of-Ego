using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public bool finished;
    public string title;
    public string description;
    public int id;

    public Quest(int questID)
    {
        finished = false;
        switch (questID)
        {
            case 1:
                title = "World Peace";
                description = "Find a way to bring an end to the war before things get out of hand.";
                id = 1;
                break;
            case 2:
                title = "Bring Mabel Back";
                description = "Find Mabel and bring her back to her older brother.";
                id = 2;
                break;
            case 4:
                title = "Defend Mutus";
                description = "Kill Stult, bringing an end to the war. Stop the carnage before it begins by only ending a single life, just one power drunk aggressor.";
                id = 4;
                break;
            case 5:
                title = "Avenge Stultum";
                description = "Kill Mut, winning the war for Stultum. Avenge King Stul by ending the thieving fool.";
                id = 5;
                break;
            case 8:
                title = "End Sally";
                description = "End Sally's life. This will bring an end to the positive force protecting the forest from eternal darkness, and the Dark Commander will rule for all eternity.";
                id = 8;
                break;
            default:
                return;
        }
    }

    private void FinishedQuest()
    {
        
    }
    
    
}



