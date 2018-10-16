using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    //public string changeTheScene;

    public void changeToScene(string changeTheScene)
    {
        SceneManager.LoadScene(changeTheScene);
    }


    public void exitapplication ()
    {
        Application.Quit();
    }
}
