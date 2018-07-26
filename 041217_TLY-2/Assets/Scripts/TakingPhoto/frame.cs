using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frame : MonoBehaviour {

    public Animator anim;
    public GameObject BtnEnter;
    public GameObject BtnExit;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        BtnEnter.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void FrameEnter()
    {
        anim.SetTrigger("enter");
        BtnExit.SetActive(true);
        BtnEnter.SetActive(false);
    }

    public void FrameExit()
    {
        anim.SetTrigger("exit");
        BtnExit.SetActive(false);
        BtnEnter.SetActive(true);
    }
}
