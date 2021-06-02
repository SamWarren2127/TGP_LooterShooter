using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    public GameObject objectToThrow;
    public Transform armLocation;
    public float ammo;
    public float maxAmmo;
    public static bool throwing;


    // Start is called before the first frame update
    void Start()
    {
        ammo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            throwing = true;
            objectToThrow.GetComponent<GrenadeEffects>().throwEffect(armLocation);
            throwing = false;
            ammo -= 1;
        }
    }

    public void grenadePickedUP(GameObject grenade)
    {
        if (objectToThrow != grenade)
        {
            objectToThrow = grenade;
        }
        increaseGrenadeAmmo(1);
    }

    public void increaseGrenadeAmmo(int amount)
    {
        ammo += amount;
    }
}
