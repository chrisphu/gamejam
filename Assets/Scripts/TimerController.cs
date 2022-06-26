using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    GameLoopHandler gameLoopHandler;
    float gameTime = 0.0f;

    void Awake ()
    {
        #if UNITY_EDITOR
            QualitySettings.vSyncCount = 0;  // VSync must be disabled
            Application.targetFrameRate = 60;
        #endif
    }

    void Start()
    {
        gameLoopHandler = GameObject.FindGameObjectWithTag("GameLoopHandler").GetComponent<GameLoopHandler>();
    }

    void LateUpdate()
    {
        if (!gameLoopHandler.gameOver)
        {

        }
    }
}
