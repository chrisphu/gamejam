using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointLifespanManager : MonoBehaviour
{
    public DistanceJoint2D joint;
    public JointLifespanManager manager;
    public float lifespan = 5.0f;
    float age = 0.0f;
    float speed = 5.0f;

    void FixedUpdate()
    {
        if (joint == null)
        {
            Destroy(manager);
        }

        if (age > lifespan)
        {
            Destroy(joint);
        }
        else
        {
            if (joint != null)
            {
                if (!joint.CompareTag("HolySquare") && !joint.connectedBody.CompareTag("HolySquare"))
                {
                    joint.distance -= speed * Time.fixedDeltaTime;
                }
            }
            age += Time.fixedDeltaTime;
        }
    }
}
