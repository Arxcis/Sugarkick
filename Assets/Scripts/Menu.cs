using UnityEngine;
using System.Collections;
using System.Collections.Generic;           //for dictionaries and lists.
using UnityEngine.UI;                       // the ui elements like text and buttons.
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

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

    Dictionary <int, int> playerControl = new Dictionary<int, int>();       // the Dictionary use to store values of the numUpDowns.
    public List<int> potetSekk = new List<int>();  // the list got by Actions and Main to determine how many players to spwan and what controlls to give them.
    

    void Start()
    {
        mainOpen();                 //enables the main canvas and disables all other canvases.
    }

    void generatePlayerList()                       // convers the Dictionary used by the numUpDowns to a list used by Main and Actions.
    {
        foreach (KeyValuePair<int, int> IndexValue in playerControl)
        {
            if (IndexValue.Value != 0)                      //Makes a list containging the index of those who have a value other than 0.
                    potetSekk.Add(IndexValue.Key);
        }
    }

    public void numericPluss(Text numeric)                   //increases the value of parrent numpUpDown whitch is a gameObject with text script.
    {
        SoundManager.instance.bamPow(buttonSound);          // button sound.

        int value = int.Parse(numeric.text);
        int index = int.Parse(numeric.name.Split('_')[0]);      //gets the index from the number in GameObject name. Seperated with '_'.

        value++;
        if (value > 6)   // edge case 1
        {
            value = 0;
        }
        int i = 0;
        while (playerControl.ContainsValue(value) && i < 7)   // edge case 2.
        {
            value++;

            if (value > 6)
            {
                value = 0;
            }
            i++;
        }
        if (playerControl.ContainsKey(index))   // insert in dictionary if id already exists.
        {
            playerControl[index] = value;
        }
        else                                // make new key slot if id not exist.
        {
            playerControl.Add(index, value);
        }
        numeric.text = value.ToString();        //updates what value the numUpDown displays.

    }

    public void numericMinus(Text numeric)                   //decreases the value of parrent numpUpDown whitch is a gameObject with text script.
    {
        SoundManager.instance.bamPow(buttonSound);           // button sound.

        int value = int.Parse(numeric.text);
        int index = int.Parse(numeric.name.Split('_')[0]);   //gets the index from the number in GameObject name. Seperated with '_'.

        value--;    
        if (value < 0)   // edge case 1
        {
            value = 6;
        }
        int i = 0;
        while (playerControl.ContainsValue(value) && i < 7)   // edge case 2
        {
            value--;

            if (value < 0)
            {
                value = 6;
            }
            i++;
        }
        if (playerControl.ContainsKey(index))   // insert in dictionary if id already exists.
        {
            playerControl[index] = value;
        }
        else                                // make new key slot if id not exist.
        {
            playerControl.Add(index, value);
        }
        numeric.text = value.ToString();        //updates what value the numUpDown displays.
    }


    void FixedUpdate()
    {
        GameObject.Find("Background1").transform.Rotate(new Vector3(0,0, backgroundSpeed1));
        GameObject.Find("Background2").transform.Rotate(new Vector3(0, 0, backgroundSpeed2));           //rotats backgrounds.
        GameObject.Find("Background3").transform.Rotate(new Vector3(0, 0, backgroundSpeed3));
    }


    public void mainOpen()                             // Switches to the level selection.
    {
        SoundManager.instance.bamPow(buttonSound);
        MainCanvas.enabled = true;
        SelectionCanvas.enabled = false;
        OptionsCanvas.enabled = false;
        PlayerCanvas.enabled = false;
        mainStartFocus.GetComponent<Button>().Select();         // sets start button as main focus then opening main menu.
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
        playerStartFocus.GetComponent<Button>().Select();   // sets the correct ui elemet as focus when a new canvas is opened.
    }

    public void selectionOpen()                         // Switches to the options menu.
    {
        SoundManager.instance.bamPow(buttonSound);
        MainCanvas.enabled = false;
        SelectionCanvas.enabled = true;
        OptionsCanvas.enabled = false;
        PlayerCanvas.enabled = false;
        selectionStartFocus.GetComponent<Button>().Select();   // sets the correct ui elemet as focus when a new canvas is opened.
        generatePlayerList();                                   //Runs the function that make the list Main and Actions use to spawn and configure players.

    }



    /////////////////////////////   LOADING OF SCENES   /////////////////////////
    public void quitGame()
    {
        SoundManager.instance.bamPow(buttonSound);
        Application.Quit();                             //Closes the aplication, does not work in editor, only built version.
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
