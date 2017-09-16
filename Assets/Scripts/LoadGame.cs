using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGame : MonoBehaviour {

    public void LoadMainScene()
    {
        Application.LoadLevel(1);
    }

    public void LoadMenu()
    {
        Application.LoadLevel(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
