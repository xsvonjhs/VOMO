using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipi : MonoBehaviour {

    public GameObject pipi_1;

	// Use this for initialization
	void Start () {
        transform.position = Random.insideUnitCircle*1;
        //Instantiate(pipi_1, new Vector3(2.0f, 0, 0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
