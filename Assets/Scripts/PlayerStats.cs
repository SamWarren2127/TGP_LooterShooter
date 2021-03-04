using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Image a;

    public float health = 1.0f;
    public float armor = 1.0f;
    public float maxHealth = 1.0f;
    public float maxArmor = 1.0f;
    public bool canDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("damagePlayer", 10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        //a.fillAmount = 0;
        a.fillAmount = Mathf.Lerp(a.fillAmount, (health / maxHealth), Time.deltaTime);
        Color healthColor = Color.Lerp(Color.red, Color.white, (health / maxHealth));
        a.color = healthColor;
    }

    void damagePlayer()
    {
        if (health > 0)
        {
            health -= 0.2f;
        }
        else
        {
            print("dead");
        }
        
    }
}
