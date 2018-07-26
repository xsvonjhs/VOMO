using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectBtnclick : MonoBehaviour {

    public Button yourButton;
    public GameObject textloading;

    void Start()
    {

    }


        // Update is called once per frame
        void Update () {

        if (Input.GetButtonDown ("Upload_Button"))
        {
            textloading.SetActive(true);
        }

        //Button btn = yourButton.GetComponent<Button>();
        //btn.onClick.AddListener(TaskOnClick);

    }

    void TaskOnClick()
    {
       // Debug.Log("You have clicked the button!");
    }

    public void showloading()
    {
      //  textloading.SetActive(true);
    }
}
