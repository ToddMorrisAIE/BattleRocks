using UnityEngine;
using System.Collections;

public class PotMovement : MonoBehaviour {


    [SerializeField] GameObject pot;
    [SerializeField] GameObject potShadow;
    [SerializeField] GameObject playerRock = null;
    [SerializeField] GameObject enemyRock = null;


    // Pots Up/Down Variables
    private Vector3 initPos;
    private Vector3 initRot;
    [SerializeField] Vector3 endPos = new Vector3(0,1,0);
    [SerializeField] float moveSpeed = 13;
    public bool potMove;
    [SerializeField] bool potDown;

    // Shadow Variables.
    private Vector3 shadowScale;
    [SerializeField] float shadowAlphaChangeSpeed = 0;
    [SerializeField] float shadowGrowSpeed;
    public SpriteRenderer shadowRenderer;
    [SerializeField] Color shadowColour;

    // Set Rock Variables.
    [SerializeField] Color rockStartColour;
    public SpriteRenderer playerRenderer;
    public SpriteRenderer enemyRenderer;
    public SpriteRenderer playerChildRenderer;
    public SpriteRenderer enemyChildRenderer;

    private Animator animator; // stores animator.

	void Start () {
        // Set Player and Enemy.
	    playerRock = GameObject.FindGameObjectWithTag("Player");
        enemyRock = GameObject.FindGameObjectWithTag("Enemy");

        // Set Shadow Variables.
        shadowRenderer = potShadow.GetComponent<SpriteRenderer>();
        shadowColour = shadowRenderer.color;

        // Set Rock Variables.
        playerRenderer = playerRock.GetComponent<SpriteRenderer>();
        enemyRenderer = enemyRock.GetComponent<SpriteRenderer>();
        rockStartColour = playerRenderer.color;


        // Set Pots Up/Down Variables
        initPos = pot.transform.position;
        initRot = pot.transform.eulerAngles;
        potDown= true;

	}
	
	// Update is called once per frame
	void Update () 
    {
        if(potMove == true)
        {
            PotUpDown();
        }
	}

    void PotUpDown()
    {
        if(potDown == true)
        {
            pot.transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
            shadowScale.x += shadowGrowSpeed;
            shadowScale.y += shadowGrowSpeed;
            shadowRenderer.color = Color.Lerp(potShadow.GetComponent<SpriteRenderer>().color, shadowColour, shadowAlphaChangeSpeed * Time.deltaTime);
            playerRenderer.color = Color.Lerp(playerRock.GetComponent<SpriteRenderer>().color, Color.black, shadowAlphaChangeSpeed * 0.007f);
            enemyRenderer.color = Color.Lerp(enemyRock.GetComponent<SpriteRenderer>().color, Color.black, shadowAlphaChangeSpeed * 0.007f);
            foreach(Transform child in playerRock.transform)
            {
                try
                {
                playerChildRenderer = child.GetComponent<SpriteRenderer>();
                playerChildRenderer.color = Color.Lerp(enemyRock.GetComponent<SpriteRenderer>().color, Color.black, shadowAlphaChangeSpeed * 0.007f);
                }
                catch
                {

                }
            }
            foreach(Transform child in enemyRock.transform)
            {
                try
                {
                    enemyChildRenderer = child.GetComponent<SpriteRenderer>();
                    enemyChildRenderer.color = Color.Lerp(enemyRock.GetComponent<SpriteRenderer>().color, Color.black, shadowAlphaChangeSpeed * 0.007f);
                }
                catch
                {

                }
            }
            potShadow.transform.localScale = shadowScale;
            if (pot.transform.position.y <= endPos.y)
            {
                //playerRenderer.enabled = false;
                //enemyRenderer.enabled = false;
                potMove = false;
                potDown = false;
            }
        }
        if (potDown == false)
        {
            playerRenderer.enabled = true;
            enemyRenderer.enabled = true;
            pot.transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            shadowScale.x -= shadowGrowSpeed;
            shadowScale.y -= shadowGrowSpeed;
            shadowRenderer.color = Color.Lerp(potShadow.GetComponent<SpriteRenderer>().color, Color.clear, shadowAlphaChangeSpeed * Time.deltaTime);
            playerRenderer.color = Color.Lerp(playerRock.GetComponent<SpriteRenderer>().color, rockStartColour, shadowAlphaChangeSpeed * 0.007f);
            enemyRenderer.color = Color.Lerp(enemyRock.GetComponent<SpriteRenderer>().color, rockStartColour, shadowAlphaChangeSpeed * 0.007f);
            foreach(Transform child in playerRock.transform)
            {
                try
                {
                    playerChildRenderer = child.GetComponent<SpriteRenderer>();
                    playerChildRenderer.color = Color.Lerp(playerRock.GetComponent<SpriteRenderer>().color, rockStartColour, shadowAlphaChangeSpeed * 0.007f);
                }
                catch
                {

                }
            }
            foreach(Transform child in enemyRock.transform)
            {
                try
                {
                    enemyChildRenderer = child.GetComponent<SpriteRenderer>();
                    enemyChildRenderer.color = Color.Lerp(enemyRock.GetComponent<SpriteRenderer>().color, rockStartColour, shadowAlphaChangeSpeed * 0.007f);
                }
                catch
                {

                }
            }
            potShadow.transform.localScale = shadowScale;
            if (pot.transform.position.y > initPos.y)
            {
                potMove = false;
                potDown = true;
            }
        }
    }
}
