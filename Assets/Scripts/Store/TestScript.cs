using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	public void PlaySound()
    {
        _MusicManager.instance.PlaySoundEffect(_MusicManager.TypesOfSounds.DREAM_HARP);
    }


}
