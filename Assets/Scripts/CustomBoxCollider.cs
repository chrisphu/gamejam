using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBoxCollider : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 positionDifference;
    bool isAttached;
    GameObject player;
    GameObject attachedObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectsWithTag("Player").FirstOrDefault();
        isAttached = false;
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

    void LateUpdate()
    {
        if (attachedObject && isAttached)
            rb.transform.position = player.transform.position - positionDifference;
        else
            isAttached = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAttached && (collision.rigidbody.gameObject.CompareTag("Player") || collision.rigidbody.gameObject.CompareTag("ShieldBox")))
        {
            attachedObject = collision.rigidbody.gameObject;
            positionDifference = player.transform.position - rb.transform.position;
            isAttached = true;
        }
    }
}