using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stul : NPC
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
        maxHealth = 30;
        health = maxHealth;
        maxMana = 2;
        mana = maxMana;
        Debug.Log("Stul NPC initialized.");
    }

    public override void DetermineDialogue()
    {

        switch (state)
        {
            case 0:
                DoDialogue("I am King Stul. What matters do you have here with me?");
                StartCoroutine(DelayedPrompt("I'm hear to help your cause.", "Nothing. *Leave*", "Today is the day you die. *Attack*"));
                break;
            
            case 1:
                DoDialogue("If you wish to help my cause, go East. Kill that thieving fool, and bring me his head.");
                GiveQuest(5);
                StartCoroutine(DelayedPrompt("I am at your service.", "It's the other way around. I'm taking your head. *Attack*", "Actually, nevermind. *Leave*"));
                break;

            case 2:
                DoDialogue("What's taking so long?");
                StartCoroutine(DelayedPrompt("I'll get back to it.", "Just trying to be careful about it.", "*Attack*"));
                break;
            
            case 3:
                DoDialogue("You have killed the traitor. Thank you for bringing us glory.");
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
