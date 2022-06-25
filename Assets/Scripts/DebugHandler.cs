using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugHandler : MonoBehaviour
{
    void LateUpdate()
    {
        print(transform.GetComponents<DistanceJoint2D>().Length);
    }
}
