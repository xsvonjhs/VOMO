﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;

public class PopOut : MonoBehaviour, ITrackableEventHandler {

    public GameObject Ticket;
    public GameObject Btn;
    public bool show = false;

    private TrackableBehaviour mTrackableBehaviour;

    private bool mShowGUIButton = false;
    private Rect mButtonRect = new Rect(50, 50, 120, 60);

    void Start()
    {
        Ticket.SetActive(false);
        Btn.SetActive(false);
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED)
        {
            mShowGUIButton = true;
        }
        else
        {
            mShowGUIButton = false;
        }
    }

    void OnGUI()
    {
        if (mShowGUIButton)
        {
            Btn.SetActive(true);
            /*
            // draw the GUI button
            if (GUI.Button(mButtonRect, "Hello"))
            {
                // do something on button click 
            }
            */
        }
        else
        {
            Btn.SetActive(false);
        }
    }

    public void onCancel()
    {
        if (show == true)
        {
            Ticket.SetActive(false);
            show = false;
        }
    }

    public void BtnClick()
    {
        Ticket.SetActive(true);
        show = true;
    }
}