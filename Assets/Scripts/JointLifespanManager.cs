using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointLifespanManager : MonoBehaviour
{
    public DistanceJoint2D joint;
    public JointLifespanManager manager;
    public float lifespan = 5.0f;
    float age = 0.0f;

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
            age += Time.fixedDeltaTime;
        }
    }
}
