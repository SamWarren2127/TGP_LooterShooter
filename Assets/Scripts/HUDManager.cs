using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HUDManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] Text ammoText;
    [SerializeField] Text reloadingText;
    [SerializeField] Text equippedGunText;
    [SerializeField] Text abilityTempText;

    [SerializeField] Slider healthBar;
    [SerializeField] Slider armorBar;
    [SerializeField] Slider xpBar;
    [SerializeField] Slider cooldownImage;

    [SerializeField] GameObject xpPanel;

    [SerializeField] TextMeshProUGUI levelTextMesh;
    [SerializeField] TextMeshProUGUI levelUpText;

    [SerializeField] Image grenadeIcon1;
    [SerializeField] Image grenadeIcon2;
    [SerializeField] Image grenadeIcon3;

    private float showTime;
    private float xpShowTimer;

    private float animateTime;
    private float animateTimer;

    private string ammoString = "Ammo: ";

    private Image[] m_grenades;

    private void Start()
    {
        UpdateEquippedGunText();
        showTime = 5f;
        xpShowTimer = 0f;
        animateTime = 2.5f;
        m_grenades = new Image[] { grenadeIcon1, grenadeIcon2, grenadeIcon3 };

        // Make sure xp panel isnt showing
        if (xpPanel.activeSelf == true)
        {
            levelUpText.enabled = false;
            HideXP();
        }
    }

    private void Update()
    {
        // The XP panel has a time limit, it resets if more XP is gained while showing (See ShowXP)
        if(xpPanel.activeSelf == true)
        {
            xpShowTimer += Time.deltaTime;
        }
        else
        {
            xpShowTimer = 0;
        }

        if(xpShowTimer >= showTime)
        {
            levelUpText.enabled = false;
            HideXP();
        }

        //TODO Call these functions when a grenade is thrown or picked up
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("UpArrowPressed");
            ShowGrenade();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("DownArrowPressed");
            HideGrenade();
        }
    }

    public void UpdateAmmoText(int ammo, int maxAmmo)
    {
        ammoText.text = ammoString + ammo.ToString() + "/" + maxAmmo.ToString();
    }

    public void UpdateEquippedGunText()
    {
        // Eventually we will pass what gun we are holding from the inventory to update this text
        equippedGunText.text = "MP7";
    }

    public void ShowReload()
    {
        reloadingText.enabled = true;
    }

    public void HideReload()
    {
        reloadingText.enabled = false;
    }

    public void RefillGrenades()
    {
        foreach (Image grenade in m_grenades)
        {
            if (grenade.enabled == false)
            {
                grenade.enabled = true;
            }
        }
    }

    public void ShowGrenade()
    {
        Debug.Log("ShowGrenade");
        foreach (Image grenade in m_grenades)
        {
            if (grenade.enabled == false)
            {
                grenade.enabled = true;
                return;
            }
        }
    }

    public void HideGrenade()
    {
        Debug.Log("HideGrenade");
        for (int i = 2; i > -1; i--)
        {
            if (m_grenades[i].enabled == true)
            {
                m_grenades[i].enabled = false;
                return;
            }
        }
    }

    public void ShowXP()
    {
        if(xpPanel.activeSelf == true)
        {
            xpShowTimer = 0;
        }
        else
        {
            xpPanel.SetActive(true);
        }
    }

    public void HideXP()
    {
        xpPanel.SetActive(false);
    }

    public void UpdateHealthBar(float _value)
    {
        healthBar.value = _value;
    }

    public void UpdateArmorBar(float _value)
    {
        armorBar.value = _value;
    }

    public void UpdateAbility()
    {
        // Show player if the ability is available
    }

    public void UpdateAbilityTempText(string _ability)
    {
        abilityTempText.text = _ability;
    }

    public void UpdateCooldownImage(float _value)
    {
        cooldownImage.value = _value;
    }

    public void UpdateCooldownMaxValue(float _maxValue)
    {
        cooldownImage.maxValue = _maxValue;
    }

    public void UpdateXPBar(float _value)
    {
        animateTimer = 0f;
        while (animateTimer < animateTime)
        {
            animateTimer += Time.deltaTime;
            float lerpValue = animateTimer / animateTime;
            xpBar.value = Mathf.Lerp(xpBar.value, _value, lerpValue);
        }
    }

    public void UpdateLevel(string _level)
    {
        levelTextMesh.text = "Level: " + _level;
    }

    public void ShowLevelUp()
    {
        levelUpText.enabled = true;
    }


}
