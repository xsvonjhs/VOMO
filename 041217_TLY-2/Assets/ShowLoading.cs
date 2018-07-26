using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowLoading : MonoBehaviour {

    public Button Uploaddebtn;
    public GameObject LoadingLah;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }



    public void setactivetxt()
    {
        LoadingLah.SetActive(true);
        Debug.Log("Runnung!");
        var pointer = new PointerEventData(EventSystem.current); // pointer event for Execute
        ExecuteEvents.Execute(Uploaddebtn.gameObject, pointer, ExecuteEvents.pointerUpHandler);
    }

}


