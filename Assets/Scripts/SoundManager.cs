using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour {

    public AudioSource musicAud;
    public AudioSource effectsAud;
    public Slider musicTrackbar;            // set vol slider with inspector
    public Slider effectsTrackbar;
    

    public static SoundManager instance = null;     // not sure how instances work. But its useful.

    public float musicVol = 0.3f;               //default vol values.
    public float effectsVol = 0.4f;

    void Awake()    //ran before the Start()
    {
        if (instance == null) instance = this;          //it sound manager allready exist. Kill sound manager. This none. Not the other one.
        else if (instance != null) Destroy(gameObject);
        musicAud.volume = musicVol;
        effectsAud.volume = effectsVol;
    } 

    public void volumeChange()                  //when volume is changed with sliders, update the values.
    {
        musicVol = musicTrackbar.value;     //get values
        effectsVol = effectsTrackbar.value;
            
        musicAud.volume = musicVol;         //set values.
        effectsAud.volume = effectsVol;
    }

    public void ohLetsBreakItDown(AudioClip music)      //Play a sound with the music aud source. #Lucio
    {
        musicAud.loop = true;
        musicAud.clip = music;      //gets the soundclip from perameter
        musicAud.volume = musicVol;
        musicAud.Play();
    }

    public void bamPow(AudioClip poof)                  // playa sound with the effects aud source. 
    {
        effectsAud.loop = false;
        effectsAud.clip = poof;     //gets the soundclip from perameter
        effectsAud.volume = effectsVol;
        effectsAud.PlayOneShot(poof);
        //effectsAud.Play();
    }

}
