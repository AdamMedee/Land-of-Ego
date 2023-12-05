using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbyNPC : NPC
{
    //State summary
    //0 - Have not talked to them yet
    //1 - Talked to them but no quest
    //2 - Talked to them and took the quest
    //3 - Finished the quest
    //4 - Finished the quest and took the rewards
    public override void Init()
    {
        race = "Elf";
        Debug.Log("Bobby NPC initialized.");
    }

    public override void DetermineDialogue()
    {

        switch (state)
        {
            case 0:
                if (reputation > 40)
                {
                    DoDialogue("Howdy!");
                }
                else
                {
                    DoDialogue("I hate you");
                }
                break;
            
            case 1:
                DoDialogue("If you wanna do the quest let me know!");
                break;
            
            case 2:
                break;
            
            case 3:
                break;
            
            case 4:
                break;
            
            case 5:
                break;
            
            default:
                return;
        }

    }


}
