using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveFile : MonoBehaviour
{
    // Start is called before the first frame update
    //Save ID, Player name, Currency, Position, Reputation, Stats, Card deck, Equipment
    private int defaultX = 0;
    private int defaultY = 0;
    private string defaultName = "Bob";
    public void LoadFile(int id)
    {
        
        if (!PlayerPrefs.HasKey("File" + id + " Empty") || PlayerPrefs.GetInt("File" + id + " Empty") == 1)
        {
            NewFile(id);
        }
        else
        {
            int x = PlayerPrefs.GetInt("File" + id + " MapX");
            int y = PlayerPrefs.GetInt("File" + id + " MapY");
            SceneManager.LoadScene("Map - " + x + " - " + y);
        }
    }

    public void DeleteFile(int id)
    {
        PlayerPrefs.SetInt("File" + id + " Empty", 1);
        PlayerPrefs.Save();
    }

    public void NewFile(int id)
    {
        PlayerPrefs.SetInt("File" + id + " Empty", 0);
        PlayerPrefs.SetInt("File" + id + " MapX", defaultX);
        PlayerPrefs.SetInt("File" + id + " MapY", defaultY);
        PlayerPrefs.SetString("File" + id + " Name", defaultName);
        PlayerPrefs.Save();
    }

    public void SaveData()
    {
        
    }


}
