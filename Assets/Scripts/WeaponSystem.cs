using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public Camera cam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask layerMask;
    public GameObject spawnCasing, casing, muzzleFlash, bulletHoleGraphic;

    public int damage, magazineSize;
    public float rateOfFire, recoilX, recoilY, range, reloadTime;
    int remainingBullets, totalAmmo;
    bool shooting, canShoot, reloading, fullAutoFire, burstFire, semiAutoFire, safety, jammed, isClearingJam, burstWeapon;

    [SerializeField]
    HUDManager hudManager;

    // Start is called before the first frame update
    void Start()
    {
        remainingBullets = magazineSize;
        canShoot = true;
        reloading = false;
        shooting = false;
        fullAutoFire = true;
        jammed = false;

        hudManager.UpdateAmmoText(remainingBullets, magazineSize);
    }

    // Update is called once per frame
    void Update()
    {
        // safety
        if (Input.GetKey(KeyCode.Tilde))
        {
            canShoot = !canShoot;
            safety = !safety;
        }

        // Change fire mode
        if (Input.GetKey(KeyCode.F))
        {
            ChangeFireRate();
        }

        // Leaning

        // Reload
        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            Reload();
        }

        // Shoot
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (canShoot && !shooting && !reloading && remainingBullets > 0 && !jammed && fullAutoFire)
            {
                // full auto if possible 
                Shoot();
            }
            else 
            {
                // Play empty click sound
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canShoot && !shooting && !reloading && remainingBullets > 0 && !jammed && fullAutoFire)
            {
                // burst or single where applicable 
                Shoot();
            }
            else
            {
                // Play empty click sound
            }
        }

        // Clear Jam

        if (Input.GetKeyDown(KeyCode.H) && !isClearingJam)
        {
            StartCoroutine(ClearWeaponJam());
        }

    }

    private void Shoot()
    {
        print("shoot");
        canShoot = false;

        //recoil
        float x = Random.Range(-recoilX, recoilX);
        float y = Random.Range(-recoilY, recoilY);

        //Calculate Direction with recoil
        Vector3 direction = cam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(cam.transform.position, direction, out rayHit, range, layerMask))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                // Do damage
            }
        }

        // Sound and nice looking stuff
        //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        //Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        Instantiate(casing, spawnCasing.transform.position + spawnCasing.transform.right, spawnCasing.transform.rotation);

        remainingBullets--;

        // Updating the HUD
        hudManager.UpdateAmmoText(remainingBullets, magazineSize);

        if (fullAutoFire)
        {
            Invoke("ResetShot", rateOfFire);
        }
        
    }
    private void ResetShot()
    {
        canShoot = true;
    }

    private void ChangeFireRate()
    {
        if (!burstWeapon)
        { 
            if (fullAutoFire)
            {
                fullAutoFire = false;
                semiAutoFire = true;
            }
           else  if (semiAutoFire)
           {
                semiAutoFire = false;
                fullAutoFire = true;
           }
        }
        else
        {
            if (fullAutoFire)
            {
                fullAutoFire = false;
                burstFire = true;
            }
            else if (burstFire)
            {
                burstFire = false;
                semiAutoFire = true;
            }
            else if (semiAutoFire)
            {
                semiAutoFire = false;
                fullAutoFire = true;
            }
        }
    }

    private void WeaponJam()
    {
        jammed = true;
    }

    IEnumerator ClearWeaponJam()
    {
        isClearingJam = true;
        print("clearing weapon jam");
        // do animation 

        // Maybe add a skill based game eg press E when the progress par reaches X position 

        yield return new WaitForSeconds(10);
        print("weapon jammed cleared");
    }

    private void Reload()
    {
        reloading = true;
        hudManager.ShowReload();
        if (totalAmmo >= magazineSize)
        {
            totalAmmo -= magazineSize;
        }
        else if (magazineSize > totalAmmo && totalAmmo > 0)
        {
            remainingBullets = totalAmmo;
        }
        else
        {
            reloading = false;
            return;
        }
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        remainingBullets = magazineSize;
        reloading = false;

        hudManager.HideReload();
        hudManager.UpdateAmmoText(remainingBullets, magazineSize);
    }
}
