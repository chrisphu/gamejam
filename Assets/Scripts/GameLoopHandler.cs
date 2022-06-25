using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameLoopHandler : MonoBehaviour
{
    public bool gameOver { get; private set; } = false;
    GameObject player;
    TMP_Text hpText;
    public int hp = 3;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hpText = GameObject.FindGameObjectWithTag("HP").GetComponent<TMP_Text>();
    }
    
    void LateUpdate()
    {
        hpText.text = "HP: " + hp.ToString();

        if (hp <= 0)
        {
            gameOver = true;
        }
    }
}
