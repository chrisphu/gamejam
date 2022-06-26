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
    float killRadius = 5.0f * 1.25f / 2.0f;
    ScoreHandler scoreHandler;
    bool exploded = false;
    SpriteRenderer flash;
    Rigidbody2D rb;
    AudioSource explosionSound;
    AudioSource sizzleSound;
    bool sizzleStart = false;
    float selfDestroyTime = 1.0f;
    float timeAfterExplosion = 0.0f;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        sr = transform.GetComponent<SpriteRenderer>();
        validTargetHandler = GameObject.FindGameObjectWithTag("ValidTargetHandler").GetComponent<ValidTargetHandler>();
        enemies = GameObject.FindGameObjectWithTag("Enemies");
        scoreHandler = GameObject.FindGameObjectWithTag("ScoreHandler").GetComponent<ScoreHandler>();

        flash = transform.GetChild(0).GetComponent<SpriteRenderer>();
        flash.transform.localScale = new Vector3(killRadius * 2.0f, killRadius * 2.0f, 1.0f);

        explosionSound = transform.GetComponent<AudioSource>();
        sizzleSound = transform.GetChild(1).GetComponent<AudioSource>();
    }

    void Update()
    {
        if (state == 1)
        {
            if (!sizzleStart)
            {
                sizzleStart = true;
                sizzleSound.Play();
            }

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
            rb.bodyType = RigidbodyType2D.Static;

            if (!exploded)
            {
                exploded = true;
                KillAndKnockback();
            }

            flash.color = new Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(flash.color.a, 0.0f, 0.9f * Time.fixedDeltaTime * 5.0f));

            if (timeAfterExplosion > selfDestroyTime)
            {
                validTargetHandler.DestroyObjAndJoints(gameObject);
            }
            else
            {
                timeAfterExplosion += Time.fixedDeltaTime;
            }
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
        sizzleSound.Stop();
        explosionSound.Play();
        Destroy(transform.GetComponent<BoxCollider2D>());

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
