using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : Ability
{
    float healAmount = 0.2f;

    public HealAbility(AbilityController _controller) : base(_controller)
    {
        //Using base constructor
        name = "Heal";
        cooldown = 8f;
        cost = 0;
        levelRequirement = 1;
        isUnlocked = true;
    }

    public override void Activate()
    {
        Debug.Log("Ability_Heal.Activate();");

        // Heal player
        Heal(healAmount);

        // Slightly increase move speed
        IncreaseMoveSpeed(1.2f);

        // Play Sound
        PlaySound("Heal");

        // UI / Particle Effect
        // String should be name of the prefab
        SpawnParticles("HealParticle");
    }
}
