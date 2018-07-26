using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ID2 : MonoBehaviour {

    public GameObject Loginscript;
    public GameObject stayin;
    private CheckLogin passID;
    private string Receive_ID;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void get_ID ()
    {
        passID = Loginscript.GetComponent<CheckLogin>();
        Receive_ID = passID.send_ID;
        print(Receive_ID);
        DontDestroyOnLoad(stayin);
    }
}
