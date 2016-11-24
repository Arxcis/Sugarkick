using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Canvas MainCanvas;                          // Decleares canvases used.
    public Canvas OptionsCanvas;
    public Canvas SelectionCanvas;
    public GameObject mainStartFocus;           // ui element that is suppose to be focused when a new canvas is enabled. Only one per canvas.
    public GameObject optionsStartFocus;
    public GameObject selectionStartFocus;
    public AudioClip menuMusic;             //audioclips for the menu,.
    public AudioClip buttonSound;

    public float backgroundSpeed1 = 0.5f;       // background images rotation speed.
    public float backgroundSpeed2 = 0.3f;
    public float backgroundSpeed3 = 0.3f;

    void Start()
    {
        mainOpen();
    }

    public void mainOpen()                             // Switches to the level selection.
    {
        SoundManager.instance.bamPow(buttonSound);
        MainCanvas.enabled      = true;                 
        SelectionCanvas.enabled = false;
        OptionsCanvas.enabled   = false;
        mainStartFocus.GetComponent<Button>().Select();         // sets start button as main focus then opening main menu.
    }

    void FixedUpdate()
    {
        GameObject.Find("Background1").transform.Rotate(new Vector3(0,0, backgroundSpeed1));
        GameObject.Find("Background2").transform.Rotate(new Vector3(0, 0, backgroundSpeed2));           //rotats backgrounds.
        GameObject.Find("Background3").transform.Rotate(new Vector3(0, 0, backgroundSpeed3));
    }


    public void OptionsOpen()                          // Switches back to the main menu.                 
    {

        SoundManager.instance.bamPow(buttonSound);      // play button sound.
        MainCanvas.enabled = false;
        SelectionCanvas.enabled = false;
        OptionsCanvas.enabled = true;
        optionsStartFocus.GetComponent<Slider>().Select();          // select music vol slider as focus.


    }

    public void selectionOpen()                         // Switches to the options menu.
    {
        SoundManager.instance.bamPow(buttonSound);
        MainCanvas.enabled = false;
        SelectionCanvas.enabled = true;
        OptionsCanvas.enabled = false;
        selectionStartFocus.GetComponent<Button>().Select();

    }


    public void quitGame()
    {
        SoundManager.instance.bamPow(buttonSound);
        Application.Quit();
    }

    public void LoadOctagon()                           // Loads the different scenes.
    {
        SoundManager.instance.bamPow(buttonSound);      //button sound.
        SceneManager.LoadScene("Octagon");
    }
    public void LoadHexagon()
    {
        SoundManager.instance.bamPow(buttonSound);
        SceneManager.LoadScene("Hexagon");
    }
    public void LoadSquare()
    {
        SoundManager.instance.bamPow(buttonSound);
        SceneManager.LoadScene("Square");
    }
    public void LoadTriangle()
    {
        SoundManager.instance.bamPow(buttonSound);
        SceneManager.LoadScene("Triangle");
    }
    public void LoadLine()
    {
        SoundManager.instance.bamPow(buttonSound);
        SceneManager.LoadScene("Line");
        
    }

}
