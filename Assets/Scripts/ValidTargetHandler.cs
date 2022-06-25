using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidTargetHandler : MonoBehaviour
{
    public bool CheckIfTargetValue(GameObject object1, GameObject object2)
    {
        bool go = true;

        if (object1 != object2)
        {
            // check object1 for existing joint
            foreach (DistanceJoint2D joint in object1.GetComponents<DistanceJoint2D>())
            {
                if (joint.connectedBody == object2.GetComponent<Rigidbody2D>())
                {
                    go = false;
                }
            }

            // check object2 for existing joint
            foreach (DistanceJoint2D joint in object2.GetComponents<DistanceJoint2D>())
            {
                if (joint.connectedBody == object1.GetComponent<Rigidbody2D>())
                {
                    go = false;
                }
            }
        }
        else
        {
            go = false;
        }

        return go;
    }
}
