using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission1 : MonoBehaviour
{
    public Text missionText;
    public int enemiesKilled;
    public int missionGoal;

    public GameObject missionUI;

    int[] enemies = new int[7] { 1, 2, 3, 4, 5, 6, 7 };

    // Start is called before the first frame update
    void Start()
    {
        NewMission();
        Debug.Log("Started");
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesKilled >= missionGoal)
        {
            missionText.text = "Mission Completed";
            //add xp
        }
    }

    public void NewMission()
    {
        Debug.Log("Mission started");
        missionUI.SetActive(true);
        int missionTarget = enemies[Random.Range(0, enemies.Length)];
        missionText.text = "Kill enemies (" + enemiesKilled + "/" + missionTarget + ")";
        missionGoal = missionTarget;
    }

    IEnumerator MissionCompleteUI()
    {
        yield return new WaitForSeconds(4f);
        missionUI.SetActive(false);

        yield return new WaitForSeconds(15f);
        NewMission();
    }

  
}
