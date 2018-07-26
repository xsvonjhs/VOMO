using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deact : MonoBehaviour {

    public GameObject Box;

	// Use this for initialization
	void Start () {
        Box.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DeactBox()
    {
        Box.SetActive(false);
    }

    public void ActBox()
    {
        Box.SetActive(true);
    }
}
