using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DarkCommander : NPC
{
    public override void Init()
    {
        race = "Human";
        cards = new List<Card>()
        {
            new MightySlash(),
            new MightySlash(),
            new MightySlash(),
            new MightySlash()
        };
        maxHealth = 100;
        health = maxHealth;
        maxMana = 2;
        mana = maxMana;
        Debug.Log("DarkCommander NPC initialized.");
    }

    public override void DetermineDialogue()
    {
        
        switch (state)
        {
            case 0:
                DoDialogue("I am the Dark Commander. Kneel before me, underling.");
                StartCoroutine(DelayedPrompt("*Kneel*", "Um, I think I walked into the wrong place. *Leave*", "Over my dead body. *Attack*"));
                break;
            
            case 1:
                DoDialogue("If you wish to help my cause, go South. Get rid of that slime. They are the force that powers this forest, that energizes it.");
                GiveQuest(8);
                StartCoroutine(DelayedPrompt("I am at your service.", "You want to attack that poor lady? No shot. *Attack*", "Actually, nevermind. *Leave*"));
                break;

            case 2:
                DoDialogue("What's taking so long?");
                StartCoroutine(DelayedPrompt("I'll get back to it.", "Just trying to be careful about it.", "*Attack*"));
                break;
            
            case 3:
                DoDialogue("You have killed the disease. Now, darkness shall rule.");
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
                switch (answer)
                {
                    case 1:
                        state = 1;
                        DetermineDialogue();
                        break;
                    
                    case 2:
                        state = 1;
                        break;
                    
                    case 3:
                        StartCombat();
                        break;
                    
                    default:
                        return;
                }

                break;
            
            case 1:
                switch (answer)
                {
                    case 1:
                        break;
                    
                    case 2:
                        StartCombat();
                        break;

                    default:
                        return;
                }

                break;
            
            case 2:
                switch (answer)
                {
                    case 1:
                        break;
                    
                    case 2:
                        break;
                    
                    case 3:
                        StartCombat();
                        break;
                    
                    default:
                        return;
                }

                break;
            default:
                return;
        }
    }

    IEnumerator DelayedPrompt(string m1, string m2, string m3)
    {
        yield return new WaitForSeconds(2f);
        PromptDialogue(m1, m2, m3);
    }
}
