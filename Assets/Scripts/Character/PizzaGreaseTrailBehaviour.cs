using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PizzaGreaseTrailBehaviour : MonoBehaviour
{
    private SpriteRenderer sR;
    private float opacity;
    
    // Start is called before the first frame update
    void Start()
    {
        opacity = 1;
        sR = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        GreaseFade();
    }

    private void GreaseFade()
    {
        opacity *= 0.98f;
        sR.color = new Color(0.8679245f, 0.7270874f, 0.2087931f, opacity);
    }
}
