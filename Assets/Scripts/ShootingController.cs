using UnityEngine;

public class ShootingController : MonoBehaviour
{
    KeyCode resetObject1 = KeyCode.R;

    public GameObject bullet;
    GameObject bullets;

    float shootingSpeed = 30.0f;
    float initialOffset = 0.75f;

    public GameObject object1;
    public GameObject object2;
    GameLoopHandler gameLoopHandler;
    ValidTargetHandler validTargetHandler;
    AudioSource audioSource;

    void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;
#endif
    }

    void Start()
    {
        gameLoopHandler = GameObject.FindGameObjectWithTag("GameLoopHandler").GetComponent<GameLoopHandler>();
        validTargetHandler = GameObject.FindGameObjectWithTag("ValidTargetHandler").GetComponent<ValidTargetHandler>();
        bullets = GameObject.FindGameObjectWithTag("Bullets");
        audioSource = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!gameLoopHandler.gameOver)
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

                audioSource.Play();
            }

            if (object1 && object2)
            {
                transform.GetComponent<DistanceJoint2D>().connectedBody = null;

                if (!object1.CompareTag("BlackHole") || !object2.CompareTag("BlackHole"))
                {
                    // blackhole always object 1
                    if (object2.CompareTag("BlackHole"))
                    {
                        GameObject tempObject = object1;
                        object1 = object2;
                        object2 = tempObject;
                    }

                    // don't really need to check but good safety
                    if (validTargetHandler.CheckIfTargetValue(object1, object2))
                    {
                        DistanceJoint2D newJoint = object1.AddComponent<DistanceJoint2D>();
                        newJoint.connectedBody = object2.GetComponent<Rigidbody2D>();
                        newJoint.maxDistanceOnly = true;
                        newJoint.enableCollision = true;
                        newJoint.autoConfigureDistance = false;
                        newJoint.distance = (object1.transform.position - object2.transform.position).magnitude;

                        JointLifespanManager newManager = object1.AddComponent<JointLifespanManager>();
                        newManager.joint = newJoint;
                        newManager.lifespan = 30.0f;
                        newManager.manager = newManager;

                        // dummy joint for counting connections
                        DistanceJoint2D newDummyJoint = object2.AddComponent<DistanceJoint2D>();
                        newDummyJoint.connectedBody = object1.GetComponent<Rigidbody2D>();
                        newDummyJoint.enabled = false;

                        JointLifespanManager newDummyManager = object2.AddComponent<JointLifespanManager>();
                        newDummyManager.joint = newDummyJoint;
                        newDummyManager.lifespan = 30.0f;
                        newDummyManager.manager = newDummyManager;
                    }
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
}
