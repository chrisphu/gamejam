using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BowlingBallController : MonoBehaviour
{
    Rigidbody2D rb;
    ScoreHandler scoreHandler;
    ValidTargetHandler validTargetHandler;
    bool isLaunched = false;
    Vector2 boulderVelocity = new Vector2();
    float age = 0.0f;
    float lifeSpan = 2.5f;
    float initialSpeed = 12.5f;
    float spinRate = 300.0f;
    Transform sprite;
    public bool externalControl = false;
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
        rb = GetComponent<Rigidbody2D>();
        scoreHandler = GameObject.FindGameObjectWithTag("ScoreHandler").GetComponent<ScoreHandler>();
        validTargetHandler = GameObject.FindGameObjectWithTag("ValidTargetHandler").GetComponent<ValidTargetHandler>();
        sprite = transform.GetChild(0);
        audioSource = transform.GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (!externalControl)
        {
            // spin in direction of movement
            if ((rb.velocity.x > 0 && spinRate > 0.0f) || (rb.velocity.x < 0 && spinRate < 0.0f))
            {
                spinRate = -spinRate;
            }

            if (!isLaunched)
            {
                if (gameObject.GetComponent<DistanceJoint2D>() != null)
                {
                    audioSource.Play();
                    DistanceJoint2D joint = gameObject.GetComponent<DistanceJoint2D>();
                    boulderVelocity = (joint.connectedBody.transform.position - rb.transform.position).normalized * initialSpeed;
                    rb.velocity = boulderVelocity;
                    isLaunched = true;
                    validTargetHandler.DestroyJoints(gameObject);
                }
            }
            else
            {
                if (age > lifeSpan)
                {
                    validTargetHandler.DestroyObjAndJoints(gameObject);
                }
                else
                {
                    if (age < 0.8f * lifeSpan)
                    {
                        sprite.eulerAngles += new Vector3(0.0f, 0.0f, spinRate * Time.fixedDeltaTime);
                        rb.velocity = boulderVelocity;
                    }
                    else
                    {
                        sprite.eulerAngles += new Vector3(0.0f, 0.0f, (spinRate / 2.0f) * Time.fixedDeltaTime);
                        rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(), 0.9f * Time.fixedDeltaTime);
                    }
                    age += Time.fixedDeltaTime;
                }
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!externalControl && isLaunched && rb.velocity.magnitude > 3 && collision.gameObject.CompareTag("Enemy"))
        {
            validTargetHandler.DestroyObjAndJoints(collision.gameObject);
            scoreHandler.score++;
        }
    }
}
