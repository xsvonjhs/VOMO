using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gyro : MonoBehaviour {

    GameObject camParent;

	// Use this for initialization
	void Start () {
        camParent = new GameObject("CamParent");
        camParent.transform.position = this.transform.position;
        this.transform.parent = camParent.transform;
        Input.gyro.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        camParent.transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y, 0);
        this.transform.Rotate(-1.5f*Input.gyro.rotationRateUnbiased.x, 0, 0);
	}
}
