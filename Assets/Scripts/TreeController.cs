using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    int uprootThreshold;
    int breakThreshold;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        uprootThreshold = 2;
        breakThreshold = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (IsUprootThresholdBroken())
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.mass = 10.0f;
            rb.drag = 10.0f;
            rb.freezeRotation = true;
        }
        else if (rb.bodyType == RigidbodyType2D.Dynamic)
            rb.bodyType = RigidbodyType2D.Static;
        if (IsBreakThresholdBroken(out var joints))
        {
            foreach (var joint in joints)
            {
                if (joint.connectedBody)
                    Destroy(joint.connectedBody.GetComponents<Joint2D>().Where(x => x.connectedBody == joint.attachedRigidbody).FirstOrDefault());
                Destroy(joint);
            }
        }
    }

    bool IsUprootThresholdBroken()
        => rb.GetComponents<Joint2D>().Length > uprootThreshold;

    bool IsBreakThresholdBroken(out Joint2D[] joints)
    {
        joints = rb.GetComponents<Joint2D>();
        return joints.Length > breakThreshold;
    }
}
