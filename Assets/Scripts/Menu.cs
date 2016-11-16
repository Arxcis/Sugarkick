using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Canvas MainCanvas;                               //Decleares canvases used.
    public Canvas OptionsCanvas;
    public Canvas SelectionCanvas;

    void Awake()                                            //is run before "start" functions.
    {
        mainOpen();
    }

    public void mainOpen()                             //switches to the level selection.
    {
        MainCanvas.enabled = true;
        SelectionCanvas.enabled = false;
        OptionsCanvas.enabled = false;                     
    }

    public void OptionsOpen()                                           //switches back to the main menu.                 
    {
        MainCanvas.enabled = false;
        SelectionCanvas.enabled = false;
        OptionsCanvas.enabled = true;
    }

    public void selectionOpen()                                 //switches to the options menu.
    {
        MainCanvas.enabled = false;
        SelectionCanvas.enabled = true;
        OptionsCanvas.enabled = false;
    }

    public void LoadOctagon()                                   //Loads the different scenes.
    {
        SceneManager.LoadScene("Octagon");
    }
    public void LoadHexagon()
    {
        SceneManager.LoadScene("Hexagon");
    }
    public void LoadSquare()
    {
        SceneManager.LoadScene("Square");
    }
    public void LoadTriangle()
    {
        SceneManager.LoadScene("Triangle");
    }
    public void LoadLine()
    {
        SceneManager.LoadScene("Line");
    }

}
