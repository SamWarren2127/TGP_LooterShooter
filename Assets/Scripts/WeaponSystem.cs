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

    public int damage, magazineSize, totalAmmo, bulletsPerBurst;
    public float rateOfFire, recoilX, recoilY, range, reloadTime;
    int remainingBullets, currentBurst;
    bool shooting, canShoot, reloading, jammed, isClearingJam, changingFireRate;
    public bool fullAutoFire, semiAutoFire, burstFire, safety, burstWeapon, fullAutoWeapon; // todo add a full auto weapon bool

    [SerializeField] HUDManager hudManager;

    // Start is called before the first frame update
    void Start()
    {
        remainingBullets = magazineSize;
        currentBurst = bulletsPerBurst;
        canShoot = true;
        reloading = false;
        shooting = false;
        jammed = false;
        changingFireRate = false;

        //hudManager.UpdateAmmoText(remainingBullets, magazineSize);
    }

    // Update is called once per frame
    void Update()
    {
        // safety
        if (Input.GetKeyDown(KeyCode.H))
        {
            print("safety toggled");
            canShoot = !canShoot;
            safety = !safety;
        }

        // Change fire mode
        if (Input.GetKeyDown(KeyCode.F) && !changingFireRate)
        {
            print("F key pressed");
            changingFireRate = true;
            ChangeFireRate();

        }

        // Reload
        if (Input.GetKeyDown(KeyCode.R) && !reloading)
        {
            StartCoroutine(Reload());
        }

        // Shoot
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (canShoot && !shooting && fullAutoFire && !reloading && remainingBullets > 0 && !jammed && hudManager.abilityUI.activeSelf == false)
            {
                StartCoroutine(Shoot());
            }
            else
            {
                // Play empty click sound
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canShoot && !shooting && !reloading && remainingBullets > 0 && !jammed && hudManager.abilityUI.activeSelf == false)
            {
                // burst or single where applicable 
                StartCoroutine(Shoot());
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

    IEnumerator Shoot()
    {
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
                Debug.Log("Hit enemy");
            }

            Enemy health = rayHit.collider.GetComponent<Enemy>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        // Sound and nice looking stuff
        //Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        //Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        Instantiate(casing, spawnCasing.transform.position + spawnCasing.transform.right, spawnCasing.transform.rotation);

        remainingBullets--;
        FindObjectOfType<AudioManager>().Play("gunshot");

        // Updating the HUD
        hudManager.UpdateAmmoText(remainingBullets, magazineSize);

        //Reseting can shoot
        yield return new WaitForSeconds(rateOfFire);
        canShoot = true;

        if (burstFire && remainingBullets > 0 && currentBurst > 0)
        {
            currentBurst -= 1;
            if (currentBurst <= 0)
            {
                currentBurst = bulletsPerBurst;
            }
            else
            {
                print("burst still firing");
                StartCoroutine(Shoot());
            }
        }
    }

    private void ChangeFireRate()
    {
        if (fullAutoWeapon)
        {
            if (fullAutoFire)
            {
                print("switching to semi");
                fullAutoFire = false;
                semiAutoFire = true;
            }
            else if (semiAutoFire)
            {
                print("switching to full auto");
                semiAutoFire = false;
                fullAutoFire = true;
            }
        }
        else if (burstWeapon && fullAutoWeapon)
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
        else if (burstWeapon)
        {
            if (burstFire)
            {
                burstFire = false;
                semiAutoFire = true;
            }
            else if (semiAutoFire)
            {
                semiAutoFire = false;
                burstFire = true;
            }
        }

        print("setting to false");
        changingFireRate = false;
    }

    private void WeaponJam()
    {
        jammed = true;
    }

    IEnumerator ClearWeaponJam()
    {
        isClearingJam = true;
        // do animation 

        // Maybe add a skill based game eg press E when the progress par reaches X position 

        yield return new WaitForSeconds(10);
    }

    IEnumerator Reload()
    {
        reloading = true;
        hudManager.ShowReload();
        if (totalAmmo >= magazineSize)
        {
            print(1);
            totalAmmo -= magazineSize;
        }
        else if (magazineSize > totalAmmo && totalAmmo > 0)
        {
            print(2);

            remainingBullets += totalAmmo;
            totalAmmo = totalAmmo - remainingBullets;
            if (remainingBullets > magazineSize)
            {
                totalAmmo = remainingBullets - magazineSize;
                remainingBullets = magazineSize;
            }
            if (totalAmmo < 0)
            {
                totalAmmo = 0;
            }

        }
        else
        {
            print(3);
            reloading = false;
            yield return null;
        }
        yield return new WaitForSeconds(reloadTime);

        //remainingBullets = magazineSize;
        reloading = false;

        hudManager.HideReload();
        hudManager.UpdateAmmoText(remainingBullets, magazineSize);
    }

    void Damage(Transform enemy)
    {
        Enemy e = enemy.GetComponent<Enemy>();

        //if(e != null)
        //{
        //    e.TakeDamage(damage);
        //}

        IDamageable<float> eInterface = enemy.gameObject.GetComponent<IDamageable<float>>();

        if (eInterface != null)
        {
            eInterface.Damage(damage);
        }
    }
}