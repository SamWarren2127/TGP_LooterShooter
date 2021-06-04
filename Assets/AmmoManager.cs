using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    public int arMaxAmmo;
    public int smgMaxAmmo;
    static int curARAmmo;
    static int curSMGAmmo;
    IGunDisplayable gunDisplayable;

    // Start is called before the first frame update
    void Start()
    {
        curARAmmo = arMaxAmmo;
        curSMGAmmo = smgMaxAmmo;
    }

    public void IncreaseCurrentAmmo(int amount)
    {
        int type = (int)gunDisplayable.GetGunType();
        // 1 mp7 3 ump 2ak
        if (type == 1 || type == 3)
        {
            arMaxAmmo += amount;
            if (curARAmmo > arMaxAmmo)
            {
                curARAmmo = arMaxAmmo;
            }
        }
        else if (type == 2)
        {
            smgMaxAmmo += amount;
            if (curARAmmo > arMaxAmmo)
            {
                curSMGAmmo = smgMaxAmmo;
            }
        }
    }

    public void DecreaseCurrentAmmo(int amount)
    {
        int type = (int)gunDisplayable.GetGunType();
        // 1 mp7 3 ump 2ak
        if (type == 1 || type == 3)
        {
            smgMaxAmmo -= amount;
            if (curARAmmo > arMaxAmmo)
            {
                curSMGAmmo = smgMaxAmmo;
            }
        }
        else if (type == 2)
        {
            arMaxAmmo -= amount;
            if (curARAmmo > arMaxAmmo)
            {
                curARAmmo = arMaxAmmo;
            }
        }
    }

    public int GetCurrentMaxAmmo()
    {
        int type = (int)gunDisplayable.GetGunType();
        // 1 mp7 3 ump 2ak
        if (type == 1 || type == 3)
        {
            return smgMaxAmmo;
        }
        else
        {
            return arMaxAmmo;
        }       
    }

    public void UpdateCurrentAmmo(int ammo)
    {
        int type = (int)gunDisplayable.GetGunType();
        // 1 mp7 3 ump 2ak
        if (type == 1 || type == 3)
        {
            if (curARAmmo > arMaxAmmo)
            {
                curSMGAmmo = ammo;
            }
        }
        else if (type == 2)
        {
            if (curARAmmo > arMaxAmmo)
            {
                arMaxAmmo = ammo;
            }
        }
    }
}
