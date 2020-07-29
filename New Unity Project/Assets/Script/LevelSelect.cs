using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    public SceneFader fader;

    public void Select(string levelName)
    {
        fader.FadeTo(levelName);
        SceneManager.LoadScene(levelName);
    }

}
