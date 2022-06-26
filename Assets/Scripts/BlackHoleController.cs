using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    GameObject enemies;

    void Start()
    {
        enemies = GameObject.FindGameObjectWithTag("Enemies");
    }

    void FixedUpdate()
    {
        foreach (Transform enemy in enemies.GetComponentInChildren<Transform>())
        {
            
        }
    }
}
