using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deact_choose_Frame : MonoBehaviour {

    public bool PT_condition;
    public GameObject PopFrame;

    // Use this for initialization
    void Start()
    {
        PopFrame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (PT_condition == false)
        {
            PopFrame.SetActive(true);
        }

        else
        {
            PopFrame.SetActive(false);
        }

    }
}
