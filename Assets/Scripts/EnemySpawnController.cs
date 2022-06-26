using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public GameObject pointOfRotation;
    public GameObject spawnObject;

    Rigidbody2D rb;
    float spawnTime = 5.0f;
    float currentTime = 0.0f;
    GameObject enemies;
    public bool externalControl = false;
    public bool isKinematic = true;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        enemies = GameObject.FindGameObjectWithTag("Enemies");
    }

    void FixedUpdate()
    {
        if (!externalControl)
        {
            if (pointOfRotation == null)
            {
                isKinematic = false;
            }

            if (isKinematic)
            {
                rb.velocity = Vector2.Perpendicular(new Vector2(pointOfRotation.transform.position.x - transform.position.x, pointOfRotation.transform.position.y - transform.position.y)) * 0.2f;
            }

            currentTime += Time.fixedDeltaTime;

            if (currentTime > spawnTime)
            {
                currentTime = 0.0f;

                Vector2 spawnPoint = new Vector2(transform.position.x + 1, transform.position.y + 1);
                Instantiate(spawnObject, spawnPoint, Quaternion.identity, enemies.transform);

                spawnPoint = new Vector2(transform.position.x + 1, transform.position.y - 1);
                Instantiate(spawnObject, spawnPoint, Quaternion.identity, enemies.transform);

                spawnPoint = new Vector2(transform.position.x - 1, transform.position.y + 1);
                Instantiate(spawnObject, spawnPoint, Quaternion.identity, enemies.transform);

                spawnPoint = new Vector2(transform.position.x - 1, transform.position.y - 1);
                Instantiate(spawnObject, spawnPoint, Quaternion.identity, enemies.transform);
            }
        }
    }
}
