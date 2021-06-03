using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public GameObject crouchPanel;
    public GameObject abilityPanel;
    public GameObject jumpPanel;
    public GameObject doubleJumpPanel;
    public GameObject objectivePanel;

    public void ShowCrouch()
    {
        crouchPanel.SetActive(true);
    }

    public void ShowAbility()
    {
        abilityPanel.SetActive(true);
    }

    public void ShowJump()
    {
        jumpPanel.SetActive(true);
    }

    public void ShowDoubleJump()
    {
        doubleJumpPanel.SetActive(true);
    }

    public void ShowObjective()
    {
        objectivePanel.SetActive(true);
    }
}
