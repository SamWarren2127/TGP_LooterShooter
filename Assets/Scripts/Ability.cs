using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability
{
    protected float cooldown;
    protected string name;
    protected AbilityController _abilityController;

    public Ability(AbilityController _controller)
    {
        _abilityController = _controller;
    }

    public abstract void Activate();

    protected void SpawnParticles(string _particleEffect)
    {
        _abilityController.SpawnParticles(_particleEffect);
    }

    protected void PlaySound(string _sound)
    {
        _abilityController.PlaySound(_sound);
    }

    protected void Heal(float _healAmount)
    {
        _abilityController.Heal(_healAmount);
    }

    protected void Move(Vector3 _moveDir)
    {
        _abilityController.Move(_moveDir);
    }

    protected void IncreaseMoveSpeed(float _moveMult)
    {
        _abilityController.IncreaseMoveSpeed(_moveMult);
    }

    protected void DoubleJump()
    {
        _abilityController.DoubleJump();
    }

    protected void Dash()
    {
        _abilityController.Dash();
    }

    public string GetName()
    {
        return name;
    }

    public float GetCooldown()
    {
        return cooldown;
    }
}
