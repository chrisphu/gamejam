using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    TMP_Text scoreText;
    public int score = 0;
    GameObject enemies;

    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TMP_Text>();
        enemies = GameObject.FindGameObjectWithTag("Enemies");
    }

    void FixedUpdate()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
