using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject holySquare;
    Vector2 spawnPoint;
    float spawnTime;
    float currentTime;
    public GameObject spawnObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        holySquare = FindObjectsOfType<GameObject>().Where(x => x.name == "HolySquare").FirstOrDefault();
        currentTime = 0.0f;
        spawnTime = 5.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector2.Perpendicular(new Vector2(holySquare.transform.position.x - transform.position.x, holySquare.transform.position.y - transform.position.y)) * 0.2f;
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
    }
}
