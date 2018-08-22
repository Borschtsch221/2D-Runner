using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSettings : MonoBehaviour {



    public void Restart()
    {
        Application.LoadLevel("main_scene");

    }

    public void MainMenu()
    {

        Application.LoadLevel("menu");

    }


}
