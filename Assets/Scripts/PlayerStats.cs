using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IHealable<float>, IDamageable<float>
{
    public HUDManager hudManager;

    public float Health { get; private set; }
    public float Armor { get; private set; }
    [SerializeField]
    private float maxHealth = 1.0f;
    [SerializeField]
    private float maxArmor = 1.0f;
    private bool isUnkillable;
    private bool isHealingOverTime;

    [SerializeField]
    private float lowHealthResistance = 0.5f;
    private float damageResistance = 1.0f;
    private float lastDamagedTimer;

    private bool canDamage;

    private float healingTime;

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
        Armor = maxArmor;
        isUnkillable = false;
        isHealingOverTime = false;
        canDamage = false;
        healingTime = 5f;
        UpdateHealthBar();
        UpdateArmorBar();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Damage(0.1f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Damage(0.5f);
        }

        lastDamagedTimer += Time.deltaTime;

        if(Health < 0.5f && lastDamagedTimer > 5f && !isHealingOverTime)
        {
            StartCoroutine(HealOverTime());
        }
    }

    private bool isAlive()
    {
        if (Health <= 0f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool isLowHealth()
    {
        if (Health <= maxHealth * 0.2f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool hasArmor()
    {
        if (Armor > 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ResetDamageTimer()
    {
        lastDamagedTimer = 0f;
    }

    void UpdateHealthBar()
    {
        hudManager.UpdateHealthBar(Health);
        Debug.Log(Health);
    }

    void UpdateArmorBar()
    {
        hudManager.UpdateArmorBar(Armor);
    }

    public void Heal(float _healAmount)
    {
        if (_healAmount < 0)
        {
            Debug.Log("Cannot heal a negative number.");
        }
        else if (Health + _healAmount > maxHealth)
        {
            Health = maxHealth;
            damageResistance = 1.0f;
            hudManager.HideBloodPanel();
        }
        else
        {
            Health += _healAmount;

            if (Health > 0.2f)
            {
                damageResistance = 1.0f;
                hudManager.HideBloodPanel();
            }
        }

        UpdateHealthBar();
    }

    private IEnumerator HealOverTime()
    {
        float healAmount = Health - (maxHealth * 0.5f);
        healAmount = Mathf.Abs(healAmount);
        float healperTick = healAmount / (healingTime / 0.2f);
        isHealingOverTime = true;

        for(float i = healingTime; i >= 0f; i -= 0.2f)
        {
            if(lastDamagedTimer < 5f)
            {
                isHealingOverTime = false;
                yield return null;
            }

            Heal(healperTick);
            yield return new WaitForSeconds(0.2f); 
        }

        isHealingOverTime = false;
    }

    public void Damage(float _damageAmount)
    {
        if (_damageAmount > 0f && _damageAmount < 1.0f)
        {
            if (hasArmor())
            {
                // Armor takes the first damage
                Armor -= _damageAmount;

                // If the armor is depleted then the leftover damage should be done to the health
                if (!hasArmor())
                {
                    // Set damage amount to the leftover damage and reset armor to 0
                    _damageAmount = Armor;
                    Armor = 0f;
                    if(_damageAmount < 0f)
                    {
                        Mathf.Abs(_damageAmount);
                    }

                    // If already low health then make sure damage resistance is right and deal the reduced damaged
                    if (isLowHealth())
                    {
                        if (damageResistance != lowHealthResistance)
                        {
                            damageResistance = lowHealthResistance;
                        }

                        // Deal the leftover damage to health
                        Health -= _damageAmount * damageResistance;
                        ResetDamageTimer();

                        if (!isAlive())
                        {
                            //TODO Handle Dying
                            Die();
                        }

                        UpdateHealthBar();
                        return;
                    }

                    // If the damage will take the health under 1/5 health then deal damage as usual down to 20 then reduce the resistance for the remainder
                    if (Health - _damageAmount <= maxHealth * 0.2f)
                    {
                        // Get the difference between 1/5 and damage amount
                        float temp = maxHealth * 0.2f - _damageAmount;

                        // Make the number positive
                        Mathf.Abs(temp);

                        // Deal damage down to 1/5 health
                        Health -= temp * damageResistance;
                        hudManager.ShowBloodPanel();
                        FindObjectOfType<AudioManager>().Play("LowHealth");

                        // Minus the damage already dealt from the remainding damage
                        _damageAmount -= temp;

                        // Set damage resistance to lowHealthResistance and deal the remainding damage
                        damageResistance = lowHealthResistance;
                        Health -= _damageAmount * damageResistance;
                        ResetDamageTimer();

                        if (!isAlive())
                        {
                            //TODO Handle Dying
                            Die();
                        }
                    }
                    else
                    {
                        Health += _damageAmount * damageResistance;
                        ResetDamageTimer();
                    }
                    UpdateHealthBar();
                }
                UpdateArmorBar();
            }
            else
            {
                // If already low health then make sure damage resistance is right and deal the reduced damaged
                if (isLowHealth())
                {
                    if (damageResistance != lowHealthResistance)
                    {
                        damageResistance = lowHealthResistance;
                    }

                    // Deal the leftover damage to health
                    Health -= _damageAmount * damageResistance;
                    ResetDamageTimer();

                    if(isUnkillable && !isAlive())
                    {
                        Health = 0.01f;
                        UpdateHealthBar();
                        return;
                    }

                    if (!isAlive())
                    {
                        //TODO Handle Dying
                        Die();
                    }

                    UpdateHealthBar();
                    return;
                }

                // If the damage will take the health under 1/5 health then deal damage as usual down to 20 then reduce the resistance for the remainder
                if (Health - _damageAmount <= maxHealth * 0.2f)
                {
                    // Get the difference between 1/5 and damage amount
                    float temp = maxHealth * 0.2f - _damageAmount;

                    // Make the number positive
                    if(temp < 0f)
                    {
                        Mathf.Abs(temp);
                    }

                    // Deal damage down to 1/5 health
                    Health -= temp * damageResistance;
                    hudManager.ShowBloodPanel();

                    // Minus the damage already dealt from the remainding damage
                    _damageAmount -= temp;

                    // Set damage resistance to lowHealthResistance and deal the remainding damage
                    damageResistance = lowHealthResistance;
                    Health -= _damageAmount * damageResistance;
                    ResetDamageTimer();


                    if (isUnkillable && !isAlive())
                    {
                        Health = 0.01f;
                        UpdateHealthBar();
                        return;
                    }

                    if (!isAlive())
                    {
                        //TODO Handle Dying
                        Die();
                    }
                }
                else
                {
                    // Else deal _damageAmount
                    Health -= _damageAmount * damageResistance;
                    ResetDamageTimer();
                }
                UpdateHealthBar();
            }
        }
    }

    public IEnumerator Unkillable(float _time)
    {
        isUnkillable = true;
        hudManager.ToggleUnkillablePanel(isUnkillable);
        yield return new WaitForSeconds(_time);
        isUnkillable = false;
        hudManager.ToggleUnkillablePanel(isUnkillable);
    }

    private void Die()
    {
        hudManager.HideHUD();
        hudManager.ShowDeathMenu();
    }
}
