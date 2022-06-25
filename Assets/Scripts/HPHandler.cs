using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPHandler : MonoBehaviour
{
    public float hp = 5.0f;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void TakeDamage(float x)
    {
        hp -= x;

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
