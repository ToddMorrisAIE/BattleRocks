using UnityEngine;
using System.Collections;

// This class designed to create a new sound object and holds its information while being played. Is destroyed after completed playing
public class SoundBite : MonoBehaviour {

    AudioClip MySound;   // Holds the sound that is part of this object
    public bool SoundPlaying;
    float PlayTimer;

	public void Init(AudioClip AudioToPlay) // Actually loads & plays the sound from itself
    {
        MySound = AudioToPlay;
        this.GetComponentInChildren<AudioSource>().clip = MySound;
        this.GetComponentInChildren<AudioSource>().Play();
        PlayTimer = MySound.length;
        SoundPlaying = true;
    }

    public void Update() // Function to keep track of how long it has been playing for deletion (only half works)
    {
        PlayTimer -= Time.deltaTime;

        if (PlayTimer <= 0.0f)
        {
            SoundPlaying = false;
        }
    }
}
