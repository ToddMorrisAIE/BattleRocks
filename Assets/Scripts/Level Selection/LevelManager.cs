using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public GameObject level1Butt;
    public GameObject level2Butt;
    public GameObject level3Butt;
    public GameObject level4Butt;
    public GameObject level5Butt;
    public GameObject level6Butt;
    public GameObject level7Butt;
    public GameObject level8Butt;
    public GameObject level9Butt;
    public GameObject level10Butt;

    // Use this for initialization
    void Start ()
    {

        //An extremely "Hacky" method for only displaying unlocked levels
        //If the Level has been unlocked, display the UI button

        if (_GameManager.instance.unlockedLevels[2] == 1)
        {
            level2Butt.SetActive(true);
        }
        else
        {
            level2Butt.SetActive(false);
        }

        if (_GameManager.instance.unlockedLevels[3] == 1)
        {
            level3Butt.SetActive(true);
        }
        else
        {
            level3Butt.SetActive(false);
        }

        if (_GameManager.instance.unlockedLevels[4] == 1)
        {
            level4Butt.SetActive(true);
        }
        else
        {
            level4Butt.SetActive(false);
        }

        if (_GameManager.instance.unlockedLevels[5] == 1)
        {
            level5Butt.SetActive(true);
        }
        else
        {
            level5Butt.SetActive(false);
        }

        if (_GameManager.instance.unlockedLevels[6] == 1)
        {
            level6Butt.SetActive(true);
        }
        else
        {
            level6Butt.SetActive(false);
        }


        if (_GameManager.instance.unlockedLevels[7] == 1)
        {
            level7Butt.SetActive(true);
        }
        else
        {
            level7Butt.SetActive(false);
        }

        if (_GameManager.instance.unlockedLevels[8] == 1)
        {
            level8Butt.SetActive(true);
        }
        else
        {
            level8Butt.SetActive(false);
        }

        if (_GameManager.instance.unlockedLevels[9] == 1)
        {
            level9Butt.SetActive(true);
        }
        else
        {
            level9Butt.SetActive(false);
        }

        if (_GameManager.instance.unlockedLevels[10] == 1)
        {
            level10Butt.SetActive(true);
        }
        else
        {
            level10Butt.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	    



    }
}
