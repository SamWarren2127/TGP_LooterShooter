using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    public int skillpoints;
    private int skillpointsSpent;

    public void AddSkillPoints(int amount)
    {
        skillpoints += amount;
    }

    public void SpendSkillPoints(int amount)
    {
        if ( skillpoints >= amount)
        {
            skillpoints -= amount;
            skillpointsSpent += amount;
        }
    }

    public void resetSkillPoints()
    {
        skillpoints = skillpointsSpent;
        skillpointsSpent = 0;
    }
}
