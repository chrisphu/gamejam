using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNTController : MonoBehaviour
{
    int state = 0;
    float delay = 5.0f;
    float time = 0.0f;
    float blinkRate = 0.25f;
    SpriteRenderer sr;
    ValidTargetHandler validTargetHandler;
    GameObject enemies;
    float killRadius = 2.5f;
    float knockbackRadius = 25.0f;
    ScoreHandler scoreHandler;
    bool exploded = false;
    SpriteRenderer flash;

    void Start()
    {
        sr = transform.GetComponent<SpriteRenderer>();
        validTargetHandler = GameObject.FindGameObjectWithTag("ValidTargetHandler").GetComponent<ValidTargetHandler>();
        enemies = GameObject.FindGameObjectWithTag("Enemies");
        scoreHandler = GameObject.FindGameObjectWithTag("ScoreHandler").GetComponent<ScoreHandler>();
        flash = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (state == 1)
        {
            if (time > delay)
            {
                state = 2;
            }
            else
            {
                time += Time.fixedDeltaTime;

                if (time % blinkRate < blinkRate / 2.0f)
                {
                    sr.color = Color.red;
                }
                else
                {
                    sr.color = Color.white;
                }
            }
        }
        else if (state == 2)
        {
            if (!exploded)
            {
                exploded = true;
                KillAndKnockback();
            }

            flash.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(flash.color.a, 0.0f, 0.9f * Time.fixedDeltaTime * 5.0f));
        }
        else if (state == 3)
        {
            validTargetHandler.DestroyObjAndJoints(gameObject);
        }
    }

    public void ExplodeTNT()
    {
        state = 1;
    }

    void KillAndKnockback()
    {
        sr.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        flash.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        foreach (Transform enemy in enemies.GetComponentInChildren<Transform>())
        {
            float distance = (enemy.position - transform.position).magnitude;

            if (distance < killRadius)
            {
                validTargetHandler.DestroyObjAndJoints(enemy.gameObject);
                scoreHandler.score++;
            }
        }
    }
}
