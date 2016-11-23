using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class SoundManager : MonoBehaviour {

    public AudioSource musicAud;
    public AudioSource effectsAud;
    public Slider musicTrackbar;
    public Slider effectsTrackbar;
    

    public static SoundManager instance = null;

    public float musicVol = 0.3f;
    public float effectsVol = 0.4f;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(gameObject);
        musicAud.volume = musicVol;
        effectsAud.volume = effectsVol;
    } 

    public void volumeChange()
    {
        musicVol = musicTrackbar.value;
        effectsVol = effectsTrackbar.value;

        musicAud.volume = musicVol;
        effectsAud.volume = effectsVol;
    }

    public void ohLetsBreakItDown(AudioClip music)
    {
        musicAud.loop = true;
        musicAud.clip = music;
        musicAud.volume = musicVol;
        musicAud.Play();
    }

    public void bamPow(AudioClip poof)
    {
        effectsAud.loop = false;
        effectsAud.clip = poof;
        effectsAud.volume = effectsVol;
        effectsAud.Play();
    }

}
