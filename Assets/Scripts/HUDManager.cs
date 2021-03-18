using System.Collections;
using System.Collections.Generic;
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

    private string ammoString = "Ammo: ";

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

    }

}
