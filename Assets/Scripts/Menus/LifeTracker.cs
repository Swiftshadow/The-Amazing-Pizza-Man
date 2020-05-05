/*****************************************************************************
// File Name :         LifeTracker.cs
// Author :            Doug Guzman
                       
// Creation Date :     4/15/2020
//
// Brief Description : Shows and hides the life icons on the menu. Each life
                       icon gets this individually
*****************************************************************************/
using UnityEngine;

public class LifeTracker : MonoBehaviour
{
    private BasePlayerBehaviour player;

    [Tooltip("Which life this represents")]
    public int hideLifeAmount;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<BasePlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.lives < hideLifeAmount)
        {
            gameObject.SetActive(false);
        }
    }
}
