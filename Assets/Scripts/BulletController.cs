using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    GameObject player;

    public float speed = 1.0f;
    public int lifespan = 100;
    int age = 0;
    public float damage = 1.0f;
    Vector3 prevPos = new Vector3();

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        prevPos = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 futurePos = transform.position + transform.right * speed * Time.fixedDeltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, (futurePos - prevPos).magnitude);
        if (hit.collider != null && hit.transform.gameObject != player)
        {
            /*
            if (hit.collider.GetComponent<HPHandler>() != null)
            {
                hit.collider.GetComponent<HPHandler>().TakeDamage(damage);
            }
            */

            Destroy(gameObject);
        }

        age++;
        if (age > lifespan)
        {
            Destroy(gameObject);
        }

        transform.position = futurePos;
        prevPos = transform.position;
    }
}
