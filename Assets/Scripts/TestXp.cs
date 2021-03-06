﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestXp : MonoBehaviour
{
    [SerializeField] HUDManager hudManager;
    Skills skills;

    public float Level;
    private float currentXp;
    private float xpCap;

    // Start is called before the first frame update
    void Start()
    {
        Level = 1f;
        currentXp = 0f;
        xpCap = 50f;
        skills = GetComponent<Skills>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            AddXp(10f);
            UpdateXPBar();
        }

        if(currentXp >= xpCap)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        Level++;
        currentXp = 0;
        xpCap += 50f;
        skills.AddSkillPoints(1);
        hudManager.UpdateSkillPoints(skills.skillpoints);

        // HUD Manager will hide XP after a certain amount of seconds
        hudManager.ShowXP();
        hudManager.ShowLevelUp();
        UpdateXPBar();
        UpdateLevelText();

        // Handle other things like sound, show level up UI
    }

    void AddXp(float _value)
    {
        hudManager.ShowXP();
        currentXp += _value;
    }

    void UpdateXPBar()
    {
        hudManager.UpdateXPBar(currentXp / xpCap);
    }

    void UpdateLevelText()
    {
        hudManager.UpdateLevel(Level.ToString());
    }
}
