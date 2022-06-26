using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{

    public void StartGame()
    {
        SceneManager.LoadScene("MainLoop");
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

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    Button startButton = GameObject.Find("Start").GetComponent<Button>();
    //    Button quitButton = GameObject.Find("Quit").GetComponent<Button>();
    //    if (eventData.hovered.Where(x => x.CompareTag("Start")).Any())
    //    {
    //        startButton.GetComponentInChildren<TextMeshProUGUI>().text = "KILL";
    //        eventData.Reset();
    //    }
    //    if (eventData.hovered.Where(x => x.CompareTag("Quit")).Any())
    //    {
    //        quitButton.GetComponentInChildren<TextMeshProUGUI>().text = ":(";
    //        eventData.Reset();
    //    }
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    Button startButton = GameObject.Find("Start").GetComponent<Button>();
    //    Button quitButton = GameObject.Find("Quit").GetComponent<Button>();
    //    startButton.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
    //    quitButton.GetComponentInChildren<TextMeshProUGUI>().text = "Quit";
    //    eventData.Reset();
    //}

    IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(0.75f);

        StartGame();
    }
}
