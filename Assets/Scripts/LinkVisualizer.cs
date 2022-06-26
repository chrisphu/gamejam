using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkVisualizer : MonoBehaviour
{
    ArrayList joints = new ArrayList();
    GameObject enemies;
    GameObject tools;
    GameObject templateLine;
    GameObject lines;
    GameObject player;

    void Start()
    {
        enemies = GameObject.FindGameObjectWithTag("Enemies");
        tools = GameObject.FindGameObjectWithTag("Tools");
        templateLine = transform.GetChild(0).gameObject;
        lines = transform.GetChild(1).gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
        UpdateJoints();

        UpdateLineRenderers();

        DrawLines();
    }

    void UpdateJoints()
    {
        joints.Clear();

        foreach (Transform enemy in enemies.GetComponentsInChildren<Transform>())
        {
            foreach (DistanceJoint2D joint in enemy.GetComponents<DistanceJoint2D>())
            {
                if (joint.enabled)
                {
                    joints.Add(joint);
                }
            }
        }

        foreach (Transform tool in tools.GetComponentsInChildren<Transform>())
        {
            foreach (DistanceJoint2D joint in tool.GetComponents<DistanceJoint2D>())
            {
                if (joint.enabled)
                {
                    joints.Add(joint);
                }
            }
        }

        joints.Add(player.GetComponent<DistanceJoint2D>());
    }

    void UpdateLineRenderers()
    {
        // not enough line renderers
        if (joints.Count > lines.transform.childCount)
        {
            int needed = joints.Count - lines.transform.childCount;

            for (int i = 0; i < needed; i++)
            {
                GameObject newLineRenderer = Instantiate(templateLine, new Vector3(), Quaternion.identity, lines.transform);
            }
        }
        // too many line renderers
        else if (joints.Count < lines.transform.childCount)
        {
            int extras = lines.transform.childCount - joints.Count;

            for (int i = 0; i < extras; i++)
            {
                Destroy(lines.transform.GetChild(0).gameObject);
            }
        }
    }

    void DrawLines()
    {
        int i = 0;

        foreach (DistanceJoint2D joint in joints)
        {
            LineRenderer line = lines.transform.GetChild(i).GetComponent<LineRenderer>();

            // safety check due to player's joint
            if (joint.connectedBody != null)
            {
                line.enabled = true;
                line.SetPosition(0, joint.transform.position);
                line.SetPosition(1, Vector3.Lerp(joint.transform.position, joint.connectedBody.transform.position, 0.5f));
                line.SetPosition(2, joint.connectedBody.transform.position);
            }
            else
            {
                line.enabled = false;
            }
            
            i++;
        }
    }
}
