using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    int level = 1;
    float currentEXP = 0f;
    float maxEXP = 1f;
    float expMultipler = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void increaseEXP(float exp)
    {
        currentEXP = expMultipler * exp;
        if (currentEXP >= maxEXP)
        {
            increaseLevel();
        }
    }

    void increaseLevel()
    {
        currentEXP -= maxEXP;
        maxEXP += 1;
        level += 1;
    }

    void increaseEXPMultiplerForSetTime(int time, int multipler)
    {
        StartCoroutine(TimeEXP(time, multipler));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TimeEXP(int time, int multipler)
    {
        float curMultiper = expMultipler;
        expMultipler = multipler;
        yield return new WaitForSeconds(time);
        expMultipler = 1;
    }
}
