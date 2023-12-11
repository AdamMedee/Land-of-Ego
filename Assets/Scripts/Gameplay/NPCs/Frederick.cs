using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Frederick : NPC
{
    public override void Init()
    {
        race = "Human";
        cards = new List<Card>()
        {
            new ShootArrow(),
            new ShootArrow(),
            new RangersNet(),
            new RangersNet()
        };
        maxHealth = 10;
        health = maxHealth;
        maxMana = 2;
        mana = maxMana;
        Debug.Log("Frederick NPC initialized.");
    }

    public override void DetermineDialogue()
    {

        switch (state)
        {
            case 0:
                DoDialogue("Yo, I'm Frederick. Hows you doing?");
                break;

            default:
                return;
        }

    }

    public override void PromptAnswer(int answer)
    {
        switch (state)
        {
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
