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
    [SerializeField] Slider healthBar;
    [SerializeField] Slider armorBar;
    [SerializeField] GameObject xpPanel;
    [SerializeField] Slider xpBar;
    [SerializeField] TextMeshProUGUI levelTextMesh;

    private float showTime;
    private float xpShowTimer;

    private float animateTime;
    private float animateTimer;

    private string ammoString = "Ammo: ";

    private void Start()
    {
        UpdateEquippedGunText();
        showTime = 5f;
        xpShowTimer = 0f;
        animateTime = 2.5f;

        // Make sure xp panel isnt showing
        if(xpPanel.activeSelf == true)
        {
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
            HideXP();
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
        reloadingText.enabled = false;
    }

    public void HideReload()
    {
        reloadingText.enabled = true;
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
}
