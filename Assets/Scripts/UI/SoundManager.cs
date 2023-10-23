using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer mixer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void ChangeMasterVolume(float value)
    {
        //Account.masterVolume = value;
        PlayerPrefs.SetFloat("masterVolume", value);
        PlayerPrefs.Save();
        mixer.SetFloat("MasterVolume", Mathf.Log10(value+0.0001f)*20);
    }

    public void ChangeMusicVolume(float value)
    {
        //Account.musicVolume = value;
        PlayerPrefs.SetFloat("musicVolume", value);
        PlayerPrefs.Save();
        mixer.SetFloat("MusicVolume", Mathf.Log10(value+0.0001f)*20);
    }

    public void ChangeSoundEffectsVolume(float value)
    {
        //Account.effectsVolume = value;
        PlayerPrefs.SetFloat("effectsVolume", value);
        PlayerPrefs.Save();
        mixer.SetFloat("SFXVolume", Mathf.Log10(value+0.0001f)*20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
