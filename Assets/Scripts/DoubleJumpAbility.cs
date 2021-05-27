using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpAbility : Ability
{
    public DoubleJumpAbility(AbilityController _controller) : base(_controller)
    {
        name = "DoubleJump";
        cooldown = 3f;
    }

    public override void Activate()
    {
        // Play Sound
        //PlaySound("DoubleJump");

        // Spawn Particles
        //SpawnParticles("DoubleJump");

        // Double Jump
        DoubleJump();
    }
}
