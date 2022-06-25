using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBoxCollider : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 positionDifference;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 45;
#endif
    }

    void FixedUpdate()
    {
        if (rb.GetComponent<FixedJoint2D>().connectedBody is Rigidbody2D player)
        {
            rb.transform.position = player.transform.position + positionDifference;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody.gameObject.CompareTag("Player"))
        {
            collision.otherRigidbody.GetComponent<FixedJoint2D>().connectedBody = collision.rigidbody;
            positionDifference = collision.rigidbody.position = collision.otherRigidbody.position;
        }
    }
}
