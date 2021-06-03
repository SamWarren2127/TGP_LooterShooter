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
    [SerializeField] TextMeshProUGUI skillPointsText;
    [SerializeField] TextMeshProUGUI[] statistics = new TextMeshProUGUI[4];
    [SerializeField] TextMeshProUGUI[] costAndCooldownText = new TextMeshProUGUI[6];

    [SerializeField] Slider healthBar;
    [SerializeField] Slider armorBar;
    [SerializeField] Slider xpBar;
    [SerializeField] Slider cooldownImage;

    [SerializeField] GameObject xpPanel;
    public GameObject abilityUI;
    [SerializeField] GameObject bloodPanel;
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject deathMenu;
    [SerializeField] GameObject unkillablePanel;

    [SerializeField] TextMeshProUGUI levelTextMesh;
    [SerializeField] TextMeshProUGUI levelUpText;
    [SerializeField] TextMeshProUGUI levelNotHighEnoughText;

    //TODO This only needs to be an array
    private Image[] m_grenades;
    [SerializeField] Image grenadeIcon1;
    [SerializeField] Image grenadeIcon2;
    [SerializeField] Image grenadeIcon3;
    [SerializeField] Image abilityBackground;

    private float showTime;
    private float xpShowTimer;
    private float levelNotHighEnoughShowTimer;
    private float levelNotHighEnoughShowTime;
    private float animateTime;
    private float animateTimer;

    private string ammoString = "Ammo: ";

    private Color originalColor;

    private void Start()
    {
        UpdateEquippedGunText("MP7");
        showTime = 5f;
        xpShowTimer = 0f;
        levelNotHighEnoughShowTime = 4f;
        levelNotHighEnoughShowTimer = 0f;
        animateTime = 2.5f;
        m_grenades = new Image[] { grenadeIcon1, grenadeIcon2, grenadeIcon3 };
        originalColor = abilityBackground.color;

        // Make sure xp panel isnt showing
        if (xpPanel.activeSelf)
        {
            levelUpText.enabled = false;
            HideXP();
        }

        if(abilityUI.activeSelf)
        {
            abilityUI.SetActive(!abilityUI.activeSelf);
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

        /*if(levelNotHighEnoughText.enabled == true)
        {
            levelNotHighEnoughShowTimer += Time.deltaTime;
        }
        else
        {
            levelNotHighEnoughShowTimer = 0;
        }*/

        if(levelNotHighEnoughShowTimer >= levelNotHighEnoughShowTime)
        {
            levelNotHighEnoughText.enabled = false;
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

    public void ToggleLevelNotHighEnoughText(bool _toggle)
    {
        levelNotHighEnoughText.enabled = _toggle;
    }

    public void ToggleUnkillablePanel(bool _toggle)
    {
        unkillablePanel.SetActive(_toggle);
    }

    public void ShowBloodPanel()
    {
        bloodPanel.SetActive(true);
    }

    public void HideBloodPanel()
    {
        bloodPanel.SetActive(false);
    }

    public void ShowHUD()
    {
        HUD.SetActive(true);
    }

    public void HideHUD()
    {
        HUD.SetActive(false);
    }

    public void ShowDeathMenu()
    {
        deathMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public void HideDeathMenu()
    {
        deathMenu.SetActive(false);
    }

    public void UpdateAmmoText(int ammo, int maxAmmo)
    {
        ammoText.text = ammoString + ammo.ToString() + "/" + maxAmmo.ToString();
    }

    public void UpdateEquippedGunText (string _gunName)
    {
        equippedGunText.text = _gunName;
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

    public void ToggleAbilityUI()
    {
        abilityUI.SetActive(!abilityUI.activeSelf);

        if (abilityUI.activeSelf)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void UpdateHealthBar(float _value)
    {
        healthBar.value = _value;
    }

    public void UpdateArmorBar(float _value)
    {
        armorBar.value = _value;
    }

    public void UpdateAbilityAvailable(bool _available)
    {
        // Show player if the ability is available
        if(!_available)
        {
            if(abilityBackground.color != originalColor)
            {
                abilityBackground.color = originalColor;
            }
            else
            {
                return;
            }
        }
        else
        {
            if(abilityBackground.color != Color.red)
            {
                abilityBackground.color = Color.red;
            }
        }
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
        FindObjectOfType<AudioManager>().Play("LevelUp");
    }

    public void UpdateSkillPoints(int _skillPoints)
    {
        skillPointsText.text = "Skill Points: " + _skillPoints;
    }

    public void UpdateCostAndCooldown(int _ability, int _cost, float _cooldown)
    {
        costAndCooldownText[_ability].text = "Cost: " + _cost + "\n" + "Cooldown: " + _cooldown;
    }
}
