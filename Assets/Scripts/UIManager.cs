using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour  // , IPointerEnterHandler, IPointerExitHandler
{
    /*
    Button startButton;
    Button quitButton;

    void Start()
    {
        startButton = GameObject.FindGameObjectWithTag("Start").GetComponent<Button>();
        quitButton = GameObject.FindGameObjectWithTag("Quit").GetComponent<Button>();
    }
    */

    public void StartGame()
    {
        SceneManager.LoadScene("MainLoop");
    }

    public void StartGameWithFade()
    {
        if (transform.GetComponent<FadeMainMenu>() is FadeMainMenu fadeMainMenu)
        {
            fadeMainMenu.FadeOut();
        }
        StartCoroutine(WaitForFade());
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Retry()
    {
        GameObject.Find("Blackout").GetComponent<Image>().CrossFadeAlpha(1.0f, 0.75f, false);
        StartCoroutine(WaitForFade());
    }

    /*
    public void OnPointerEnter(PointerEventData eventData)
    {
        print(eventData.pointerCurrentRaycast.gameObject.name);

        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("StartPanel"))
        {
            startButton.GetComponentInChildren<TextMeshProUGUI>().text = "KILL";
        }
        else if (eventData.pointerCurrentRaycast.gameObject.CompareTag("QuitPanel"))
        {
            quitButton.GetComponentInChildren<TextMeshProUGUI>().text = ":(";
        }

        if (eventData.hovered.FirstOrDefault().CompareTag("Start"))
        {
            startButton.GetComponentInChildren<TextMeshProUGUI>().text = "KILL";
        }
        if (eventData.hovered.FirstOrDefault().CompareTag("Quit"))
        {
            quitButton.GetComponentInChildren<TextMeshProUGUI>().text = ":(";
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        startButton.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        quitButton.GetComponentInChildren<TextMeshProUGUI>().text = "Quit";
    }
    */

    IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(0.75f);
        StartGame();
    }
}
