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

    /// Abilities
    /// 0 - None
    /// 1 - Heal
    /// 2 - Haste
    /// 3 - Double Jump
    /// 4 - Dash
    /// 5 - Unkillable

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
    AbilityButtonManager abilityButtonManager;
    Skills skills;
    TestXp testXp;

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

        abilityButtonManager = FindObjectOfType<AbilityButtonManager>();
        if (abilityButtonManager == null)
        {
            Debug.Log(abilityButtonManager + " is null");
        }

        skills = GetComponent<Skills>();
        if(skills == null)
        {
            Debug.Log(skills + " is null");
        }

        testXp = GetComponent<TestXp>();
        if (testXp == null)
        {
            Debug.Log(testXp + " is null");
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

        hudManager.UpdateSkillPoints(skills.skillpoints);

        for (int i = 1; i < abilities.Count; i++)
        {
            hudManager.UpdateCostAndCooldown(i, abilities[i].GetCost(), abilities[i].GetCooldown());
        }

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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            hudManager.ToggleAbilityUI();
        }

        if (m_currentAbility == EAbility.DOUBLE_JUMP)
        {
            if (Input.GetKeyDown(KeyCode.Space) && cooldownTimer <= 0f && !playerController.IsGrounded())
            {
                ActivateCurrentAbility();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) && cooldownTimer <= 0f && abilities[(int)m_currentAbility].IsUnlocked())
        {
            ActivateCurrentAbility();
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

        abilityButtonManager.EquipAbilityButton((int)m_currentAbility);

        //TODO Make a sound 
    }

    public void ChangeCurrentAbilityByNumber(int _number)
    {
        if (m_currentAbility != (EAbility)_number)
        {
            ChangeCurrentAbility((EAbility)_number);
            Debug.Log("Ability changed: " + ((EAbility)_number));
        }
    }

    public void UnlockAbilityByNumber(int _number)
    {
        if (testXp.Level < abilities[_number].GetLevelRequirement())
        {
            Debug.Log("Level is not high enough");
            hudManager.ToggleLevelNotHighEnoughText(true);
            return;
        }

        if (skills.skillpoints <= 0 && skills.skillpoints < abilities[_number].GetCost())
        {
            Debug.Log("Dont have enough skill points");
            return;
        }

        abilities[_number].Unlock();
        skills.SpendSkillPoints(abilities[_number].GetCost());
        hudManager.UpdateSkillPoints(skills.skillpoints);
        abilityButtonManager.UnlockAbilityButton(_number);
        Debug.Log(abilities[_number] + " unlocked");
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
        StartCoroutine(playerController.IncreaseMoveMultCoroutine(_moveMult));
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
