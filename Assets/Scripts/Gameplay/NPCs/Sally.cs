using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SallyNPC : NPC
{
    public override void Init()
    {
        race = "Slime";
        cards = new List<Card>()
        {
            new MajorHeal(),
            new MajorHeal(),
            new MajorHeal(),
            new MajorHeal()
        };
        maxHealth = 10;
        health = maxHealth;
        maxMana = 2;
        mana = maxMana;
        Debug.Log("Sally NPC initialized.");
    }

    public override void DetermineDialogue()
    {

        switch (state)
        {
            case 0:
                DoDialogue("Why, hey there sugar! My name's Sally, Sally the slime. You don't look like you belong here, what brings you to these parts?"); 
                StartCoroutine(DelayedPrompt("Honestly? I don't know, I don't remember much.", "That is none of your business.", "*Leave*"));
                break;
            
            case 1:
                DoDialogue("Well, this is the Land of Ego! A nice lil home, but things haven't been great recently. Some people just don't know how to keep to themselves. Do you wanna help?");
                StartCoroutine(DelayedPrompt("Sure!", "I'm one of those people.", "*Leave*"));
                break;
            
            case 2:
                DoDialogue("Well, there are a couple kingdoms here. There's Mutus in the West, and Stultum in the East. They're at war over somethin' silly, one king took another kinds toy.");
                StartCoroutine(DelayedPrompt("How can I help?", "Do not speak of royalty like that. ", "*Leave*"));
                break;
            
            case 3:
                DoDialogue("Go find the stolen artifact. I've known these people since they were in diapers. No one stole anything from anyone. Find it and end this kerfuffle once and fer all");
                GiveQuest(1);
                state = 8;
                break;

            case 4:
                DoDialogue("I suggest looking North. FAR North. There are some, unnatural forces at work there.");
                break;
            
            case 5:
                DoDialogue("Ya did it! Good job sugar, I always believed in you. Now, we can finally rest.");
                break;
            
            case 6:
                DoDialogue("Well, I'm sorry for offending you.");
                state = 7;
                break;
            
            case 7:
                DoDialogue("Hi.");
                StartCoroutine(DelayedPrompt("Hey.", "*Attack*", "*Leave*"));
                break;
            
            case 8:
                DoDialogue("Hey! I hope you're doing well on your quest.");
                StartCoroutine(DelayedPrompt("It's going alright.", "*Attack*", "*Leave*"));
                break;
            
            case 9:
                DoDialogue("You did it! The Land of Ego is saved.");
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
                        state = 6;
                        DetermineDialogue();
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
                        state = 3;
                        DetermineDialogue();
                        break;
                    case 2:
                        state = 6;
                        DetermineDialogue();
                        break;
                    default:
                        return;
                }

                break;
            
            case 7:
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

            case 8:
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
