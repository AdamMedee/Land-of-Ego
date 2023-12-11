using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mabel : NPC
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
        maxHealth = 4;
        health = maxHealth;
        maxMana = 2;
        mana = maxMana;
        Debug.Log("Mabel NPC initialized.");
    }

    public override void DetermineDialogue()
    {

        switch (state)
        {
            case 0:
                DoDialogue("*You see a little girl playing with the leaves, unbeknown to your presence. She seems to be talking to herself, lost in her own world.*");
                StartCoroutine(DelayedPrompt("Hey, excuse me.", "*Leave*", "*Attack*"));
                break;
            
            case 1:
                DoDialogue("*She takes a minute to notice you* Oh, hello.");
                StartCoroutine(DelayedPrompt("What are you doing all the way out here?", "Are you Mabel? Your brother is looking for you.", "*Leave*"));
                break;

            case 2:
                DoDialogue("It's none of YOUR business, stupid head.");
                StartCoroutine(DelayedPrompt("Hey, your brother is worried sick. Come on, let's go.", "*Leave*", "I won't allow you to speak to me like that. *Attack the toddler*"));
                break;
            
            case 3:
                DoDialogue("Ugh, fine. *She leaves to where Todd is*");
                //Finish quest 2
                Destroy(gameObject);
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
                        state = 2;
                        DetermineDialogue();
                        break;
                    
                    case 2:
                        state = 2;
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
                        state = 3;
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
        yield return new WaitForSeconds(2f);
        PromptDialogue(m1, m2, m3);
    }
}
