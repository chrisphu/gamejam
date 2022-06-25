using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;

    KeyCode rightMove = KeyCode.D;
    KeyCode leftMove = KeyCode.A;
    KeyCode upMove = KeyCode.W;
    KeyCode downMove = KeyCode.S;

    Rigidbody2D rb;

    void Awake ()
    {
        #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = 45;
        #endif
    }

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        int hor = Convert.ToInt16(Input.GetKey(rightMove)) - Convert.ToInt32(Input.GetKey(leftMove));
        int ver = Convert.ToInt16(Input.GetKey(upMove)) - Convert.ToInt32(Input.GetKey(downMove));

        rb.MovePosition(rb.position + new Vector2(hor, ver).normalized * speed * Time.fixedDeltaTime);
    }
}
