using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    public GameObject bullet;
    GameObject bullets;

    float shootingSpeed = 20.0f;

    void Awake ()
    {
        #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = 45;
        #endif
    }

    void Start()
    {
        bullets = GameObject.FindGameObjectWithTag("Bullets");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenCenter = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
            Vector2 shootingDirection = (Input.mousePosition - screenCenter).normalized;
            float shootingAngle = Vector2.SignedAngle(Vector2.right, shootingDirection);

            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity, bullets.transform);
            newBullet.transform.eulerAngles = new Vector3(0.0f, 0.0f, shootingAngle);
            newBullet.GetComponent<BulletController>().speed = shootingSpeed;
        }
    }    
}
