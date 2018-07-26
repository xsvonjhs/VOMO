using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadImage : MonoBehaviour {

    public RawImage img;
    public string url;

    private void Awake()
    {
        img = this.gameObject.GetComponent<RawImage>();
    }

    IEnumerator Start()
    {
        WWW www = new WWW(url);
        yield return www;
        img.texture = www.texture;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
