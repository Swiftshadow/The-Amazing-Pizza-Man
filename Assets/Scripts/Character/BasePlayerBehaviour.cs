/*****************************************************************************
// File Name :         BasePlayerBehaviour.cs
// Author :            Andrew Krenzel (100%)
// Creation Date :     2/13/2020
//
// Brief Description : Controls the overall behaviors of the players that 
                       transfer across forms.
*****************************************************************************/

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BasePlayerBehaviour : MonoBehaviour
{
    // Player Stats
    public int health;
    public int lives;

    public float speed;
    public float pizzaSpeedMultiplier;

    public float pizzaDamageReduction;
    
    // Lives
    // Speed

    // Component References
    private Rigidbody2D rb2d; //(AK 11)
    private SpriteRenderer sR; //(AK 12)
    private Animator anim; //(AK 13)
    private DistanceJoint2D joint; //(AK 37)
    private BoxCollider2D humanCollider;
    private CircleCollider2D pizzaCollider;
    private LineRenderer lineRenderer;

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
        GetComponents();
        
        playerHuman = true; 
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        FormSwitch();
        PlayerMovement();
        GrapplingHook();
        GrappleLength();

        if (Input.GetKeyDown("1"))
        {
            SceneManager.LoadScene("WorldGenTest");
        }

        if (joint.enabled == true)
        {
            lineRenderer.enabled = true;
            
            var points = new Vector3[] {joint.connectedAnchor, 
                                        gameObject.transform.position};
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(points);
        }
        else if (joint.enabled == false)
        {
            lineRenderer.enabled = false;
        }

    }

    /// <summary>
    /// Gets all of the components that are called in the code in a condensed
    /// function.
    /// </summary>
    private void GetComponents() 
    {
        rb2d = GetComponent<Rigidbody2D>(); 
        sR = GetComponent<SpriteRenderer>(); 
        anim = GetComponent<Animator>(); 
        joint = GetComponent<DistanceJoint2D>(); 
        humanCollider = GetComponent<BoxCollider2D>();
        pizzaCollider = GetComponent<CircleCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

   
    private void FormSwitch()
    {
        if(Input.GetButtonDown("FormSwitch")) //(AK 4)
        {
            playerHuman = !playerHuman; //(AK 5)

            if(playerHuman == true) //(AK 6)
            {
                // Sets the joint to false when the player transform from pizza
                // to human 
                joint.enabled = false;
                sR.sprite = humanDefaultSprite; //(AK 17)
                rb2d.velocity = Vector2.zero;
                
                // Sets the appropriate hitbox to be active
                humanCollider.enabled = true;
                pizzaCollider.enabled = false;
                
                gameObject.transform.localScale = new Vector3(3, 3);

                CancelInvoke();

                gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                rb2d.freezeRotation = true;
            }
            else //(AK 7)
            {
                sR.sprite = pizzaDefaultSprite; //(AK 18)
                
                gameObject.transform.localScale = new Vector3(2, 2);

                rb2d.freezeRotation = false;
                
                InvokeRepeating("SpawnGrease", 0f, 0.1f);
                
                // Sets the appropriate hitbox to be active
                humanCollider.enabled = false;
                pizzaCollider.enabled = true;
            }
        }
    }
    
    private void PlayerMovement()
    {
        if (playerHuman == true) 
        {
            float xMove = Input.GetAxis("Horizontal"); 
            float yMove = Input.GetAxis("Vertical"); 
            
            Vector3 newPos = transform.position;

            newPos.x += xMove * Time.deltaTime * speed; 
            newPos.y += yMove * Time.deltaTime * speed; 

            transform.position = newPos; 
        }
        else if (playerHuman == false) 
        {
            // Gets the player inputs 
            float xMove = Input.GetAxis("Horizontal"); 
            float yMove = Input.GetAxis("Vertical"); 

        
            xMove = xMove * speed * Time.deltaTime * pizzaSpeedMultiplier; 
        
            Vector2 moveForce = new Vector2(xMove, yMove); 
        
            float velocityCap = 10f;

            moveForce.x = Mathf.Clamp(moveForce.x, -velocityCap, velocityCap); 
            moveForce.y = Mathf.Clamp(moveForce.y, -velocityCap, velocityCap); 
        
            rb2d.AddForce(moveForce); 
        }
    }
    
    
    /// <summary>
    /// Controls the behaviour and functionality of the pizza's grappling hook
    /// ability
    /// Written by Andrew Krenzel
    /// Adapted from Wabble - Unity Tutorials Grappling Hook Tutorial Series
    /// </summary>
    private void GrapplingHook()
    {
        if (Input.GetButtonDown("Fire1") && playerHuman == false)
        {
            Vector3 screenPos = Input.mousePosition;
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(screenPos);
            Debug.Log("ScreenPos = " + screenPos + " WorldPos = " + targetPos);

            joint.connectedAnchor = targetPos;
            //joint.distance = Vector3.Distance(targetPos, transform.position);
            joint.enabled = true;

            
        }
        // On Mouse Up, clears existing values and when it goes down it resests them
        else if (Input.GetButtonUp("Fire1") && playerHuman == false)
        {
            joint.enabled = false;
        }
    }

    private void GrappleLength()
    {
        if (joint.enabled == true)
        {
            joint.distance = Vector2.Distance(transform.position, joint.connectedAnchor);

            // Move Towards
        }
    }
    
    

    private void SpawnGrease()
    {
        Instantiate(greaseTrail, transform.position, Quaternion.identity);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Grease")
        {
            rb2d.velocity *= new Vector2(0.5f, 0.5f);
        }
    }
}