using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerWarning : MonoBehaviour {

    public float t = 0.0f;
    public GameObject timeWarningtext;

    // Use this for initialization
    void Start () {
        t = 0.0f;
    }
	
	// Update is called once per frame
	void Update () {

        t = t + Time.deltaTime;
        if (t > 2.0f)
        {
            timeWarningtext.SetActive(false);
            t = 0.0f;
        }

	}
}
