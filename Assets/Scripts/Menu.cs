using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {


    public Text NumUpDownKeyMouse;
    public Text NumUpDownKeyboard;
    public Text NumUpDownJoystick1;
    public Text NumUpDownJoystick2;
    public Text NumUpDownJoystick3;
    public Text NumUpDownJoystick4;



    public Canvas MainCanvas;                          // Decleares canvases used.
    public Canvas OptionsCanvas;
    public Canvas SelectionCanvas;
    public Canvas PlayerCanvas;
    public GameObject mainStartFocus;           // ui element that is suppose to be focused when a new canvas is enabled. Only one per canvas.
    public GameObject optionsStartFocus;
    public GameObject selectionStartFocus;
    public GameObject playerStartFocus;
    public AudioClip menuMusic;             //audioclips for the menu,.
    public AudioClip buttonSound;

    public float backgroundSpeed1 = 0.5f;       // background images rotation speed.
    public float backgroundSpeed2 = 0.3f;
    public float backgroundSpeed3 = 0.3f;


    Dictionary<int, int> playerControl = new Dictionary<int, int>();
    List <int> playerList = new List<int>();

    void Start()
    {
        mainOpen();
    }

    void generatePlayerArray()
    {
        for (int j = 0; j < playerControl.Count; j++)
        {
        // if (playerControl.)    too tired for this shit.
        }
    }

    public void numericPluss(Text numeric)
    {
        SoundManager.instance.bamPow(buttonSound);
        int id = numeric.GetInstanceID();
        int value = int.Parse(numeric.text);

        value++;
        if (value > 6)   // edge case 1
        {
            value = 0;
        }
        int i = 0;
        while (playerControl.ContainsValue(value) && i < 7)   // edge case 2
        {
            value++;

            if (value > 6)
            {
                value = 0;
            }
            i++;
        }


        if (playerControl.ContainsKey(id))   // insert in dictionary if id already exists
        {
            playerControl[id] = value;
        }
        else                                // make new key slot if id not exist
        {
            playerControl.Add(id, value);  
        }
        numeric.text = value.ToString();
    }

    public void numericMinus(Text numeric)
    {
        SoundManager.instance.bamPow(buttonSound);
        int id = numeric.GetInstanceID();
        int value = int.Parse(numeric.text);

        value--;
        if (value < 0)   // edge case 1
        {
            value = 6;
        }
        int i = 0;
        while (playerControl.ContainsValue(value) && i < 7)   // edge case 2
        {
            value--;

            if (value < 6)
            {
                value = 6;
            }
            i++;
        }


        if (playerControl.ContainsKey(id))   // insert in dictionary if id already exists
        {
            playerControl[id] = value;
        }
        else                                // make new key slot if id not exist
        {
            playerControl.Add(id, value);
        }
        numeric.text = value.ToString();
    }

    public void mainOpen()                             // Switches to the level selection.
    {
        SoundManager.instance.bamPow(buttonSound);
        MainCanvas.enabled      = true;                 
        SelectionCanvas.enabled = false;
        OptionsCanvas.enabled   = false;
        PlayerCanvas.enabled    = false;
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
        PlayerCanvas.enabled = false;
        optionsStartFocus.GetComponent<Slider>().Select();          // select music vol slider as focus.


    }

    public void playerSelectionOpen()
    {
        SoundManager.instance.bamPow(buttonSound);
        MainCanvas.enabled = false;
        SelectionCanvas.enabled = false;
        OptionsCanvas.enabled = false;
        PlayerCanvas.enabled = true;
        playerStartFocus.GetComponent<Button>().Select();
    }

    public void selectionOpen()                         // Switches to the options menu.
    {
        SoundManager.instance.bamPow(buttonSound);
        MainCanvas.enabled = false;
        SelectionCanvas.enabled = true;
        OptionsCanvas.enabled = false;
        PlayerCanvas.enabled = false;
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
