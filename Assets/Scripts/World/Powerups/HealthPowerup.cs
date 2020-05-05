/*****************************************************************************
// File Name :         HealthPowerup.cs
// Author :            Doug Guzman
                       
// Creation Date :     4/15/2020
//
// Brief Description : Health powerup. Heals the player by a configurable
                       amount, and makes sure the player is not overhealed.
*****************************************************************************/
using UnityEngine;

public class HealthPowerup : PowerupBase
{
    [Tooltip("How much the player should heal by")]
    public int healAmount = 10;

    /// <summary>
    /// Heal the player
    /// </summary>
    protected override void ApplyPowerup()
    {
        // Find the player
        BasePlayerBehaviour player = GameObject.FindWithTag("Player").GetComponent<BasePlayerBehaviour>();

        // Increase the player's health
        player.health += healAmount;
        
        // TODO: Make max health a varaible
        // If their health is over the max, set it to the max
        if (player.health > 100)
        {
            player.health = 100;
        }
    }
}
