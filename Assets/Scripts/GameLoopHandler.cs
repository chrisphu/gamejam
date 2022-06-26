using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameLoopHandler : MonoBehaviour
{
    public bool gameOver { get; private set; } = false;
    GameObject player;
    TMP_Text hpText;
    public int hp = 3;
    Image splash;
    AudioSource audioSource;
    GameObject gameOverScreen;
    Image[] gameOverImageComponents;
    TextMeshProUGUI[] textMeshProUguiComponents;

    void Awake ()
    {
        #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = 45;
        #endif
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hpText = GameObject.FindGameObjectWithTag("HP").GetComponent<TMP_Text>();
        splash = GameObject.FindGameObjectWithTag("Splash").GetComponent<Image>();
        audioSource = transform.GetComponent<AudioSource>();
        gameOverScreen = GameObject.Find("GameOver");
        gameOverImageComponents = gameOverScreen.GetComponentsInChildren<Image>();
        textMeshProUguiComponents = gameOverScreen.GetComponentsInChildren<TextMeshProUGUI>();
        gameOverScreen.SetActive(false);
        // audioSource.Play();
    }
    
    void LateUpdate()
    {
        splash.color = new Color(splash.color.r, splash.color.g, splash.color.b, Mathf.Lerp(splash.color.a, 0.0f, 0.9f * Time.deltaTime * 10.0f));

        hpText.text = "HP: " + hp.ToString();

        if (hp <= 0)
        {
            hp = 0;
            gameOver = true;
        }
    }

    void FixedUpdate()
    {
        if (gameOver)
        {
            gameOverScreen.SetActive(true);
            for (float i = 0.000f; i < 1.0f; i += Time.fixedDeltaTime / 100)
            {
                StartCoroutine(IncreaseAlpha(i));
            }
        }
    }

    public void TakeDamage()
    {
        if (!gameOver)
        {
            hp -= 1;
            splash.color = new Color(0.6f, 0.0f, 0.0f, 0.8f);
        }
    }

    IEnumerator IncreaseAlpha(float alpha)
    {
        foreach (var component in gameOverImageComponents)
        {
            component.color = new Color(component.color.r, component.color.g, component.color.b, alpha);
            yield return null;
        }
        foreach (var component in textMeshProUguiComponents)
        {
            component.color = new Color(component.color.r, component.color.g, component.color.b, alpha);
            yield return null;
        }
    }
}
