using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio : MonoBehaviour {

    public string url;
    //public AudioClip source;

	// Use this for initialization
	IEnumerator Start () {
        // Start a download of the given URL
        WWW www = new WWW(url);

        //Wait for download to complete
        yield return www;

        /*
        //get text
        source = www.GetAudioClip(false, false);
        audio.clip = source;
        //having audio here. Do with it whatever you want...
        */

        AudioSource _source = GetComponent<AudioSource>();
        _source.clip = www.GetAudioClip(false, true, AudioType.MPEG);
        _source.Play();
		
	}
	
	// Update is called once per frame
	void Update () {
	}

    /*
    private void OnGUI()
    {
        if (null != source)
        {
            if (!audio.isPlaying)
                audio.Play();
        }

        else
            GUI.Label(new Rect(10, 10, 200, 30), "Loading...");
    }
    */
}
