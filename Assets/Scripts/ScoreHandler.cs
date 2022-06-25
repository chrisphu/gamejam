using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    float speed = 4.0f;
    GameObject player;
    float safety = 0.1f;
    TMP_Text scoreText;
    int score = 0;
    float i = 0.0f;
    GameObject enemies;
    float killDistance = 3.0f;

    void Awake ()
    {
        #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = 45;
        #endif
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TMP_Text>();
        enemies = GameObject.FindGameObjectWithTag("Enemies");
    }

    void FixedUpdate()
    {
        // pull enemies in
        foreach (DistanceJoint2D joint in transform.GetComponents<DistanceJoint2D>())
        {
            joint.distance -= speed * Time.fixedDeltaTime;
        }

        // destroy enemies (and their joints) if they get too close *and* are jointed
        foreach (Transform enemy in enemies.GetComponentsInChildren<Transform>())
        {
            float distance = (enemy.transform.position - transform.position).magnitude;

            if (distance <= killDistance)
            {
                bool go = false;

                foreach (DistanceJoint2D joint in transform.GetComponents<DistanceJoint2D>())
                {
                    if (joint.connectedBody == enemy.GetComponent<Rigidbody2D>())
                    {
                        go = true;
                        break;
                    }
                }

                if (go)
                {
                    foreach (DistanceJoint2D joint in enemy.GetComponents<DistanceJoint2D>())
                    {
                        if (joint.connectedBody != null)
                        {
                            foreach (DistanceJoint2D otherJoint in joint.connectedBody.GetComponents<DistanceJoint2D>())
                            {
                                if (otherJoint.connectedBody == enemy.GetComponent<Rigidbody2D>())
                                {
                                    Destroy(otherJoint);
                                }
                            }
                        }

                        Destroy(joint);
                    }

                    Destroy(enemy.gameObject);
                    score++;
                }

            }
        }

        scoreText.text = "Score: " + score.ToString();
    }
}
