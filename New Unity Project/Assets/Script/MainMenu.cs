using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    string hoverSound = "ButtonHover";

    [SerializeField]
    string pressSound = "ButtonPress";

    public GameObject TitleMenu;

    public GameObject LevelSelect;

    public GameObject Option;

    public GameObject Fader;

    public string levelToLoad = "Game";

public void StartGame()
    {
        TitleMenu.SetActive(false);
        LevelSelect.SetActive(true);
        Fader.SetActive(true);
    }

    public void OptionMenu()
    {
        TitleMenu.SetActive(false);
        Option.SetActive(true);
        Fader.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
