using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;                      //Using Scene manager.
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {


    bool inPauseMenu = false;
    float oldTimeScale = 0f;                         //to preserve timescale.
    public GameObject pauseMenu;
    public Button pauseStartFocus;

    public AudioClip ButtonClick;                       //set the soundclip for the button in the inspector.


    void FixedUpdate()
    {
        if (Input.GetAxis("Pause") > 0.1f)           // checks to see if user presses pause button.
        {
            if (inPauseMenu == true) closePause();              // opens or closes pause menu depending on if user is already in menu.
            else openPause();
        }
    }


    public void toggleAiming()
    {
        SoundManager.instance.bamPow(ButtonClick);      //Plays clikc sound.
        Main.toggleMouseAiming();       //calls the static toggle aiming function in main.
    }

    public void toMainMenu()
    {
        SoundManager.instance.bamPow(ButtonClick);      //plays buttonclick
        SceneManager.LoadScene("MainMenu");             //loads main menu scene using scene manager.
    }

    public void restartLevel()
    {
        SoundManager.instance.bamPow(ButtonClick);      //plays buttonclick
        Scene currentScene = SceneManager.GetActiveScene();     //gets the scene loaded.
        string nameOfLevel = currentScene.name;                  //gets the name of that scene.
        SceneManager.LoadScene(nameOfLevel);                    //loads the scene with that name.

    }
    
    //Opens pause meny
    void openPause()
    {
        SoundManager.instance.bamPow(ButtonClick);      //Plays clikc sound.
        inPauseMenu = true;                             // the player is now in menu.
        pauseMenu.SetActive(inPauseMenu);               //pause menu gameobject is now active.
        pauseStartFocus.Select();                       //selevt the back button as selected ui element.
        oldTimeScale = Time.timeScale;                  //save the current timescale vaule to a float.
        Time.timeScale = 0f;                            //0 the timescale so the gameplay freezes.
    }

    public void closePause()                // public cuz used by back button.
    {
        SoundManager.instance.bamPow(ButtonClick);      //Plays clikc sound.
        Time.timeScale = oldTimeScale;                  //Sets th timescale back to what it was before the pause.
        inPauseMenu = false;                            //No longer in menu.
        pauseMenu.SetActive(inPauseMenu);               //sets pause menu object as inactive.
    }



}
