using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpAbility : Ability
{

    public DoubleJumpAbility(AbilityController _controller) : base(_controller)
    {
        name = "Double Jump";
        cooldown = 6f;
        cost = 1;
        levelRequirement = 2;
    }

    public override void Activate()
    {
        // Play Sound
        PlaySound("DoubleJump");

        // Spawn Particles
        //SpawnParticles("DoubleJump");

        // Double Jump
        DoubleJump();
    }
}
