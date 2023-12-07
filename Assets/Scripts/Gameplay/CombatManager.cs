using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    //Array for fight order
    //NPC has public method to do their move
    
    
    public bool fighting;

    public List<string> initiativeOrder;

    public int current;
    
    public static CombatManager Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        fighting = false;

        Instance = this;
    }
    
    void Update()
    {
        if (fighting)
        {
            // Get script of current fighter
            // 
        }
    }

    public void StartCombat()
    {
        if (!fighting)
        {
            initiativeOrder.Add("Hero");
            GameObject NPCs = GameObject.Find("NPCs");
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


}
