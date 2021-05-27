using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : Ability
{
    public DashAbility(AbilityController _controller) : base(_controller)
    {
        // using base constructor
        name = "Dash";
        cooldown = 4f;
    }

    public override void Activate()
    {
        // Play Sound
        //PlaySound("dashSound");

        // Spawn Particles
        //SpawnParticles("dashParticles");

        // Dash
        Dash();
    }
}
