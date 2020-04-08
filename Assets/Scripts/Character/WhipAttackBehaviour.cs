/*****************************************************************************
// File Name :         WhipAttackBehaviour.cs
// Author :            Andrew Krenzel
// Creation Date :     March 05, 2020
//
// Brief Description : ADD BRIEF DESCRIPTION OF THE FILE HERE
*****************************************************************************/
using UnityEngine;

public class WhipAttackBehaviour : MonoBehaviour
{
    private ParticleSystem ps;
    private Rigidbody2D rb2d;
    private CircleCollider2D collider;

    private Vector3 effectSize;

    public float forceValue;
    public float effectLifeTime;
    
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        rb2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();

        Destroy(gameObject, effectLifeTime);
    }

    void Update()
    {
        forceValue += forceValue;
        effectSize = new Vector3(forceValue, forceValue, 0);
        gameObject.transform.localScale = effectSize;
        //collider.radius += forceValue;
        
    }
    
}