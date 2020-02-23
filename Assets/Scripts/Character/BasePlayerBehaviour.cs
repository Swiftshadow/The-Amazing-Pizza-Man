/*****************************************************************************
// File Name :         BasePlayerBehaviour.cs
// Author :            Andrew Krenzel (100%)
// Creation Date :     2/13/2020
//
// Brief Description : Controls the overall behaviors of the players that 
                       transfer across forms.
*****************************************************************************/
using UnityEngine;

// Required Components
//[RequireComponent(typeof(Rigidbody2D))] //(AK 8)
//[RequireComponent(typeof(SpriteRenderer))] //(AK 9)
//[RequireComponent(typeof(Animator))] //(AK 10)

public class BasePlayerBehaviour : MonoBehaviour
{
    // Player Stats
    public int health; //(AK 21)
    public int lives; //(AK 22)

    public float speed;
    public float pizzaMultiplier;
    
    // Lives
    // Speed

    // Component References
    private Rigidbody2D rb2d; //(AK 11)
    private SpriteRenderer sR; //(AK 12)
    private Animator anim; //(AK 13)

    // Object References
    [Header("Sprites")]
    [Tooltip("The default sprite for the human form")]
    public Sprite humanDefaultSprite;
    [Tooltip("The default sprite for the pizza form")]
    public Sprite pizzaDefaultSprite;
    
    [Header("GameObjects")]
    [Tooltip("The prefab for the pizza's grease trail")]
    public GameObject greaseTrail;


    public bool playerHuman; //(AK 1)
    
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //(AK 14)
        sR = GetComponent<SpriteRenderer>(); //(AK 15)
        anim = GetComponent<Animator>(); //(AK 16)

        playerHuman = true; //(AK 2)
        
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if(Input.GetButtonDown("FormSwitch")) //(AK 4)
        {
            playerHuman = !playerHuman; //(AK 5)

            if(playerHuman == true) //(AK 6)
            {
                sR.sprite = humanDefaultSprite; //(AK 17)
                rb2d.velocity = Vector2.zero;
            }
            else //(AK 7)
            {
                sR.sprite = pizzaDefaultSprite; //(AK 18)
            }
        }
        
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (playerHuman == true) //(AK 23)
        {
            float xMove = Input.GetAxis("Horizontal"); //(AK 24)
            float yMove = Input.GetAxis("Vertical"); //(AK 25)
            
            Vector3 newPos = transform.position;//(AK 26)

            newPos.x += xMove * Time.deltaTime * speed; //(AK 27)
            newPos.y += yMove * Time.deltaTime * speed; //(AK 28)

            transform.position = newPos; //(AK 29)
        }
        else if (playerHuman == false) //(AK 30)
        {
            // Gets the player inputs 
            float xMove = Input.GetAxis("Horizontal"); //(AK 19)
            float yMove = Input.GetAxis("Vertical"); //(AK 20)

        
            xMove = xMove * speed * Time.deltaTime * pizzaMultiplier; //(AK 31)
        
            Vector2 moveForce = new Vector2(xMove, yMove); //(AK 32)
        
            float velocityCap = 10f; //(AK 33)

            moveForce.x = Mathf.Clamp(moveForce.x, -velocityCap, velocityCap); //(AK 34)
            moveForce.y = Mathf.Clamp(moveForce.y, -velocityCap, velocityCap); //(AK 35)
        
            rb2d.AddForce(moveForce); //(AK 36)
            
            Debug.Log(moveForce); //(AK 37)
        }
        /*
        if(Input.GetButton("Horizontal")||Input.GetButton(("Vertical")))
        {
            // Gets the player inputs 
            float xMove = Input.GetAxis("Horizontal"); //(AK 19)
            float yMove = Input.GetAxis("Vertical"); //(AK 20)

        
            xMove = xMove * speed * Time.deltaTime;
        
            Vector2 moveForce = new Vector2(xMove, yMove);
        
            float velocityCap = 10f;

            moveForce.x = Mathf.Clamp(moveForce.x, -velocityCap, velocityCap);
            moveForce.y = Mathf.Clamp(moveForce.y, -velocityCap, velocityCap);
        
            rb2d.AddForce(moveForce);
            
            Debug.Log(moveForce);
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
        */
         
    }

    
}