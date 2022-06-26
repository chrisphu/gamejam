using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BowlingBallController : MonoBehaviour
{
    Rigidbody2D rb;
    ScoreHandler scoreHandler;
    bool isLaunched;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scoreHandler = GameObject.FindGameObjectsWithTag("ScoreHandler").FirstOrDefault().GetComponent<ScoreHandler>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.GetComponent<Joint2D>() is DistanceJoint2D joint)
        {
            rb.velocity = joint.connectedBody.transform.position * 2 - rb.transform.position * 2;
            isLaunched = true;
            Destroy(joint.connectedBody.gameObject.GetComponent<Joint2D>());
            Destroy(joint);
        }
        if (isLaunched && rb.velocity == Vector2.zero)
            isLaunched = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isLaunched && rb.velocity.magnitude > 3 && collision.gameObject.CompareTag("Enemies"))
        {
            Destroy(collision.gameObject);
            scoreHandler.score++;
        }
    }
}
