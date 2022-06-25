using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    float speed = 1.0f;

    void FixedUpdate()
    {
        foreach (DistanceJoint2D joint in transform.GetComponents<DistanceJoint2D>())
        {
            joint.distance -= speed * Time.fixedDeltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
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
        }
    }
}
