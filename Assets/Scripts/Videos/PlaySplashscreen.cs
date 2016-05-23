using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]

public class PlaySplashscreen : MonoBehaviour {

    public MovieTexture SplashScreenVideo;
    private AudioSource SplashScreenAudio;

	// Use this for initialization
	void Start ()
    {
        GetComponent<RawImage>().texture = SplashScreenVideo as MovieTexture;
        SplashScreenAudio = GetComponent<AudioSource>();
        SplashScreenAudio.clip = SplashScreenVideo.audioClip;
        SplashScreenVideo.Play();
        SplashScreenAudio.Play();
	}
	
	// Update is called once per frame
	void Update () {
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) == true || SplashScreenVideo.isPlaying == false)
        {
            GameObject.Find("FadeScreen").GetComponent<SceneFadeInAndOut>().LoadNewScene("MenuMain");
        }
	
	}
}
