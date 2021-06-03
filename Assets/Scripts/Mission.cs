using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    [SerializeField] TestXp testXp;

    public Text missionText;
    public int enemiesKilled;
    public int missionGoal;
    //public bool missionComplete = false;

    public GameObject missionUI;

    int[] enemies = new int[3] { 3, 4, 5 };

    // Start is called before the first frame update
    void Start()
    {
        NewMission();
        Debug.Log("Started");
    }

    // Update is called once per frame
    void Update()
    {
        if(enemiesKilled > 0 && enemiesKilled < missionGoal)
        {
            missionText.text = "Kill enemies (" + enemiesKilled + "/" + missionGoal + ")";
        }

        if(enemiesKilled >= missionGoal)
        {
            missionText.text = "Mission Completed";
            StartCoroutine(MissionCompleteUI());
        }
    }

    public void NewMission()
    {
        Debug.Log("Mission started");
        missionUI.SetActive(true);
        int missionTarget = enemies[Random.Range(0, enemies.Length)];
        missionText.text = "Kill enemies (" + enemiesKilled + "/" + missionTarget + ")";
        missionGoal = missionTarget;
        //missionComplete = false;

    }

    IEnumerator MissionCompleteUI()
    {
        testXp.LevelUp();
        enemiesKilled = 0;
        yield return new WaitForSeconds(35f);
        NewMission();
    }


  
}
