/*****************************************************************************
// File Name :         BasePlayerBehaviour.cs
// Author :            Andrew Krenzel (95%)
// Creation Date :     2/13/2020
//
// Brief Description : Controls the overall behaviors of the players that 
                       transfer across forms.
*****************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using UnityEngine.SceneManagement;

public class BasePlayerBehaviour : MonoBehaviour
{
    [Header("Player Stats")]
    [Tooltip("The player's starting health value")]
    public int health;
    [Tooltip("The player's total number of lives")]
    public int lives;
    //[Tooltip("The player's total number of lives")]
    public float speed;
    //[Tooltip("The player's total number of lives")]
    public float humanSpeedMultiplier;
    //[Tooltip("The player's total number of lives")]
    public float pizzaSpeedMultiplier;
    //[Tooltip("The player's total number of lives")]
    public float slimeSlowAmount; //SJ
    //[Tooltip("The player's total number of lives")]
    public float pizzaDamageReduction;

    private bool playerInvulnerable;
    private bool playerInSlime;
    private bool whipCast;
    private bool whipReady;
    
    

    // Component References
    private Rigidbody2D rb2d; // AK
    private SpriteRenderer sR; // AK
    private Animator anim; // AK
    private DistanceJoint2D joint; // AK
    private BoxCollider2D humanCollider;
    private CircleCollider2D pizzaCollider;
    private LineRenderer lineRenderer;
    private TrailRenderer trailRenderer;

    // Object References
    [Header("Sprites")] 
    [Tooltip("The default sprite for the human form")]
    public Sprite humanDefaultSprite;

    [Tooltip("The default sprite for the pizza form")]
    public Sprite pizzaDefaultSprite;

    public GameObject whipEffect;

    [Header("GameObjects")] public GameObject humanForm;
    public GameObject pizzaForm;

    [Tooltip("The prefab for the pizza's grease trail")]
    public GameObject greaseTrail;

    [Tooltip("The prefab for the pizza's grease trail")]
    public GameObject meleeHitbox1;

    [Tooltip("The Body Parts for the Human Form")]
    public List<GameObject> limbObjects;
    
    public GameObject enemyParticles; //AK IR2

    public AudioClip hurtSound;
    
    // GameObject Properties
    private Vector3 humanScale = new Vector3(1, 1);
    private Vector3 pizzaScale = new Vector3(0.9f, 0.9f);

    public bool playerHuman; // AK 

    private bool canTransform = true;

    private bool isFlipped = false;

    //Taylor....Human behaviour
    public bool attack;
    public AudioClip punchClip;
    public GameObject attackHitbox;


    public LayerMask grappleLayer;

    // AK IR2 Flash Red Stuff
    private SpriteRenderer pizzaRend;


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        GetComponents();
        SetValues();
        playerHuman = true;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (Time.timeScale != 0)
        {
            FormSwitch();
            PlayerMovement();
            GrapplingHook();
            GrappleLength();
            WhipAttack(); // AK IR2
        }

        if (Input.GetKeyDown("`"))
        {
            transform.position = new Vector2(1000, 1000);
        }

        if (joint.enabled == true)
        {
            lineRenderer.enabled = true;

            var points = new Vector3[]
            {
                joint.connectedAnchor,
                gameObject.transform.position
            };
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(points);
        }
        else if (whipCast == true)
        {
            lineRenderer.enabled = true;


        }
        else if (joint.enabled == false && whipCast != true)
        {
            lineRenderer.enabled = false;
        }


        if (health <= 0)
        {
            FlashRed();
            Invoke("Respawn", 0.5f);
        }

        if (lives <= 0)
        {
            SceneManager.LoadScene("Death");
        }


        //Taylor... attack animation
        if (Input.GetKeyDown(KeyCode.Space) && playerHuman == true && !attack)
        {
            if (anim)
            {
                canTransform = false;
                anim.SetBool("Punch", true);
                playerInvulnerable = true;
                attack = true;
                Invoke("ResetAttack", .4f);
                GameObject hitbox = Instantiate(attackHitbox, transform);
                Vector2 hitboxSpawn = hitbox.transform.localPosition;
                hitboxSpawn.x = 0.172f;
                hitboxSpawn.y = 0.16f;
                hitbox.transform.localPosition = hitboxSpawn;
                AudioSource audio = GetComponent<AudioSource>();
                audio.clip = punchClip;
                audio.Play();
            }

        }
    }

    private void Respawn()
    {
        Instantiate(enemyParticles, transform.position, Quaternion.identity);
        ResetColor();
        health = 100;
        --lives;
        transform.position = new Vector2(0, 0);
    }

    private void ResetAttack()
    {
        playerInvulnerable = false;
        attack = false;
        canTransform = true;
        anim.SetBool("Punch", false);
    }

    /// <summary>
    /// Gets all of the components that are called in the code in a condensed
    /// function.
    /// </summary>
    private void GetComponents()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        joint = GetComponent<DistanceJoint2D>();
        humanCollider = GetComponentInChildren<BoxCollider2D>();
        pizzaCollider = GetComponentInChildren<CircleCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();

        // AK IR2
        pizzaRend = pizzaForm.GetComponent<SpriteRenderer>();
    }

    private void SetValues()
    {
        health = 100;
        lives = 3;

        playerInvulnerable = false;
        whipReady = true;
    }

    // General Functionalities

    private void FormSwitch()
    {
        if (Input.GetButtonDown("FormSwitch") && canTransform) // AK 
        {
            ResetColor();
            playerHuman = !playerHuman; // AK 

            if (playerHuman == true) // AK 6
            {
                humanForm.SetActive(true);
                pizzaForm.SetActive(false);

                // Sets the joint to false when the player transform from pizza
                // to human 
                joint.enabled = false;
                anim.SetBool("playerHuman", true); //(AK 18)
                rb2d.velocity = Vector2.zero;

                trailRenderer.emitting = false;

                foreach (GameObject limbs in limbObjects)
                {
                    enabled = true;
                }
                //limbs.

                gameObject.transform.localScale = humanScale;

                CancelInvoke();

                gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                rb2d.freezeRotation = true;
            }
            else //(AK 7)
            {
                humanForm.SetActive(false);
                pizzaForm.SetActive(true);

                anim.SetBool("playerHuman", false); //(AK 18)

                gameObject.transform.localScale = pizzaScale;

                rb2d.freezeRotation = false;

                foreach (GameObject limbs in limbObjects)
                {
                    //print(limbs.gameObject.name);
                    //limbs.gameObject.SetActive(true);
                }

                trailRenderer.emitting = true;

                InvokeRepeating("SpawnGrease", 0f, 0.1f);

            }
        }
    }

    private void PlayerMovement()
    {
        // Human movement needs to be 
        if (playerHuman == true)
        {
            // Gets the player inputs 
            float xMove = Input.GetAxis("Horizontal");
            float yMove = Input.GetAxis("Vertical");

            // Slow player if in slime - Shane
            if (playerInSlime) //SJ
            {
                xMove += xMove * speed * humanSpeedMultiplier * slimeSlowAmount;
                yMove += yMove * speed * humanSpeedMultiplier * slimeSlowAmount;

            }
            else
            {
                xMove += xMove * speed * humanSpeedMultiplier;
                yMove += yMove * speed * humanSpeedMultiplier;
            }

            Vector2 moveForce = new Vector2(xMove, yMove);

            float velocityCap = 5f;

            moveForce.x = Mathf.Clamp(moveForce.x, -velocityCap, velocityCap);
            moveForce.y = Mathf.Clamp(moveForce.y, -velocityCap, velocityCap);

            rb2d.AddForce(moveForce);

            /*
            if (moveForce.x < 0.0f)
            {
                sR.flipX = true;
            } */

            //animation


            if (Input.GetAxis("Horizontal") < 1 && Input.GetAxis("Vertical") < 1)
            {
                moveForce.x *= 0f;
                moveForce.y *= 0f;
            }

            // Flipping Player AK
            if (xMove < 0)
            {
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
                isFlipped = true;
            }
            else if (xMove > 0f)
            {
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
                isFlipped = false;
            }

            anim.SetFloat("xMove", xMove);
            anim.SetFloat("yMove", yMove);
        }

        else if (playerHuman == false)
        {
            // Gets the player inputs 
            float xMove = Input.GetAxis("Horizontal");
            float yMove = Input.GetAxis("Vertical");


            xMove += xMove * speed * pizzaSpeedMultiplier;
            yMove += yMove * speed * pizzaSpeedMultiplier;

            Vector2 moveForce = new Vector2(xMove, yMove);

            float velocityCap = 10f;

            moveForce.x = Mathf.Clamp(moveForce.x, -velocityCap, velocityCap);
            moveForce.y = Mathf.Clamp(moveForce.y, -velocityCap, velocityCap);

            rb2d.AddForce(moveForce);

        }
    }

    // Human Functionalities
    //Taylor

    //Andrew
    private void WhipAttack() // AK IR2
    {
        if (Input.GetButtonDown("Fire1") && playerHuman == true && whipReady == true)
        {
            Vector3 screenPos = Input.mousePosition;
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(screenPos);
            targetPos.z += 10f;
            //Debug.Log("ScreenPos = " + screenPos + " WorldPos = " + targetPos);

            whipCast = true;

            LayerMask player = LayerMask.GetMask("Player");

            RaycastHit2D hitWall = Physics2D.Linecast(transform.position, targetPos, grappleLayer);
            if (hitWall == true)
            {
                targetPos = hitWall.point;
                Debug.Log(hitWall.collider.name);
            }

            Vector3 targetPosLine = targetPos;
            targetPosLine.z -= 10f;

            Instantiate(whipEffect, targetPos, quaternion.identity);
            //Vector3 whipImpact = GameObject.FindWithTag("WhipAttack").transform.position;
            //Debug.Log("Whip Impact: " + whipImpact + " H " + "PlayerPos: " + gameObject.transform.position);

            var points = new Vector3[] {gameObject.transform.position, targetPosLine};

            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(points);

            lineRenderer.enabled = true;

            whipReady = false;

            Invoke("BreakWhip", .25f);
        }
    }

    private void BreakWhip()
    {
        whipCast = false;
        whipReady = true;
        //lineRenderer.enabled = false;
    }


    // Pizza Functionalities

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
            //Debug.Log("ScreenPos = " + screenPos + " WorldPos = " + targetPos);

            Vector2 delta = transform.position - targetPos;

            LayerMask player = LayerMask.GetMask("Player");

            RaycastHit2D hitWall = Physics2D.Linecast(transform.position, targetPos, grappleLayer);
            if (hitWall == true)
            {
                targetPos = hitWall.point;
                Debug.Log(hitWall.collider.name);
            }

            joint.connectedAnchor = targetPos;
            //joint.distance = Vector3.Distance(targetPos, transform.position);
            joint.enabled = true;

            rb2d.drag = 0.0f;


        }
        // On Mouse Up, clears existing values and when it goes down it resests them
        else if (Input.GetButtonUp("Fire1") && playerHuman == false)
        {
            joint.enabled = false;
            rb2d.drag = 3;
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

    private void DamageTick(EnemyBehavoiur enemy)
    {
        StartCoroutine(EnableIFrames());
        AudioSource.PlayClipAtPoint(hurtSound, transform.position, 5f);
        health -= enemy.damageValue;

        FlashRed();

    }

    
    // AK IR2
    private void FlashRed()
    {
        if (playerHuman)
        {
            foreach (GameObject limbs in limbObjects)
            {
                //DG
                if (limbs.GetComponent<SpriteRenderer>() == null)
                {
                    continue;
                }
                
                limbs.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                Debug.Log("Turned Red");
                Invoke("ResetColor", 0.4f);
                Debug.Log("Limbs Reset");
            }
        }
        else
        {
            Debug.Log("Attempt to flash pizza");
            pizzaForm.GetComponent<SpriteRenderer>().color = Color.red;
            Debug.Log("Pizza Red");
            Invoke("ResetColor", 0.4f);
            Debug.Log("Pizza Reset");
        }
    }
    
    // AK IR2
    private void ResetColor()
    {
        if (playerHuman == true)
        {
            
            foreach (GameObject limbs in limbObjects)
            {
                // DG
                if (limbs.GetComponent<SpriteRenderer>() == null)
                {
                    continue;
                }
                
                limbs.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        else if(playerHuman == false)
        {
            pizzaForm.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

   private IEnumerator EnableIFrames()
   {
       playerInvulnerable = true;
       yield return new WaitForSeconds(0.4f);
       playerInvulnerable = false;
   }
   
   private void OnTriggerEnter2D(Collider2D other) //SJ
   {
       if (!playerInvulnerable)
       {
           if (other.gameObject.CompareTag("Enemy"))
           {
               DamageTick(other.GetComponent<EnemyBehavoiur>());
           }
       }
        
       if (other.gameObject.CompareTag("Slime"))
       {
           playerInSlime = true;
       }
   }

   private void OnTriggerExit2D(Collider2D collision)
   {
       if (collision.gameObject.CompareTag("Slime"))
       {
           playerInSlime = false;
       }
   }
   
   // Pizza Grease Trail Sliding
   private void OnTriggerStay(Collider other)
   {
       if (other.CompareTag("Grease"))
       {
           rb2d.velocity *= new Vector2(0.5f, 0.5f);
       }

       if (other.CompareTag("Enemy"))
       {
           Invoke("DamageTick", 0.5f);
       }
   }
   
   private void OnCollisionEnter2D(Collision2D other)
   {
       if(other.gameObject.CompareTag("Enemy"))
       {
           if (playerInvulnerable == false)
           {
               DamageTick(other.gameObject.GetComponent<EnemyBehavoiur>());
           }
       }
   }
}