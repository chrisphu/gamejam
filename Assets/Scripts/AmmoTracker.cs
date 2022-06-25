using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoTracker : MonoBehaviour
{
    public int maxAmmo = 30;
    public int ammo = 30;
    
    TMP_Text ammoCounter;

    void Start()
    {
        ammoCounter = GameObject.FindGameObjectWithTag("AmmoCount").GetComponent<TMP_Text>();
    }

    void LateUpdate()
    {
        if (ammo > 0)
        {
            ammoCounter.text = ammo.ToString() + " / " + maxAmmo.ToString();
        }
        else
        {
            ammoCounter.text = "Reloading...";
        }
    }
}
