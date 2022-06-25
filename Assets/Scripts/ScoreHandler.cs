using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    float speed = 2.0f;
    GameObject player;
    float safety = 0.1f;
    TMP_Text scoreText;
    int score = 0;
    float i = 0.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TMP_Text>();
    }

    void FixedUpdate()
    {
        foreach (DistanceJoint2D joint in transform.GetComponents<DistanceJoint2D>())
        {
            joint.distance -= speed * Time.fixedDeltaTime;
        }

        scoreText.text = "Score: " + score.ToString();

        // change size slightly to help make collisions happen
        i += Time.fixedDeltaTime;
        float scaleAdjust = Mathf.Sin(i) * 0.25f + 0.25f;
        transform.localScale = new Vector3(5.0f + scaleAdjust, 5.0f + scaleAdjust, 1.0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
        {
            bool go = false;

            foreach (DistanceJoint2D joint in transform.GetComponents<DistanceJoint2D>())
            {
                if (joint.connectedBody == collision.collider.GetComponent<Rigidbody2D>())
                {
                    go = true;
                    break;
                }
            }

            if (go)
            {
                foreach (DistanceJoint2D joint in collision.collider.GetComponents<DistanceJoint2D>())
                {
                    if (joint.connectedBody != null)
                    {
                        foreach (DistanceJoint2D otherJoint in joint.connectedBody.GetComponents<DistanceJoint2D>())
                        {
                            if (otherJoint.connectedBody == collision.collider.GetComponent<Rigidbody2D>())
                            {
                                Destroy(otherJoint);
                            }
                        }
                    }

                    Destroy(joint);
                }

                Destroy(collision.collider.gameObject);
                score++;
            }
        }

        /*
        else
        {
            Vector2 towardsPlayer = (player.transform.position - transform.position).normalized;

            // move player to safety and add bounce velocity to player
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            PlayerController playerController = player.GetComponent<PlayerController>();
            playerRb.MovePosition(playerRb.position + towardsPlayer * safety);
            playerController.residualVelocity = towardsPlayer * playerController.bounceSpeed;
        }
        */
    }
}
