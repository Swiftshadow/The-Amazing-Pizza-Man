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
    
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        rb2d = GetComponent<Rigidbody2D>();
        collider = GetComponent<CircleCollider2D>();

        Destroy(gameObject, 0.2f);
    }

    void Update()
    {
        collider.radius += 1f;
    }
    
}