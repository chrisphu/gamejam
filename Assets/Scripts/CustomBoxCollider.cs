using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBoxCollider : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 positionDifference;
    GameObject attachedObject;
    bool isAttached;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (isAttached)
            rb.transform.position = attachedObject.transform.position - positionDifference;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isAttached && collision.rigidbody.gameObject.CompareTag("Player"))
        {
            attachedObject = collision.rigidbody.gameObject;
            positionDifference = attachedObject.transform.position - rb.transform.position;
            isAttached = true;
        }
    }
}
