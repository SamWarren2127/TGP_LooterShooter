using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButtonsToggle : MonoBehaviour
{
    public GameObject equippedButton;
    public GameObject equipButton;
    public GameObject unlockButton;

    public void ToggleUnlockButton(bool _toggle)
    {
        unlockButton.SetActive(_toggle);
    }
    public void ToggleEquipButton(bool _toggle)
    {
        unlockButton.SetActive(_toggle);
    }
    public void ToggleEquippedButton(bool _toggle)
    {
        unlockButton.SetActive(_toggle);
    }
}
