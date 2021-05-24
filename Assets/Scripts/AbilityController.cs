using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    enum EAbility
    {
        NONE,
        HEAL,
        HASTE
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

        // Add abilites to a list
        abilities.Add(null);
        abilities.Add(healAbility);
        abilities.Add(hasteAbility);

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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (cooldownTimer <= 0f)
            {
                ActivateCurrentAbility();
            }
        }

        if (cooldownTimer > 0f)
        {
            hudManager.UpdateCooldownImage(cooldownTimer);
            cooldownTimer -= Time.deltaTime;
        }
    }

    void ChangeCurrentAbility(EAbility _ability)
    {
        m_currentAbility = _ability;
        cooldownTime = abilities[(int)m_currentAbility].GetCooldown();
        cooldownTimer = cooldownTime;
        //TODO Handle udating the UI
        hudManager.UpdateAbilityTempText(abilities[(int)m_currentAbility].GetName());
        hudManager.UpdateCooldownMaxValue(cooldownTime);
        //Maybe make a sound
    }

    void ActivateCurrentAbility()
    {
        abilities[(int)m_currentAbility].Activate();
        cooldownTimer = cooldownTime;
        //TODO Update UI
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
        playerController.IncreaseMoveSpeed(_moveMult);
    }

    public void Move(Vector3 _moveDir)
    {
        //TODO Add movement
    }
}
