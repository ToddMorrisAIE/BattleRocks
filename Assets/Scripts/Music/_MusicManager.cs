using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[RequireComponent(typeof(AudioSource))]

public class _MusicManager : MonoBehaviour {

    private static _MusicManager _instance;                                  // - Internal GameManager reference.
    public static _MusicManager instance                                     // - External GameManager reference for all other scenes.
    #region MusicManager instance.       
    {
        get
        {
            if (_instance != null)                                           // - If _instance not null.
            {
                return _instance;                                           // - Return current _instance.
            }
            else                                                            // - Else (if _instance not null).
            {
                GameObject musicManager = new GameObject("MusicManager");     // - Set gameMnager as a new GameObject called GameManager.
                _instance = musicManager.AddComponent<_MusicManager>();       // - Add GameManager script to that new GameObject _instance.
                return _instance;                                           // - Return that new instance of GameManager as _instance.
            }
        }
    }
    #endregion  

    public AudioClip[] MusicFiles;
    public AudioClip[] SoundFiles;
    private AudioSource MasterAudioStream;
    private bool FadeOut;
    //private float MusicVolume;
    public SoundBite SoundLoader;
    public Stack<GameObject> LoadedSounds;

    // This Enum lists the name of all the Music effects in the Library. As new ones added to the engine, must be addd here in same order
    public enum TypesOfMusic
    {
        ARIETTA_WHIMSICAL = 0,
        CARTOON_STYLE_BACKGROUND = 1,
        CAT_AND_MOUSE_PLAYFUL = 2,
        MYSTICAL_ROCKY = 3,
        MAIN_MENU_STYLE_PLAYFUL = 4,
        GLORY_UPBEAT = 5,
        BACKGROUND_STYLE_ADVENTUROUS = 6,
        BACKGROUND_STYLE_WHIMSICAL_PLAYFUL = 7
    }

    // This Enum lists the name of all the sound effects in the Library. As new ones added to the engine, must be addd here in same order
    public enum TypesOfSounds  
    {
        CUTE_GIGGLE = 0,
        CARTOON_POPUP = 1,
        WINDOW_TAPPING = 2,
        LIGHTSABER_SWISH = 3,
        EEEEE = 4,
        AHHHHH = 5,
        OH_OH = 6,
        BUBBLES = 7,
        BLOCK_THUD = 8,
        SWORD_MISS = 9,
        BATTLE_HORN = 10,
        SWORD_DRAW = 11,
        CANON = 12,
        EVIL_LAUGH = 13,
        WILHELM_SCREAM = 14,
        BURP = 15,
        GO = 16,
        EXPLOSION = 17,
        GAME_WHISTLE = 18,
        FIGHT = 19,
        SUCCESS_TRUMPET = 20,
        DREAM_HARP = 21,
        SLAP_HIT = 22,
        HORN_BEEP_BEEP = 23,
        WAR_DRUMS = 24
    }

    private TypesOfMusic AudioTransition; // Keeps track of the transitioning audio music while it is being faded and changed

    TypesOfMusic MusicMaster;

    // Dictionary Lookups for the Music and Sound library to make them eaiser to use from code
    Dictionary<int, AudioClip> MusicLookUp = new Dictionary<int, AudioClip>();  
    Dictionary<int, AudioClip> SoundLookUp = new Dictionary<int, AudioClip>();

    void Awake()
    {
        // Set _instance as this script.
        if (_instance != this)                                   // - If _instance isnt this GameManager.
        {
            if (_instance == null)                               // - If no _instance of GameManager.
            {
                _instance = this;                               // - Set _instance to this GameManager.
            }
            else                                                // - If one already exists.
            {
                Destroy(gameObject);                            // - Destory this extra one.
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < MusicFiles.Length; i++)
        {
            MusicLookUp.Add(i, MusicFiles[i]);  // Adds the muic files to the Dictionary to make playing easier
        }

        for (int i = 0; i < SoundFiles.Length; i++)
        {
            SoundLookUp.Add(i, SoundFiles[i]); // Adds the sound files to the Dictionary to make playing easier
        }


        MasterAudioStream = GetComponent<AudioSource>();
        MusicMaster = TypesOfMusic.MAIN_MENU_STYLE_PLAYFUL;
        MasterAudioStream.clip = MusicLookUp[(int)MusicMaster];
        MasterAudioStream.Play();           // Begins playing the main music on the master music channel

        LoadedSounds = new Stack<GameObject>();
        
	}

    // Update is called once per frame
    void Update()
    {
        if (FadeOut == true && MusicMaster != AudioTransition)  // Fades the music out and fades the new music in
        {
            MusicFade();
        }
        else
        {
            if (FadeOut == true && MusicMaster == AudioTransition)
            {
                MusicGrow();
            }
        }

        if (LoadedSounds.Count > 0) // Removes the sound objects from the scene once they are finished playing (only half works)
        {

            if (LoadedSounds.Peek().GetComponent<SoundBite>().SoundPlaying == false)
            {
                GameObject SoundToDelete = LoadedSounds.Pop();
                DestroyObject(SoundToDelete);
            }

        }
    }

    public void ChangeMusic(TypesOfMusic NewMusic)  // Used to change the Music that is playing
    {
        if (MusicMaster == NewMusic)
        {
            return;
        }
        else
        {
            AudioTransition = NewMusic;
            this.FadeOut = true;
        }

    }

    void MusicFade() // The Function that actually fades the current music track
    {
        if (MasterAudioStream.volume >= 0.0f)
        {
            float CurrentVolume = MasterAudioStream.volume;
            CurrentVolume -= 1.0f * Time.deltaTime;

            if (CurrentVolume < 0.0f)
            {
                MusicMaster = AudioTransition;
            }
            else
            {
                MasterAudioStream.volume = CurrentVolume;
            }
        }
    }


    void MusicGrow()  // The function that makes the new music track return to normal volume
    {
        if (MasterAudioStream.clip != MusicLookUp[(int)MusicMaster])
        {
            MasterAudioStream.Stop();
            MasterAudioStream.clip = MusicLookUp[(int)MusicMaster];
            MasterAudioStream.Play();
            MasterAudioStream.volume = 0.0f;
        }

        if (MasterAudioStream.volume <= 1.0f)
        {
            float CurrentVolume = MasterAudioStream.volume;
            CurrentVolume += 0.5f * Time.deltaTime;

            if (CurrentVolume > 1.0f)
            {
                FadeOut = false;
            }
            else
            {
                MasterAudioStream.volume = CurrentVolume;
            }
        }

    }

    public void PlaySoundEffect(TypesOfSounds SoundToPlay) // This function simply takes a sound file type from the dictionary and plays it 
    {
        GameObject Temp = Instantiate(SoundLoader.gameObject, transform.position, transform.rotation) as GameObject;
        Temp.transform.SetParent(transform);

        SoundBite SbTemp = Temp.GetComponent<SoundBite>();
        SbTemp.Init(SoundLookUp[(int)SoundToPlay]);
        LoadedSounds.Push(Temp);
    }
}
