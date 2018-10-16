using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AboutUsFunctionScript : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("2.0_front");
    }

    public void Facebookclick()
    {
        Application.OpenURL("http://www.facebook.com/VomoStudio");
    }

    public void Instaclick()
    {
        Application.OpenURL("http://www.instagram.com/vomo_studio/");
    }


}
