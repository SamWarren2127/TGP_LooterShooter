using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonManager : MonoBehaviour
{
    [SerializeField] int columns;
    [SerializeField] int rows;
    public GameObject[,] abilityButtons;
    [SerializeField] GameObject[] buttons = new GameObject[5];
    [SerializeField] AbilityController abilityController;

    // Start is called before the first frame update
    void Start()
    {
        //abilityController = FindObjectOfType<AbilityController>();

        // Initialize array
        abilityButtons = new GameObject[columns, rows];

        for (int i = 0; i < columns; i++)
        {
            if (i == 0)
            {
                continue;
            }

            Button[] temp = new Button[3];
            temp = buttons[i].GetComponentsInChildren<Button>(true);

            for (int j = 0; j < rows; j++)
            {
                abilityButtons[i, j] = temp[j].gameObject;
            }
        }
    }

    public void ToggleEquippedButton(int _abilityButton, bool _toggle)
    {
        abilityButtons[_abilityButton, 0].SetActive(_toggle);
    }

    public void ToggleEquipButton(int _abilityButton, bool _toggle)
    {
        abilityButtons[_abilityButton, 1].SetActive(_toggle);
    }

    public void ToggleUnlockButton(int _abilityButton, bool _toggle)
    {
        abilityButtons[_abilityButton, 2].SetActive(_toggle);
    }

    public int CurrentlyEquippedButton
    {
        get
        {
            for (int i = 1; i < columns; i++)
            {
                if (abilityButtons[i, 0].activeSelf)
                {
                    return i;
                }
            }

            Debug.Log("Couldnt find currently equipped button");
            return 0;
        }
    }

    public void UnlockAbilityButton(int _abilityButton)
    {
        ToggleUnlockButton(_abilityButton, false);
        ToggleEquipButton(_abilityButton, true);
    }

    public void EquipAbilityButton(int _abilityButton)
    {
        // Get the currently equipped button and revert it to the 
        ToggleEquipButton(CurrentlyEquippedButton, true);
        ToggleEquippedButton(CurrentlyEquippedButton, false);

        // Change the currently equipped button 
        ToggleEquipButton(_abilityButton, false);
        ToggleEquippedButton(_abilityButton, true);
    }

    public void ResetAllAbilityButtons()
    {
        // Run through all abilities and enabling the unlock button then disabling all others
        for (int i = 1; i < columns; i++)
        {
            if (!abilityButtons[i, 0].activeSelf)
            {
                abilityButtons[i, 0].SetActive(true);
            }

            if (abilityButtons[i, 1].activeSelf)
            {
                abilityButtons[i, 1].SetActive(false);
            }

            if (abilityButtons[i, 2].activeSelf)
            {
                abilityButtons[i, 2].SetActive(false);
            }
        }
    }


}
