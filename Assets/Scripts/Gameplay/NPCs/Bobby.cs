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
            new BlackHole()
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
                DoDialogue("Howdy!"); 
                StartCoroutine(DelayedPrompt("Hey!", "You will die.", "*Leave*"));
                break;
            
            case 1:
                DoDialogue("I HATE these guys. Can you go kill them?");
                StartCoroutine(DelayedPrompt("Sure!", "No.", "*Leave*"));
                break;
            
            case 2:
                DoDialogue("Thanks! Good luck!");
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
                if (answer == 1)
                {
                    state = 1;
                    DetermineDialogue();
                }
                else if (answer == 2)
                {
                    StartCombat(); 
                }
                break;
            
            case 1:
                if (answer == 1)
                {
                    state = 2;
                    GiveQuest(1);
                    DetermineDialogue();
                }
                else if (answer == 2)
                {
                    StartCombat(); 
                }
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

    IEnumerator DelayedPrompt(string m1, string m2, string m3)
    {
        yield return new WaitForSeconds(3f);
        PromptDialogue(m1, m2, m3);
    }
}
