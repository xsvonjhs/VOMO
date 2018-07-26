using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class loadVideo : MonoBehaviour {

    public RawImage image;
    public VideoClip videoToPlay;
    public string VideoURL;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;

    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(playVideo());
    }

    IEnumerator playVideo()
    {
        //WWW www = new WWWW(VideoURL);
        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Add audioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio;
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        //We want to play from video clip not from url;
        videoPlayer.source = VideoSource.VideoClip;

        /*
        //video clip from url
        videoPlayer.source = VideoSource.Url;
        //videoPlayer.url = "http://quirksmode.org/html5/videos/big_buck_bunny.mp4";
        videoPlayer.url = VideoURL;
        */

        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay; // hide this when url
        videoPlayer.Prepare();

        //Waiy until video is prepared
        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            //Prepare/wait for 5 seconds only
            yield return waitTime;
            //Break out of the while loop after 5 seconds wait
            break;
        }

        Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;

        //play video
        videoPlayer.Play();

        //play sound
        audioSource.Play();

        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)
        {
            Debug.LogWarning("video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }

        Debug.Log("Done Playing Video");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
