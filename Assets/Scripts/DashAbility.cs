using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : Ability
{

    public DashAbility(AbilityController _controller) : base(_controller)
    {
        // using base constructor
        name = "Dash";
        cooldown = 6f;
        cost = 2;
        levelRequirement = 2;
    }

    public override void Activate()
    {
        // Play Sound
        PlaySound("Dash");

        // Spawn Particles
        //SpawnParticles("dashParticles");

        // Dash
        Dash();
    }
}
