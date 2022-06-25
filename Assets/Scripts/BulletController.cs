using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public ShootingController shootingController;
    public int objectToFind = 1;
    GameObject player;

    public float speed = 1.0f;
    public int lifespan = 100;
    int age = 0;
    float offscreenLifespan = 0.5f;
    float offscreenAge = 0.0f;
    public float damage = 1.0f;
    Vector3 prevPos = new Vector3();

    SpriteRenderer sr;
    ValidTargetHandler validTargetHandler;

    void Awake()
    {
        sr = transform.GetComponent<SpriteRenderer>();
        validTargetHandler = GameObject.FindGameObjectWithTag("ValidTargetHandler").GetComponent<ValidTargetHandler>();
        player = GameObject.FindGameObjectWithTag("Player");
        prevPos = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 futurePos = transform.position + transform.right * speed * Time.fixedDeltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, (futurePos - prevPos).magnitude);

        if (hit.collider != null && hit.transform.gameObject != player)
        {
            if (shootingController.object1 == null)
            {
                shootingController.object1 = hit.collider.gameObject;
            }
            else if (shootingController.object2 == null)
            {
                if (validTargetHandler.CheckIfTargetValue(shootingController.object1, hit.collider.gameObject))
                {
                    shootingController.object2 = hit.collider.gameObject;
                }
            }

            Destroy(gameObject);
        }

        if (!sr.isVisible)
        {
            offscreenAge += Time.fixedDeltaTime;
        }

        // early destruction conditions
        age++;
        if (age > lifespan || offscreenAge > offscreenLifespan)
        {
            Destroy(gameObject);
        }

        transform.position = futurePos;
        prevPos = transform.position;
    }
}
