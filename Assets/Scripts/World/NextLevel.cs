using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    private static int levelsCompleted = 0;
    private RoomGenerator generator;
    // Start is called before the first frame update
    void Start()
    {
        generator = GameObject.FindWithTag("Generator").GetComponent<RoomGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (levelsCompleted >= 2)
            {
                levelsCompleted = 0;
                SceneManager.LoadScene("WinScene");
            }
            
            ++levelsCompleted;
            generator.difficulty = (EnumList.RoomDifficulty) ((int) generator.difficulty << 1);
            Debug.Log("After bitshift, difficulty is " + generator.difficulty);
            generator.difficulty = generator.difficulty | (EnumList.RoomDifficulty)3;
            Debug.Log("After or, difficulty is " + generator.difficulty);
            
            generator.SpawnMap();
        }
    }

    /// <summary>
    /// Set the number of levels completed
    /// </summary>
    /// <param name="num">How many levels are complete</param>
    public static void SetLevelsComplete(int num)
    {
        levelsCompleted = num;
    }
}
