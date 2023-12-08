using System;
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
        cards = new List<Card>()
        {
            new ShootArrow(),
            new ShootArrow(),
            new ShootArrow(),
            new ShootArrow(),
            new ShootArrow(),
            new ShootArrow()
        };
        maxHealth = 40;
        health = maxHealth;
        maxMana = 2;
        mana = maxMana;
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
                    StartCoroutine(DelayedPrompt("abg", "dasd", "www"));
                }
                else
                {
                    DoDialogue("I hate you");
                }
                break;
            
            case 1:
                DoDialogue("If you wanna do the quest let me know!");
                //PromptDialogue();
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

    public override void PromptAnswer(int answer)
    {
        switch (state)
        {
            case 0:
                if (reputation > 40)
                {
                    if (answer == 1)
                    {
                        reputation = Math.Min(reputation + 10, 100);
                        state = 1;
                    }
                    else if (answer == 2)
                    {
                        reputation = Math.Max(reputation - 10, 100);
                        state = 1;
                        StartCombat();
                    }
                    else
                    {
                        state = 2;
                        //Code to add quest to quest list
                    }
                }
                else
                {

                }
                break;
            
            case 1:
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
        DetermineDialogue();
    }

    IEnumerator DelayedPrompt(string m1, string m2, string m3)
    {
        yield return new WaitForSeconds(2f);
        PromptDialogue(m1, m2, m3);
    }
}
