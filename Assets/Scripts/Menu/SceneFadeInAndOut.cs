using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFadeInAndOut : MonoBehaviour 
{
    //[SerializeField]
	float fadeSpeed = 3f;          // Speed that the screen fades to and from black.
	
	
	private bool sceneStarting = true;      // Whether or not the scene is still fading in.
	private bool sceneEnding = false; 		// whether or not the scene is still fading out.

    private string sceneNameToLoad;
	
	void Awake()
	{
		GetComponent<SpriteRenderer>().color = Color.black;
	}
	void Update()
	{
        if(sceneStarting)
		{
			StartScene();	// ... call the StartScene function.
		}
		if(sceneEnding)
		{
			FadeToWhite();
		}
	}

	void FadeToClear()
	{
		// Lerp the colour of the texture between itself and transparent.
        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	void FadeToWhite()
	{
		if(GetComponent<SpriteRenderer>().color.a >= 0.99f)
		{
            // ... reload the scene.
            //Debug.Log("Testing screen load.");
            SceneManager.LoadScene(sceneNameToLoad);

            if(sceneNameToLoad.Contains("Battle"))
            {
                _MusicManager.FindObjectOfType<_MusicManager>().ChangeMusic(_MusicManager.TypesOfMusic.CAT_AND_MOUSE_PLAYFUL);
            }
            else if(sceneNameToLoad.Contains("Menu"))
            {
                _MusicManager.FindObjectOfType<_MusicManager>().ChangeMusic(_MusicManager.TypesOfMusic.CARTOON_STYLE_BACKGROUND);
            }
            else
            {
                _MusicManager.FindObjectOfType<_MusicManager>().ChangeMusic(_MusicManager.TypesOfMusic.CARTOON_STYLE_BACKGROUND);
            }
		}
		// Lerp the colour of the texture between itself and white.
        GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	void StartScene()
	{
		// Fade the texture to clear.
		FadeToClear();
		
		// If the texture is almost clear...
        if(GetComponent<SpriteRenderer>().color.a <= 0.01f)
		{
			// ... set the colour to clear and disable the GUITexture.
            GetComponent<SpriteRenderer>().color = Color.clear;
            GetComponent<SpriteRenderer>().enabled = false;
			
            // The scene is no longer starting.sceneStarting
			sceneStarting = false;
		}
	}

    public void LoadNewScene(string levelNameToLoad)
    {
        sceneNameToLoad = levelNameToLoad;
        GetComponent<SpriteRenderer>().enabled = true;// Make sure the texture is enabled.
        sceneStarting = false;
        sceneEnding = true;
    }
}
