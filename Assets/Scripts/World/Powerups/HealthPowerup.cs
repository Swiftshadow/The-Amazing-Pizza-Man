using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : PowerupBase
{
    public int healAmount = 10;

    protected override void ApplyPowerup()
    {
        BasePlayerBehaviour player = GameObject.FindWithTag("Player").GetComponent<BasePlayerBehaviour>();

        player.health += healAmount;
        
        // TODO: Make max health a varaible
        if (player.health > 100)
        {
            player.health = 100;
        }
    }
}
