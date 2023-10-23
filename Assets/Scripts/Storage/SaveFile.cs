using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveFile : MonoBehaviour
{
    // Start is called before the first frame update
    //Save ID, Player name, Currency, Position, Reputation, Stats, Card deck, Equipment


    public void LoadFile(int id)
    {
        
        if (PlayerPrefs.GetInt("File" + id + " Empty") == 1)
        {
            NewFile(id);
        }
        else
        {
            int x = PlayerPrefs.GetInt("File" + id + " MapX");
            int y = PlayerPrefs.GetInt("File" + id + " MapY");
            PlayerPrefs.Save();
            SceneManager.LoadScene("Map - " + x + " - " + y);
        }
    }

    public void DeleteFile()
    {
        
    }

    public void NewFile(int id)
    {
        
    }

    public void SaveData()
    {
        
    }


}
