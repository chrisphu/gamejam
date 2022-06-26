using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void StartGame()
    {
        SceneManager.LoadScene("MainLoop");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Button startButton = GameObject.Find("Start").GetComponent<Button>();
        Button quitButton = GameObject.Find("Quit").GetComponent<Button>();
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
        Button startButton = GameObject.Find("Start").GetComponent<Button>();
        Button quitButton = GameObject.Find("Quit").GetComponent<Button>();
        startButton.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
        quitButton.GetComponentInChildren<TextMeshProUGUI>().text = "Quit";
    }
}
