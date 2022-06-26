using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    TMP_Text scoreText;
    public int score = 0;

    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TMP_Text>();
    }

    void FixedUpdate()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
