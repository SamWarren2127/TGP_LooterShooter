using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpAbility : Ability
{
    AudioManager audioManager;

    public DoubleJumpAbility(AbilityController _controller) : base(_controller)
    {
        name = "Double Jump";
        cooldown = 3f;
    }

    public override void Activate()
    {
        // Play Sound
        AudioManager.FindObjectOfType<AudioManager>().Play("DoubleJump");

        // Spawn Particles
        //SpawnParticles("DoubleJump");

        // Double Jump
        DoubleJump();
    }
}
