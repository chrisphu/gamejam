using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuArtController : MonoBehaviour
{
    RectTransform rectTransform;
    float speed = 2.0f;
    public float angle = 5.0f;
    float i;

    void Awake ()
    {
        #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = 60;
        #endif
    }

    void Start()
    {
        rectTransform = transform.GetComponent<RectTransform>();
    }

    void Update()
    {
        i += Time.deltaTime;
        rectTransform.eulerAngles = new Vector3(0.0f, 0.0f, angle * Mathf.Sin(i));
    }
}
