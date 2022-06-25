using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkVisualizer : MonoBehaviour
{
    public Color color = Color.green;

    void LateUpdate()
    {
        foreach (DistanceJoint2D joint in transform.GetComponents<DistanceJoint2D>())
        {
            if (joint.connectedBody != null)
            {
                Debug.DrawLine(transform.position, joint.connectedBody.transform.position, color);
            }
        }
    }
}
