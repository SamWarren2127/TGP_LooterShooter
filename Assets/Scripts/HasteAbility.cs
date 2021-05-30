﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasteAbility : Ability
{

    public HasteAbility(AbilityController _controller) : base(_controller)
    {
        //Using base constructor
        name = "Haste";
        cooldown = 4f;
    }

    public override void Activate()
    {
        Debug.Log("Ability_Haste.Activate();");

        // Increase movement speed
        IncreaseMoveSpeed(2f);

        // Play Sound
        //PlaySound("Haste");

        // UI / Particle Effect
        //SpawnParticles("hasteEffect");
    }
}
