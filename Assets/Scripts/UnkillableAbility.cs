using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnkillableAbility : Ability
{
    public UnkillableAbility(AbilityController _controller) : base(_controller)
    {
        // Using the base constructor
        name = "Unkillable";
        cooldown = 4f;
    }

    public override void Activate()
    {
        // Play Sound

        // Spawn particles

        // Make Unkillable
        _abilityController.UnkillableForATime(5f);
    }
}
