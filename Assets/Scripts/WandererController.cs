using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererController : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    float speed = 2.0f;
    float safety = 0.1f;
    Vector2 residualVelocity = new Vector2();
    float bounceSpeed = 3.5f;
    GameLoopHandler gameLoopHandler;
    SpriteRenderer sr;

    void Awake ()
    {
        #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = 45;
        #endif
    }

    void Start()
    {
        gameLoopHandler = GameObject.FindGameObjectWithTag("GameLoopHandler").GetComponent<GameLoopHandler>();
        rb = transform.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        sr = transform.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!gameLoopHandler.gameOver)
        {
            Vector2 towardsPlayer = (player.transform.position - transform.position).normalized;
            
            // rb.MovePosition(rb.position + towardsPlayer * speed * Time.fixedDeltaTime);
            rb.velocity = towardsPlayer * speed * (1.0f - residualVelocity.magnitude / bounceSpeed) + residualVelocity;
            residualVelocity = Vector2.Lerp(residualVelocity, new Vector2(), 0.9f * Time.fixedDeltaTime * 2.5f);

            sr.flipX = (rb.velocity.x < 0.0f);
        }
        else
        {
            rb.velocity = new Vector2();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector2 towardsPlayer = (player.transform.position - transform.position).normalized;

            // move to safety and add bounce velocity to self
            rb.MovePosition(rb.position - towardsPlayer * safety);
            residualVelocity = -towardsPlayer * bounceSpeed;

            // move player to safety and add bounce velocity to player
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerRb.MovePosition(playerRb.position + towardsPlayer * safety);
            playerController.residualVelocity = towardsPlayer * playerController.bounceSpeed;

            gameLoopHandler.hp -= 1;
        }
    }
}
