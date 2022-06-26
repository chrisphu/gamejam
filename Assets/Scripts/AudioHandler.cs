using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    AudioSource audioSource;
    float fadeInLifespan = 0.5f;
    float fadeOutLifespan = 2.0f;
    float fadeAge = 0.0f;
    int state = 0;  // 1 = fade in music, 2 = fade out music
    float maxVolume = 0.025f;

    void Awake ()
    {
        #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = 60;
        #endif
    }

    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (state == 1)
        {
            fadeAge += Time.deltaTime;

            if (fadeAge > fadeInLifespan)
            {
                audioSource.volume = maxVolume;
                state = 0;
            }
            else
            {
                audioSource.volume = Mathf.Lerp(0.0f, maxVolume, Mathf.Clamp(fadeAge / fadeInLifespan, 0.0f, 1.0f));
            }
        }
        else if (state == 2)
        {
            fadeAge += Time.deltaTime;

            if (fadeAge > fadeOutLifespan)
            {
                audioSource.volume = 0.0f;
                state = 0;
            }
            else
            {
                audioSource.volume = Mathf.Lerp(maxVolume, 0.0f, Mathf.Clamp(fadeAge / fadeOutLifespan, 0.0f, 1.0f));
            }
        }
    }

    public void FadeMusicIn()
    {
        fadeAge = 0.0f;
        state = 1;
    }

    public void FadeMusicOut()
    {
        fadeAge = 0.0f;
        state = 2;
    }
}
