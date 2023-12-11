using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Todd : NPC
{
    public override void Init()
    {
        race = "Human";
        cards = new List<Card>()
        {
            new SwordSlash(),
            new SwordSlash(),
            new SwordSlash(),
            new SwordSlash()
        };
        maxHealth = 10;
        health = maxHealth;
        maxMana = 2;
        mana = maxMana;
        Debug.Log("Todd NPC initialized.");
    }

    public override void DetermineDialogue()
    {

        switch (state)
        {
            case 0:
                DoDialogue("Oh finally, someone who could possible help me. Please, I really need your assistance. I'm Todd. Do you have the time to spare?");
                StartCoroutine(DelayedPrompt("Of course, what do you need?", "I don't.", "*Attack*"));
                break;
            
            case 1:
                DoDialogue("It's my younger sister Mabel, she ran off away from the group and I can't find her. Could you find her and tell her where we are?");
                StartCoroutine(DelayedPrompt("Sure, I'll look around.", "You lost your sister? This is on you.", "*Leave*"));
                break;

            case 2:
                DoDialogue("Hey, change your mind?");
                StartCoroutine(DelayedPrompt("Yeah, what do you need?", "No.", "*Attack*"));
                break;
            
            case 3:
                DoDialogue("Oh thank you so much! Good luck.");
                state = 5;
                GiveQuest(2);
                break;
            
            case 4:
                DoDialogue("I know, I'm a terrible brother. Well, good luck on your travels.");
                state = 2;
                break;
            
            case 5:
                DoDialogue("Hey, hope it's going well. Find her soon, will you?");
                break;
            
            case 6:
                DoDialogue("Thank you so much for finding her! This means a lot.");
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
                        state = 2;
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
                        state = 3;
                        DetermineDialogue();
                        break;
                    
                    case 2:
                        state = 4;
                        DetermineDialogue();
                        break;
                    
                    default:
                        return;
                }

                break;
            
            case 2:
                switch (answer)
                {
                    case 1:
                        state = 1;
                        DetermineDialogue();
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
        yield return new WaitForSeconds(3f);
        PromptDialogue(m1, m2, m3);
    }
}
