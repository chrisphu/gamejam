using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandererController : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    float speed = 3.0f;
    float safety = 0.1f;
    Vector2 residualVelocity = new Vector2();
    float bounceSpeed = 2.5f;

    void Awake ()
    {
        #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = 45;
        #endif
    }

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        Vector2 towardsPlayer = (player.transform.position - transform.position).normalized;
        
        // rb.MovePosition(rb.position + towardsPlayer * speed * Time.fixedDeltaTime);
        rb.velocity = towardsPlayer * speed * (1.0f - residualVelocity.magnitude / bounceSpeed) + residualVelocity;
        residualVelocity = Vector2.Lerp(residualVelocity, new Vector2(), 0.9f * Time.fixedDeltaTime * 2.5f);
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
        }
    }
}
