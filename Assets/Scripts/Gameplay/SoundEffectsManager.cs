using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip heal;
    public AudioClip slash;
    public AudioClip hurt;
    public AudioClip magic;
    public AudioClip select;
    public AudioClip acceptQuest;
    public AudioClip arrow;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHeal()
    {
        audioSource.clip = heal;
        audioSource.Play();
    }
    
    public void PlaySlash()
    {
        audioSource.clip = slash;
        audioSource.Play();
    }
    
    public void PlayHurt()
    {
        audioSource.clip = hurt;
        audioSource.Play();
    }
    
    public void PlayMagic()
    {
        audioSource.clip = magic;
        audioSource.Play();
    }
    
    public void PlaySelect()
    {
        audioSource.clip = @select;
        audioSource.Play();
    }
    
    public void PlayQuest()
    {
        audioSource.clip = acceptQuest;
        audioSource.Play();
    }
    
    public void PlayArrow()
    {
        audioSource.clip = arrow;
        audioSource.Play();
    }
    
    
}
