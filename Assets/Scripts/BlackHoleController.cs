using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        var newSpawner = Instantiate(spawner, new Vector2(gameObject.transform.position.x + Random.Range(4, 8), gameObject.transform.position.y + Random.Range(4, 8)), Quaternion.identity, GameObject.Find("Enemies").transform);
        newSpawner.GetComponent<EnemySpawnController>().pointOfRotation = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
