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
                title = "Speak to Searcy";
                description = "Go and speak to Sallys twin sister, Searcy";
                id = 1;
                break;
            case 2:
                title = "Kill the mercenaries";
                description = "Kill all the mercsaa sdhh dsufhsudfhui udhfuidhfui sdufhuifhsd sdufhsdf sdufhsd sudfhuidhfiusd hsfduhufh sdufhdsuhfs hsduifhsdhuf sdufhishf sdufhsui dsufh";
                id = 2;
                break;
            case 3:
                break;
            default:
                return;
        }
    }

    private void FinishedQuest()
    {
        
    }
    
    
}



