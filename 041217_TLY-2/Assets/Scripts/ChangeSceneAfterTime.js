#pragma strict

var timer : float = 6.0;

function Start () {
	
}

function Update () {
	timer -= Time.deltaTime;

	if(timer <= 0)
	{
		Application.LoadLevel("1.0 Prompt_login");
	}
}
