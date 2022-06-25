using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkVisualizer : MonoBehaviour
{
    public Color color = Color.green;

    void LateUpdate()
    {
        DistanceJoint2D[] joints = transform.GetComponents<DistanceJoint2D>();

        if (joints.Length > 0)
        {
            foreach (DistanceJoint2D joint in joints)
            {
                if (joint.connectedBody != null)
                {
                    Debug.DrawLine(transform.position, joint.connectedBody.transform.position, color);
                }
            }
        }
        else
        {
            Destroy(transform.GetComponent<LinkVisualizer>());
        }
    }
}
