using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    //Array for fight order
    //NPC has public method to do their move
    
    
    public bool fighting;

    public List<string> initiativeOrder;

    public int current;

    private GameObject NPCs;
    private Character hero;
    public int sceneX;
    public int sceneY;
    public NPC sally;
    public NPC mut;
    public NPC stul;
    public NPC commander;
    public static CombatManager Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        fighting = false;
        sceneX = 0;
        sceneY = 0;
        NPCs = GameObject.Find("NPCs").transform.Find(sceneX + " - " + sceneY).gameObject;
        hero = GameObject.Find("Hero").GetComponent<Character>();
        Instance = this;

    }

    public void ReFindNPCs()
    {
        NPCs = GameObject.Find("NPCs").transform.Find(sceneX + " - " + sceneY).gameObject;
    }
    
    void Update()
    {
        if (fighting)
        {
            // Get script of current fighter
            // 
            
        }
    }

    public void ChangeTurn()
    {
        print(current);
        print(initiativeOrder);
        
        // Check if fight was won or lost
        if (!hero.alive)
        {
            EndCombat();
            return;
        }
        
        if (NPCs.transform.childCount == 0)
        {
            EndCombat();
            hero.maxMana = 2;
            hero.mana = 2;
            hero.UpdateManaBar();
            for (int i = 0; i < hero.quests.Count; i++)
            {
                switch (hero.quests[i].id)
                {
                    case 1:
                        if (sceneX == 1 && sceneY == 2)
                        {
                            hero.quests.RemoveAt(i);
                            sally.state = 9;
                        }
                        break;
                    case 4:
                        if (sceneX == 0 && sceneY == 1)
                        {
                            hero.quests.RemoveAt(i);
                            mut.state = 3;
                        }
                        break;
                    case 5:
                        if (sceneX == 2 && sceneY == 1)
                        {
                            hero.quests.RemoveAt(i);
                            stul.state = 3;
                        }
                        break;
                    case 8:
                        if (sceneX == 0 && sceneY == 0)
                        {
                            hero.quests.RemoveAt(i);
                            commander.state = 3;
                        }
                        break;
                    default:
                        return;
                }
            }
            hero.UpdateQuests();
            return;
        }
        
        NPC npc;
        GameObject n;
        
        
        // Check if it's the players turn
        if (initiativeOrder[current].Equals("Hero"))
        {
            
            // Check if player has used their turn
            if (!hero.myTurn)
            {
                
                // Go to next turn
                current += 1;
                if (current == initiativeOrder.Count)
                {
                    current = 0;
                    ProcAreas();
                }
                
                // If next turn is hero, then set var
                if (initiativeOrder[current].Equals("Hero"))
                {
                    hero.myTurn = true;
                }
                else
                {
                    //Otherwise next turn is npc
                    Transform t = NPCs.transform.Find(initiativeOrder[current]);
                    
                    
                    //If npc is null, 
                    if (t == null)
                    {
                        ChangeTurn();
                        return;
                    }

                    n = t.gameObject;
                    npc = n.GetComponent<NPC>();
                    npc.myTurn = true;
                }
            }
        }
        else
        {
            // Means that it's an NPC's turn
            Transform t = NPCs.transform.Find(initiativeOrder[current]);
                    
            
            // If NPC is dead, go next turn
            if (t == null)
            {
                current += 1;
                if (current == initiativeOrder.Count)
                {
                    current = 0;
                    ProcAreas();
                }
                if (initiativeOrder[current].Equals("Hero"))
                {
                    hero.myTurn = true;
                }
                else
                {
                    t = NPCs.transform.Find(initiativeOrder[current]);
                    
                    if (t == null)
                    {
                        ChangeTurn();
                        return;
                    }

                    n = t.gameObject;
                    npc = n.GetComponent<NPC>();
                    npc.myTurn = true;
                }
                ChangeTurn();
                return;
            }

            n = t.gameObject;
            npc = n.GetComponent<NPC>();
            
            if (!npc.myTurn)
            {
                current += 1;
                if (current == initiativeOrder.Count)
                {
                    current = 0;
                    ProcAreas();
                }

                if (initiativeOrder[current].Equals("Hero"))
                {
                    hero.myTurn = true;
                }
                else
                {
                    t = NPCs.transform.Find(initiativeOrder[current]);
                    
                    if (t == null)
                    {
                        ChangeTurn();
                        return;
                    }

                    n = t.gameObject;
                    npc = n.GetComponent<NPC>();
                    npc.myTurn = true;
                }
            }
        }
    }

    public void ProcAreas()
    {
        GameObject areas = GameObject.Find("AreasOfEffect");
        foreach(Transform child in areas.transform)
        {
            AreaOfEffect aoe = child.GetComponent<AreaOfEffect>();
            aoe.ProcArea();
            aoe.lifespan -= 1;
        }
    }

    public void StartCombat()
    {
        if (!fighting)
        {
            hero.fighting = true;
            
            initiativeOrder.Add("Hero");
            foreach (Transform child in NPCs.transform)
            {
                initiativeOrder.Add(child.name);
            }
            Shuffle(initiativeOrder);
            foreach (string w in initiativeOrder)
            {
                print(w);
            }
            
            current = 0;

            if (initiativeOrder[current].Equals("Hero"))
            {
                hero.myTurn = true;
            }
            else
            {
                NPC npc = NPCs.transform.Find(initiativeOrder[current]).gameObject.GetComponent<NPC>();
                npc.myTurn = true;
            }

            fighting = true;
        }
    }
    
    static void Shuffle<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void EndCombat()
    {
        hero.myTurn = false;
        hero.fighting = false;
        hero.ChangeHealth(100);
        fighting = false;
        foreach (Transform child in GameObject.Find("AreasOfEffect").transform)
        {
            Destroy(child);
        }
    }


}
