using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Canvas MainCanvas;                          // Decleares canvases used.
    public Canvas OptionsCanvas;
    public Canvas SelectionCanvas;
    public AudioClip menuMusic;
    public AudioClip buttonSound;
    public float musicVolume = 1f;
    public float buttonVolume = 1f;
    public float backgroundSpeed1 = 0.5f;
    public float backgroundSpeed2 = 0.3f;
    public float backgroundSpeed3 = 0.3f;

    AudioSource aud;

    void Awake()                                       // Is run before "start" functions.
    {
        mainOpen();
        aud = GetComponent<AudioSource>();
    }

    public void mainOpen()                             // Switches to the level selection.
    {
        MainCanvas.enabled      = true;
        SelectionCanvas.enabled = false;
        OptionsCanvas.enabled   = false;
    }

    void FixedUpdate()
    {
        GameObject.Find("Background1").transform.Rotate(new Vector3(0,0,backgroundSpeed1));
        GameObject.Find("Background2").transform.Rotate(new Vector3(0, 0, backgroundSpeed2));
        GameObject.Find("Background3").transform.Rotate(new Vector3(0, 0, backgroundSpeed3));
    }

    public void OptionsOpen()                          // Switches back to the main menu.                 
    {
        MainCanvas.enabled = false;
        SelectionCanvas.enabled = false;
        OptionsCanvas.enabled = true;
        aud.PlayOneShot(buttonSound, buttonVolume);

    }

    public void selectionOpen()                         // Switches to the options menu.
    {
        MainCanvas.enabled = false;
        SelectionCanvas.enabled = true;
        OptionsCanvas.enabled = false;
        aud.PlayOneShot(buttonSound, buttonVolume);
    }


    public void quitGame()
    {
        Application.Quit();
    }


    public void LoadOctagon()                           // Loads the different scenes.
    {
        aud.PlayOneShot(buttonSound, buttonVolume);
        SceneManager.LoadScene("Octagon");
    }
    public void LoadHexagon()
    {
        aud.PlayOneShot(buttonSound, buttonVolume);
        SceneManager.LoadScene("Hexagon");
    }
    public void LoadSquare()
    {
        aud.PlayOneShot(buttonSound, buttonVolume);
        SceneManager.LoadScene("Square");
    }
    public void LoadTriangle()
    {
        aud.PlayOneShot(buttonSound, buttonVolume);
        SceneManager.LoadScene("Triangle");
    }
    public void LoadLine()
    {
        aud.PlayOneShot(buttonSound, buttonVolume);
        SceneManager.LoadScene("Line");
        
    }

}
