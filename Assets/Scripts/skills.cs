using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skills : MonoBehaviour
{
    public int skillpoints;
    public int skillpointsSpent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddSkillPoints(int amount)
    {
        skillpoints += amount;
    }

    public void SpendSkillPoints(int amount)
    {
        if ( skillpoints >= amount)
        {
            skillpoints -= 1;
            skillpointsSpent += 1;
        }
    }

    public void resetSkillPoints()
    {
        skillpoints = skillpointsSpent;
        skillpointsSpent = 0;
    }
}
