using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


// Common implementation for NPC behaviors
public abstract class NPC : MonoBehaviour
{
    protected int reputation = 50; // Is out of 100, 0 they hate you 100 they love you
    public int state = 0;
    private GameObject hero = null;
    protected string race = "None";
    protected static GameObject dialogueBoxTemplate;
    protected static GameObject dialoguePromptTemplate;
    private bool targetted = false;
    protected int health;
    protected int maxHealth;
    protected int mana;
    protected int maxMana;
    protected GameObject healthBar;
    protected List<Card> cards;
    protected CombatManager combatManager;
    public int rooted;
    protected Character cha;
    public bool myTurn;

    private SoundEffectsManager sfxManager;
    // An abstract "state" idea, lets say you've never spoken to someone, state would be 0, you talk to them it becomes 1, you
    //do a quest it becomes 2. It helps in determining the dialogue tree

    void Start()
    {
        myTurn = false;
        rooted = 0;
        Init();
        hero = GameObject.Find("Hero");
        cha = hero.GetComponent<Character>();
        if (!dialogueBoxTemplate)
        {
            dialogueBoxTemplate = GameObject.Find("DialogueBoxes").transform.Find("DialogueBoxTemplate").gameObject;
        }
        if (!dialoguePromptTemplate)
        {
            dialoguePromptTemplate = GameObject.Find("DialoguePrompts").transform.Find("DialoguePromptTemplate").gameObject;
        }
        healthBar = transform.Find("HealthBar").gameObject;
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        ChangeHealth(0);
    }

    void Update()
    {
        if (!combatManager.fighting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 TargetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                if ((TargetPoint - new Vector2(transform.position.x, transform.position.y)).magnitude < 100)
                {
                    targetted = true;
                }
                else
                {
                    targetted = false;
                }
            }

            if (targetted)
            {
                if ((hero.transform.position - transform.position).magnitude < 100)
                {
                    if (!combatManager.fighting)
                    {
                        Character cha = hero.GetComponent<Character>();
                        cha.inDialogue = true;
                        DetermineDialogue();
                        targetted = false;
                    }
                }
            }
        }


        
        if (myTurn)
        {
            myTurn = false;
            StartCoroutine(DelayedCombat());
            
        }
        
    }



    public abstract void DetermineDialogue();

    public void DoDialogue(string message)
    {
        cha.inDialogue = true;
        Transform parentTransform = GameObject.Find("DialogueBoxes").transform;
        if (parentTransform.childCount < 2)
        {
            GameObject duplicatedObject = Instantiate(dialogueBoxTemplate, parentTransform);
            GameObject messageObject = duplicatedObject.transform.Find("Message").gameObject;
            TextMeshProUGUI text = messageObject.GetComponent<TextMeshProUGUI>();
            text.text = message;
            duplicatedObject.SetActive(true);
            //duplicatedObject.transform.position = new Vector3(0f, -320f, 0f);
            Invoke("DeleteDialogue", 3f);
        }
    }

    public void DeleteDialogue()
    {
        GameObject dialogue = GameObject.Find("DialogueBoxes").transform.GetChild(1).gameObject;
        Character cha = hero.GetComponent<Character>();
        cha.inDialogue = false;
        Destroy(dialogue);
    }

    // A single 
    public void PromptDialogue(string message1, string message2, string message3)
    {
        //type -1 -> nothing, 0 -> message, 1 -> quest, 2 -> combat, 3 -> shop
        // Check if the dialogue prompt prefab is assigned
        Character cha = hero.GetComponent<Character>();
        cha.inDialogue = true;
        Transform parentTransform = GameObject.Find("DialoguePrompts").transform;
        GameObject dialoguePromptObject = Instantiate(dialoguePromptTemplate, parentTransform);

        GameObject Prompt1 = dialoguePromptObject.transform.Find("Prompt1").gameObject;
        GameObject Prompt2 = dialoguePromptObject.transform.Find("Prompt2").gameObject;
        GameObject Prompt3 = dialoguePromptObject.transform.Find("Prompt3").gameObject;

        GameObject MObject1 = Prompt1.transform.Find("Message").gameObject;
        GameObject MObject2 = Prompt2.transform.Find("Message").gameObject;
        GameObject MObject3 = Prompt3.transform.Find("Message").gameObject;
        
        TextMeshProUGUI text1 = MObject1.GetComponentInChildren<TextMeshProUGUI>();
        text1.text = message1;
        
        TextMeshProUGUI text2 = MObject2.GetComponentInChildren<TextMeshProUGUI>();
        text2.text = message2;
        
        TextMeshProUGUI text3 = MObject3.GetComponentInChildren<TextMeshProUGUI>();
        text3.text = message3;

        Button button1 = Prompt1.GetComponent<Button>();
        Button button2 = Prompt2.GetComponent<Button>();
        Button button3 = Prompt3.GetComponent<Button>();

        button1.onClick.AddListener(() => PromptAnswer(1));
        button2.onClick.AddListener(() => PromptAnswer(2));
        button3.onClick.AddListener(() => PromptAnswer(3));
        
        button1.onClick.AddListener(() => Destroy(dialoguePromptObject));
        button2.onClick.AddListener(() => Destroy(dialoguePromptObject));
        button3.onClick.AddListener(() => Destroy(dialoguePromptObject));

        button1.onClick.AddListener(() => PromptAnswered());
        button2.onClick.AddListener(() => PromptAnswered());
        button3.onClick.AddListener(() => PromptAnswered());
        
        dialoguePromptObject.SetActive(true);
    }

    public abstract void Init();

    public abstract void PromptAnswer(int answer);

    public void PromptAnswered()
    {
        Character cha = hero.GetComponent<Character>();
        cha.inDialogue = false;
    }

    public void DoCombat()
    {
        print("attack!!");
        System.Random random = new System.Random();
        int pick = random.Next(0, 3 + 1);
        Card card = cards[pick];

        if (card.name.Equals("SwordSlash") || card.name.Equals("MightySlash"))
        {
            Vector3 directionToHero = hero.transform.position - transform.position;
            Vector3 normalizedDirection = directionToHero.normalized;
            float distance = Math.Min(200, (hero.transform.position - transform.position).magnitude - 5f);
            Vector3 teleportPosition = transform.position + normalizedDirection * 200;
            transform.position = teleportPosition;
        }
        cards[pick].UseCard(transform.position, hero.transform.position, 1);

    }
    
    IEnumerator DelayedCombat()
    {
        yield return new WaitForSeconds(3f);
        DoCombat();
        combatManager.ChangeTurn();
    }

    public void GiveQuest(int id)
    {
        hero.GetComponent<Character>().quests.Add(new Quest(id));
        hero.GetComponent<Character>().UpdateQuests();
    }

    public void ChangeHealth(int delta)
    {
        health = Math.Min(maxHealth, health + delta);
        if (delta < 0)
        {
            sfxManager = GameObject.Find("SoundEffectsManager").GetComponent<SoundEffectsManager>();
            sfxManager.PlayHurt();
        }
        if (health <= 0)
        {
            KillSelf();
        }
        healthBar.SetActive(health!=maxHealth);
        healthBar.GetComponent<Slider>().value = (float)health / maxHealth;
    }

    public void StartCombat()
    {
        CombatManager.Instance.StartCombat();
    }

    public void KillSelf()
    {
        Destroy(gameObject);
    }
}

// Implement specific NPC types


