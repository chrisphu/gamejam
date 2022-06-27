using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockAnimation : MonoBehaviour
{
    ScoreHandler scoreHandler;
    SpriteRenderer sr;
    float lifespan = 0.5f;
    float age = 0.0f;
    float blinkRate = 0.1f;

    void Awake()
    {
        #if UNITY_EDITOR
                QualitySettings.vSyncCount = 0;  // VSync must be disabled
                Application.targetFrameRate = 60;
        #endif
    }

    void Start()
    {
        scoreHandler = GameObject.FindGameObjectWithTag("ScoreHandler").GetComponent<ScoreHandler>();
        sr = transform.GetComponent<SpriteRenderer>();

        if (transform.GetComponent<WandererController>() != null)
        {
            transform.GetComponent<WandererController>().externalControl = true;
        }
    }

    void FixedUpdate()
    {
        if (age > lifespan)
        {
            scoreHandler.score++;
            Destroy(gameObject);
        }
        else
        {
            age += Time.fixedDeltaTime;

            if (age % blinkRate < blinkRate / 2.0f)
            {
                sr.flipX = true;
                sr.flipY = true;
                sr.color = Color.white;
            }
            else
            {
                sr.flipX = false;
                sr.flipY = false;
                sr.color = Color.cyan;
            }
        }
    }
}
