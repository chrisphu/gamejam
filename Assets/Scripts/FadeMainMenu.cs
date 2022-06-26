using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMainMenu : MonoBehaviour
{
    float lifespan = 0.75f;
    float age = 0.0f;
    int state = 0;
    CanvasGroup canvasGroup;
    AudioSource audioSource;

    void Start()
    {
        canvasGroup = transform.GetComponent<CanvasGroup>();
        audioSource = transform.GetComponent<AudioSource>();

        FadeIn();
    }

    /*
    void Update()
    {
        if (startFade)
        {
            canvasGroup.interactable = false;
            age += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, Mathf.Clamp(age / lifespan, 0.0f, 1.0f));
            audioSource.volume = Mathf.Lerp(0.05f, 0.0f, Mathf.Clamp(age / lifespan, 0.0f, 1.0f));
        }
    }
    */

    void Update()
    {
        if (state == 1)
        {
            age += Time.deltaTime;

            if (age > lifespan)
            {
                canvasGroup.alpha = 1.0f;
                audioSource.volume = 0.05f;
                state = 0;
            }
            else
            {
                canvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, Mathf.Clamp(age / lifespan, 0.0f, 1.0f));
                audioSource.volume = Mathf.Lerp(0.0f, 0.05f, Mathf.Clamp(age / lifespan, 0.0f, 1.0f));
            }
        }
        else if (state == 2)
        {
            age += Time.deltaTime;

            if (age > lifespan)
            {
                canvasGroup.alpha = 0.0f;
                audioSource.volume = 0.0f;
                state = 0;
            }
            else
            {
                canvasGroup.alpha = Mathf.Lerp(1.0f, 0.0f, Mathf.Clamp(age / lifespan, 0.0f, 1.0f));
                audioSource.volume = Mathf.Lerp(0.05f, 0.0f, Mathf.Clamp(age / lifespan, 0.0f, 1.0f));
            }
        }
    }

    public void FadeIn()
    {
        age = 0.0f;
        state = 1;
    }

    public void FadeOut()
    {
        age = 0.0f;
        state = 2;
    }
}
