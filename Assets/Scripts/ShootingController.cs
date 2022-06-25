using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    KeyCode resetObject1 = KeyCode.R;

    public GameObject bullet;
    GameObject bullets;

    float shootingSpeed = 20.0f;
    float initialOffset = 0.75f;

    public GameObject object1;
    public GameObject object2;
    ValidTargetHandler validTargetHandler;

    void Awake ()
    {
        #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = 45;
        #endif
    }

    void Start()
    {
        validTargetHandler = GameObject.FindGameObjectWithTag("ValidTargetHandler").GetComponent<ValidTargetHandler>();
        bullets = GameObject.FindGameObjectWithTag("Bullets");
    }

    void Update()
    {
        if (Input.GetKeyDown(resetObject1))
        {
            object1 = null;
            object2 = null;
        }

        if (Input.GetMouseButtonDown(0) && bullets.transform.childCount < 1)
        {
            // calculate bullet angle
            Vector3 screenCenter = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
            Vector2 shootingDirection = (Input.mousePosition - screenCenter).normalized;
            float shootingAngle = Vector2.SignedAngle(Vector2.right, shootingDirection);

            // spawn new bullet
            GameObject newBullet = Instantiate(bullet, transform.position + new Vector3(shootingDirection.x, shootingDirection.y, 0.0f).normalized * initialOffset, Quaternion.identity, bullets.transform);
            newBullet.transform.eulerAngles = new Vector3(0.0f, 0.0f, shootingAngle);
            newBullet.GetComponent<BulletController>().speed = shootingSpeed;
            newBullet.GetComponent<BulletController>().shootingController = transform.GetComponent<ShootingController>();

            // change bullet color depending on current object
            if (object1 == null)
            {
                newBullet.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                newBullet.GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }

        if (object1 && object2)
        {
            transform.GetComponent<DistanceJoint2D>().connectedBody = null;

            // don't really need to check but good safety
            if (validTargetHandler.CheckIfTargetValue(object1, object2))
            {
                if (object2.CompareTag("HolySquare"))
                {
                    GameObject tempObject = object1;
                    object1 = object2;
                    object2 = tempObject;
                }

                DistanceJoint2D newJoint = object1.AddComponent<DistanceJoint2D>();
                newJoint.connectedBody = object2.GetComponent<Rigidbody2D>();
                newJoint.maxDistanceOnly = true;
                newJoint.enableCollision = true;

                JointLifespanManager newManager = object1.AddComponent<JointLifespanManager>();
                newManager.joint = newJoint;
                newManager.lifespan = 30.0f;
                newManager.manager = newManager;

                if (object1.GetComponent<LinkVisualizer>() == null)
                {
                    LinkVisualizer linkVisualizer = object1.AddComponent<LinkVisualizer>();
                }

                // dummy joint for counting connections
                DistanceJoint2D newDummyJoint = object2.AddComponent<DistanceJoint2D>();
                newDummyJoint.connectedBody = object1.GetComponent<Rigidbody2D>();
                newDummyJoint.enabled = false;

                JointLifespanManager newDummyManager = object2.AddComponent<JointLifespanManager>();
                newDummyManager.joint = newDummyJoint;
                newDummyManager.lifespan = 30.0f;
                newDummyManager.manager = newDummyManager;
            }

            object1 = null;
            object2 = null;
        }
        else if (object1 && !object2)
        {
            transform.GetComponent<DistanceJoint2D>().connectedBody = object1.GetComponent<Rigidbody2D>();
        }
        else
        {
            transform.GetComponent<DistanceJoint2D>().connectedBody = null;
        }
    }
}
