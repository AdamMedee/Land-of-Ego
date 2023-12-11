using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private Vector2 TargetPoint;
    private bool moving;
    private Rigidbody2D rb;
    private int speed;
    private GameObject PauseMenu;
    private GameObject model;
    private Animator animator;
    private GameObject healthBar;
    private GameObject gameOver;
    public bool paused;
    public bool myTurn;
    public bool fighting;
    private bool isMoving;
    private bool isAttacking;
    public bool alive;
    public bool inDialogue;
    public Sprite crystal;
    private GameObject range;
    private int health;
    private int maxHealth;
    public int mana;
    public int maxMana;
    public int rooted;
    private SoundEffectsManager sfxManager;
    private int pickedCard;
    public List<Quest> quests;
    private GameObject cardHand;
    private GameObject endTurn;
    private int sceneX;
    private int sceneY;
    private List<Card> cards;
    public GameObject mapSpots;

    private CombatManager combatManager;
    // Start is called before the first frame update
    void Start()
    {
        
        
        
        speed = 440;
        cards = new List<Card>()
        {
            new SwordSlash(),
            new MightySlash(),
            new MinorHeal(),
            new MajorHeal(),
            new BlackHole(),
            new ShootArrow(),
            new ShootArrow(),
            new ShootArrow(),
            new ShootArrow()
        };

        quests = new List<Quest>();
        //quests.Add(new Quest(1));
        //quests.Add(new Quest(2));
        
        paused = false;
        myTurn = false;
        inDialogue = false;
        maxHealth = 20;
        health = 20;
        maxMana = 2;
        mana = 2;
        rooted = 0;
        alive = true;
        UpdateManaBar();
        UpdateInventory();
        UpdateQuests();
        rb = GetComponent<Rigidbody2D>();
        model = transform.Find("Model").gameObject;
        animator = model.GetComponent<Animator>();
        endTurn = GameObject.Find("GUI").transform.Find("EndTurn").gameObject;
        PauseMenu = GameObject.Find("MainCanvas").transform.Find("PauseMenu").gameObject;
        healthBar = GameObject.Find("GUI").transform.Find("HealthBar").transform.Find("Bar").gameObject;
        combatManager = GameObject.Find("CombatManager").GetComponent<CombatManager>();
        cardHand = GameObject.Find("GUI").transform.Find("CardHand").gameObject;
        range = transform.Find("RangeCircle").gameObject;
        gameOver = GameObject.Find("MainCanvas").transform.Find("GameOver").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(!PauseMenu.activeSelf);
        }
        if (PauseMenu.activeSelf)
        {
            Time.timeScale = 0;
            paused = true;
        }
        else
        {
            Time.timeScale = 1;
            paused = false;
        }
        
        if (!paused)
        {
            if (!fighting)
            {
                maxMana = 2;
                if (Input.GetMouseButtonDown(0))
                {
                    Vector2 t = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if (!(195 < t.x && t.x < 470 && 380 < t.y && t.y < 460) && !inDialogue) //Check if clicking GUI buttons
                    {
                        TargetPoint = t;
                        Vector2 direction = new Vector2(transform.position.x, transform.position.y) - TargetPoint;
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
                        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                        moving = true;
                    }
                }
            }
            else
            {
                if (myTurn && !endTurn.activeSelf)
                {
                    endTurn.SetActive(true);
                    mana = maxMana;
                    maxMana = Math.Min(maxMana + 1, 6);
                    UpdateManaBar();
                    isMoving = true;
                    if (rooted == 0)
                    {
                        range.SetActive(true);
                    }
                    isAttacking = false;
                }

                if (isAttacking)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector2 targ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        if (!(-400 < targ.x && targ.x < 400 && -525 < targ.y && targ.y < -275))
                        {
                            UseCard(targ);
                        }
                    }
                }
                
                if (isMoving)
                {
                    if (rooted > 0)
                    {
                        isMoving = false;
                        moving = true;
                        isAttacking = true;
                        cardHand.SetActive(true);
                        UpdateHandPics();
                        rooted -= 1;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        Vector2 targ = Camera.main.ScreenToWorldPoint(Input.mousePosition);



                            if (Vector2.SqrMagnitude(targ - new Vector2(transform.position.x, transform.position.y)) < 300*300)
                            {
                                Vector2 direction = new Vector2(transform.position.x, transform.position.y) - targ;
                                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
                                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                                range.SetActive(false);
                                TargetPoint = targ;
                                isMoving = false;
                                moving = true;
                                isAttacking = true;
                                cardHand.SetActive(true);
                                UpdateHandPics();
                            }
                        
                    }
                }
            }


            animator.SetBool("IsIdle", !moving || inDialogue);
        }
    }

    private void FixedUpdate()
    {
        if (!paused)
        {
            // Handle movement
            if (moving && Vector2.Distance(TargetPoint, transform.position) < 10)
            {
                moving = false;
                rb.velocity = Vector2.zero;
            }
            else if(moving)
            {
                Vector2 moveDirection = TargetPoint - (Vector2)transform.position;
                moveDirection.Normalize();
                rb.velocity = speed * moveDirection;
            }
        }
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
        healthBar.GetComponent<Slider>().value = (float)health / maxHealth;
    }

    public void ChangeMana(int delta)
    {
        mana += delta;
        UpdateManaBar();
    }

    public void UpdateManaBar()
    {
        GameObject bar = GameObject.Find("Crystals");
        foreach (Transform child in bar.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < mana; i++)
        {
            GameObject crys = new GameObject("Crystal" + i);
            crys.transform.SetParent(bar.transform);
            crys.AddComponent<Image>();
            crys.GetComponent<Image>().sprite = crystal;
            crys.transform.position = new Vector3(bar.transform.position.x + 30 + 30*i, bar.transform.position.y, bar.transform.position.z);
            RectTransform rectTransform = crys.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(25, 35);
        }
    }

    public void KillSelf()
    {
        alive = false;
        gameOver.SetActive(true);
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    

    public void EndCombat()
    {
        endTurn.SetActive(false);
        cardHand.SetActive(false);
        isAttacking = false;
        isMoving = false;
        myTurn = false;
        range.SetActive(false);
        combatManager.ChangeTurn();
        rooted = 0;
    }


    public void PickCard(int cardNum)
    {
        string val = "Card" + cardNum;
        foreach (Transform child in cardHand.transform)
        {
            if (child.name.Equals(val))
            {
                child.position = new Vector3(child.position.x, cardHand.transform.position.y + 20, child.position.z);
            }
            else
            {
                child.position = new Vector3(child.position.x, cardHand.transform.position.y, child.position.z);

            }
        }

        pickedCard = cardNum;

        //Card c = new ShootArrow();
        //c.UseCard(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 0);
    }

    private void UpdateHandPics()
    {
        int c = 0;
        foreach (Transform child in cardHand.transform)
        {
            child.gameObject.GetComponent<Image>().sprite = cards[c].cardModel.sprite;
            c++;
        }
    }


    public void UseCard(Vector2 targ)
    {
        if (cards[pickedCard - 1].UseCard(transform.position, targ, 0))
        {
            cards.Add(cards[pickedCard - 1]);
            cards.RemoveAt(pickedCard - 1);
            UpdateHandPics();
        }
    }
    
    
    private void UpdateInventory()
    {
        GameObject slots = GameObject.Find("MainCanvas").transform.Find("Inventory").Find("Slots").gameObject;
        for (int i = 0; i < 40; i++)
        {
            GameObject currentSlot = slots.transform.Find("Slot" + (i + 1)).gameObject;
            GameObject currentCard = currentSlot.transform.Find("Slot").gameObject;
            if (i >= cards.Count)
            {

                Image tmp = currentCard.GetComponent<Image>();
                Destroy(tmp);
            }
            else
            {
                Card curCard = cards[i];
                currentCard.GetComponent<Image>().sprite = curCard.cardModel.sprite;
            }
        }
    }

    public void UpdateQuests()
    {
        GameObject questList = GameObject.Find("MainCanvas").transform.Find("Quests").Find("QuestList").gameObject;
        for (int i = 1; i <= 5; i++)
        {
            GameObject currentSlot = questList.transform.Find("Quest" + i).gameObject;
            if (i > quests.Count)
            {
                currentSlot.SetActive(false);
            }
            else
            {
                currentSlot.SetActive(true);
                currentSlot.transform.Find("Title").gameObject.GetComponent<TextMeshProUGUI>().text = quests[i-1].title;
                currentSlot.transform.Find("Description").gameObject.GetComponent<TextMeshProUGUI>().text = quests[i-1].description;
            }
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        int x = combatManager.sceneX;
        int y = combatManager.sceneY;
        
        string boundaryName = other.gameObject.transform.name;
        if (boundaryName.Equals("NorthBoundary") && y < 2)
        {
            y += 1;
            transform.position = new Vector3(transform.position.x, -400, transform.position.z);
        }
        else if (boundaryName.Equals("SouthBoundary") && y > 0)
        {
            y -= 1;
            transform.position = new Vector3(transform.position.x, 400, transform.position.z);
        }
        else if (boundaryName.Equals("EastBoundary") && x < 2)
        {
            x += 1;
            transform.position = new Vector3(-400, transform.position.y, transform.position.z);
        }
        else if (boundaryName.Equals("WestBoundary") && x > 0)
        {
            x -= 1;
            transform.position = new Vector3(400, transform.position.y, transform.position.z);
        }
        else 
        {
            return;
        }

        TargetPoint = transform.position;
        GameObject.Find("NPCs").transform.Find(combatManager.sceneX + " - " + combatManager.sceneY).gameObject.SetActive(false);
        GameObject.Find("Environment").transform.Find(combatManager.sceneX + " - " + combatManager.sceneY).gameObject.SetActive(false);
        mapSpots.transform.Find(combatManager.sceneX + " - " + combatManager.sceneY).gameObject.SetActive(false);
        combatManager.sceneX = x;
        combatManager.sceneY = y;
        GameObject.Find("NPCs").transform.Find(combatManager.sceneX + " - " + combatManager.sceneY).gameObject.SetActive(true);
        GameObject.Find("Environment").transform.Find(combatManager.sceneX + " - " + combatManager.sceneY).gameObject.SetActive(true);
        mapSpots.transform.Find(combatManager.sceneX + " - " + combatManager.sceneY).gameObject.SetActive(true);
        combatManager.ReFindNPCs();
        
    }
}
