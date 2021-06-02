using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public enum EAbility
    {
        NONE,
        HEAL,
        HASTE,
        DOUBLE_JUMP,
        DASH,
        UNKILLABLE
    }

    EAbility m_currentAbility;
    List<Ability> abilities = new List<Ability>();

    float cooldownTimer = 0f;
    float cooldownTime;

    [Header("Managers")]
    [SerializeField] AudioManager audioManager;
    [SerializeField] ParticleManager particleManager;
    [SerializeField] HUDManager hudManager;
    PlayerStats playerStats;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        if (playerStats == null)
        {
            Debug.Log(playerStats + " is null");
        }

        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.Log(playerController + " is null");
        }

        // Initialize abilities
        HealAbility healAbility = new HealAbility(this);
        HasteAbility hasteAbility = new HasteAbility(this);
        DoubleJumpAbility doubleJumpAbility = new DoubleJumpAbility(this);
        DashAbility dashAbility = new DashAbility(this);
        UnkillableAbility unkillableAbility = new UnkillableAbility(this);

        // Add abilites to a list
        abilities.Add(null);
        abilities.Add(healAbility);
        abilities.Add(hasteAbility);
        abilities.Add(doubleJumpAbility);
        abilities.Add(dashAbility);
        abilities.Add(unkillableAbility);

        // Set current ability
        ChangeCurrentAbility(EAbility.HEAL);
        hudManager.UpdateAbilityTempText(abilities[(int)m_currentAbility].GetName());

        // Set cooldown to abilities cooldown
        cooldownTime = abilities[(int)m_currentAbility].GetCooldown();
        cooldownTimer = cooldownTime;
        hudManager.UpdateCooldownMaxValue(cooldownTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            hudManager.ToggleAbilityUI();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (cooldownTimer <= 0f)
            {
                ActivateCurrentAbility();
            }
        }

        // Keep the timer ticking
        if (cooldownTimer > 0f)
        {
            hudManager.UpdateCooldownImage(cooldownTimer);
            cooldownTimer -= Time.deltaTime;
        }

        // If the player is grounded the ability will be red to show its unavailable
        if (m_currentAbility == EAbility.DOUBLE_JUMP)
        {
            hudManager.UpdateAbilityAvailable(playerController.IsGrounded());
            if (playerController.doubleJump == false)
            {
                playerController.doubleJump = true;
            }
        }
        else if (playerController.doubleJump == true)
        {
            playerController.doubleJump = false;
            hudManager.UpdateAbilityAvailable(false);
        }
    }

    void ChangeCurrentAbility(EAbility _ability)
    {
        // Change current ability
        m_currentAbility = _ability;

        // Set cooldowns variables to current ability
        cooldownTime = abilities[(int)m_currentAbility].GetCooldown();
        cooldownTimer = cooldownTime;

        // Update UI
        hudManager.UpdateAbilityTempText(abilities[(int)m_currentAbility].GetName());
        hudManager.UpdateCooldownMaxValue(cooldownTime);

        //TODO Make a sound 
    }

    public void ChangeCurrentAbilityByNumber(int _number)
    {
        if(m_currentAbility != (EAbility)_number)
        {
            ChangeCurrentAbility((EAbility)_number);
            Debug.Log("Ability changed: " + ((EAbility)_number));
        }
    }

    void ActivateCurrentAbility()
    {
        cooldownTimer = cooldownTime;
        hudManager.UpdateCooldownImage(cooldownTimer);
        abilities[(int)m_currentAbility].Activate();
    }

    public void SpawnParticles(string _particleEffect)
    {
        particleManager.SpawnParticleEffect(_particleEffect, playerController.transform.position, playerController.transform.rotation);
    }

    public void PlaySound(string _sound)
    {
        audioManager.Play(_sound);
    }

    public void Heal(float _healAmount)
    {
        playerStats.Heal(_healAmount);
    }

    public void IncreaseMoveSpeed(float _moveMult)
    {
        //playerController.IncreaseMoveSpeed(_moveMult);
    }

    public void Move(Vector3 _moveDir)
    {
        //TODO Add movement
    }

    public void DoubleJump()
    {
        playerController.DoubleJump();
    }

    public void Dash()
    {
        playerController.Dash();
    }

    public void UnkillableForATime(float _time)
    {
        StartCoroutine(playerStats.Unkillable(_time));
    }
}
