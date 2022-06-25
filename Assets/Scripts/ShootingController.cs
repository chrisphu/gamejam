using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject bullet;
    GameObject bullets;

    float shootingSpeed = 20.0f;
    float initialOffset = 0.75f;

    AmmoTracker ammoTracker;

    float reloading = 0.0f;
    float reloadTime = 2.0f;

    float shooting = 1000000.0f;
    float shootingTime = 1.0f / 20.0f;

    void Awake ()
    {
        #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = 45;
        #endif
    }

    void Start()
    {
        ammoTracker = transform.GetComponent<AmmoTracker>();
        bullets = GameObject.FindGameObjectWithTag("Bullets");
    }

    void Update()
    {
        if (ammoTracker.ammo <= 0)
        {
            if (reloading > reloadTime)
            {
                ammoTracker.ammo = ammoTracker.maxAmmo;
                shooting = 1000000.0f;
                reloading = 0.0f;
            }
            else
            {
                reloading += Time.deltaTime;
            }
        }
        else // (Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButton(0))
            {
                if (shooting > shootingTime)
                {
                    shooting = 0.0f;
                    ammoTracker.ammo -= 1;

                    // calculate bullet angle
                    Vector3 screenCenter = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
                    Vector2 shootingDirection = (Input.mousePosition - screenCenter).normalized;
                    float shootingAngle = Vector2.SignedAngle(Vector2.right, shootingDirection);

                    // spawn new bullet
                    GameObject newBullet = Instantiate(bullet, transform.position + new Vector3(shootingDirection.x, shootingDirection.y, 0.0f).normalized * initialOffset, Quaternion.identity, bullets.transform);
                    newBullet.transform.eulerAngles = new Vector3(0.0f, 0.0f, shootingAngle);
                    newBullet.GetComponent<BulletController>().speed = shootingSpeed;
                }
                else
                {
                    shooting += Time.deltaTime;
                }
            }
        }
    }    
}
