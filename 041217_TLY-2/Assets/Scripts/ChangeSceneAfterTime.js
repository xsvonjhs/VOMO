#pragma strict
import UnityEngine.SceneManagement;

var timer : float = 6.0;
var login = false;

function Start () {
	
}

function Update () {
	timer -= Time.deltaTime;

	if(timer <= 0)
    {
        if(login === true) {
            SceneManager.LoadScene("1.0 Prompt_login");
        }
        else SceneManager.LoadScene("2.0_front");
           
	}
}
