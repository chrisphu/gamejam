using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 spawnPoint;
    float spawnTime;
    float currentTime;
    bool isKinematic;
    public GameObject pointOfRotation;
    public GameObject spawnObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        pointOfRotation = FindObjectsOfType<GameObject>().Where(x => x.name == "HolySquare").FirstOrDefault();
        currentTime = 0.0f;
        spawnTime = 5.0f;
        isKinematic = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isKinematic)
        {
            rb.velocity = Vector2.Perpendicular(new Vector2(pointOfRotation.transform.position.x - transform.position.x, pointOfRotation.transform.position.y - transform.position.y)) * 0.2f;
        }
        currentTime += Time.fixedDeltaTime;
        if (currentTime > spawnTime)
        {
            currentTime = 0.0f;

            spawnPoint = new Vector2(transform.position.x + 1, transform.position.y + 1);
            Instantiate(spawnObject, spawnPoint, Quaternion.identity, FindObjectsOfType<GameObject>().Where(x => x.CompareTag("Enemies")).FirstOrDefault().transform);
            spawnPoint = new Vector2(transform.position.x + 1, transform.position.y - 1);
            Instantiate(spawnObject, spawnPoint, Quaternion.identity, FindObjectsOfType<GameObject>().Where(x => x.CompareTag("Enemies")).FirstOrDefault().transform);
            spawnPoint = new Vector2(transform.position.x - 1, transform.position.y + 1);
            Instantiate(spawnObject, spawnPoint, Quaternion.identity, FindObjectsOfType<GameObject>().Where(x => x.CompareTag("Enemies")).FirstOrDefault().transform);
            spawnPoint = new Vector2(transform.position.x - 1, transform.position.y - 1);
            Instantiate(spawnObject, spawnPoint, Quaternion.identity, FindObjectsOfType<GameObject>().Where(x => x.CompareTag("Enemies")).FirstOrDefault().transform);
        }
        if (gameObject.GetComponent<Joint2D>() && isKinematic)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.mass = 40;
            rb.drag = 15;
            rb.freezeRotation = true;
            isKinematic = false;
        }
    }
}
