using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTowardsThenDieAction : MonoBehaviour
{
    public GameObject lerpTowards;
    Rigidbody2D rb;
    float lifespan = 0.5f;
    float age = 0.0f;
    ScoreHandler scoreHandler;
    ValidTargetHandler validTargetHandler;
    Vector2 initialPosition;
    Vector3 initialScale;

    void Start()
    {
        #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = 60;
        #endif

        rb = transform.GetComponent<Rigidbody2D>();
        scoreHandler = GameObject.FindGameObjectWithTag("ScoreHandler").GetComponent<ScoreHandler>();
        validTargetHandler = GameObject.FindGameObjectWithTag("ValidTargetHandler").GetComponent<ValidTargetHandler>();

        initialPosition = transform.position;
        initialScale = transform.localScale;
    }

    void FixedUpdate()
    {
        rb.bodyType = RigidbodyType2D.Static;
        rb.constraints = RigidbodyConstraints2D.None;

        if (age > lifespan)
        {
            if (gameObject.CompareTag("Enemy"))
            {
                scoreHandler.score++;
            }

            validTargetHandler.DestroyObjAndJoints(gameObject);
        }
        else
        {
            if (lerpTowards)
            {
                //rb.MovePosition(Vector2.Lerp(rb.position, lerpTowards.transform.position, 0.9f * Time.fixedDeltaTime));
                transform.position = Vector2.Lerp(initialPosition, lerpTowards.transform.position, age / lifespan);
            }
            transform.eulerAngles += new Vector3(0.0f, 0.0f, 60.0f) * Time.fixedDeltaTime;
            transform.localScale = Vector3.Lerp(initialScale, new Vector3(0.0f, 0.0f, 0.0f), age / lifespan);

            age += Time.fixedDeltaTime;
        }
    }
}
