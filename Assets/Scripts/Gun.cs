using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera cam;
    public float force = 1f;
    public GameObject[] bulletHolePrefabs;

    //FireRate
    public bool canShoot;
    public float fireRateTimer = 0f;
    public float fireRateTimerMax = 5f;

    //Reload
    public int currentAmmo;
    public int ammoMax = 10;
    public bool reloading = false;
    public float reloadTimer;
    public float reloadTimerMax = 10f;
    private bool pressedRecently;


        void Start()
        {
          currentAmmo = ammoMax;
        }


        void Update()
        {
            
            if (currentAmmo <= 0f)
            {
                reloading = true;
                reloadTimer += Time.deltaTime;
                
                if (reloadTimer > reloadTimerMax)
                {
                    currentAmmo = ammoMax;
                    reloading = false;
                    reloadTimer = 0f;
                    pressedRecently = false;
                }
        
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                reloading = true;
                pressedRecently = true;
            }
            
            
            if (canShoot == false)
            {
               fireRateTimer += Time.deltaTime; 
               
               if (fireRateTimer > fireRateTimerMax)
               {
                    canShoot = true;
                    fireRateTimer = 0; 
               }
            }

            

            if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot == true )
            {
               Shoot();
            }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }

    }


    public void Fire()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && Time.timeScale == 1)
        {

            GameObject choosenBulletHole = bulletHolePrefabs[Random.Range(0, bulletHolePrefabs.Length)];

            if (hit.rigidbody && Time.timeScale == 0)
            {
                var direction = new Vector3(hit.transform.position.x - transform.position.x, hit.transform.position.y - transform.position.y, hit.transform.position.z - transform.position.z);
                hit.rigidbody.AddForceAtPosition(force * Vector3.Normalize(direction), hit.point);

                var tempBullet = Instantiate(choosenBulletHole, hit.point, Quaternion.LookRotation(hit.normal));
                tempBullet.transform.parent = hit.transform;
            }
            else
            {
                Instantiate(choosenBulletHole, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
        else
            print("I'm looking at nothing!");

    }


    void Shoot()
        {  
            currentAmmo--;
            canShoot = false;
        }

}
