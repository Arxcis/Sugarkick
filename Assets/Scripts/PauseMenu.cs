using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;                      //Using Scene manager.

public class PauseMenu : MonoBehaviour {


    public AudioClip ButtonClick;                       //set the soundclip for the button in the inspector.


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


}
