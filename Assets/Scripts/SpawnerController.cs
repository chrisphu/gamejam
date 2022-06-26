using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    Camera mainCamera;
    public GameObject spawnObject;
    GameObject player;

    Vector2 upperleft = new Vector2();
    Vector2 upperright = new Vector2();
    Vector2 bottomleft = new Vector2();
    Vector2 bottomright = new Vector2();
    float safety = 32.0f;
    float spawnTime = 1.0f;
    float currentTime = 0.0f;
    GameLoopHandler gameLoopHandler;

    // Start is called before the first frame update
    void Start()
    {
        gameLoopHandler = GameObject.FindGameObjectWithTag("GameLoopHandler").GetComponent<GameLoopHandler>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        //Instantiate(spawnObject, new Vector2(), Quaternion.identity);
        //shieldBoxes.SetValue(shieldBox, shieldBoxes.Length);
        //frameCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScreenCorners();
    }

    void FixedUpdate()
    {
        if (!gameLoopHandler.gameOver)
        {
            UpdateScreenCorners();

            currentTime += Time.fixedDeltaTime;

            if (currentTime > spawnTime)
            {
                currentTime = 0.0f;

                int spawnSide = Random.Range(0, 3);
                Vector2 spawnPoint = new Vector2();

                if (spawnSide == 0)
                {
                    spawnPoint = Vector2.Lerp(bottomleft, bottomright, Random.Range(0.0f, 1.0f));
                }
                else if (spawnSide == 1)
                {
                    spawnPoint = Vector2.Lerp(bottomright, upperright, Random.Range(0.0f, 1.0f));
                }
                else if (spawnSide == 2)
                {
                    spawnPoint = Vector2.Lerp(upperright, upperleft, Random.Range(0.0f, 1.0f));
                }
                else
                {
                    spawnPoint = Vector2.Lerp(upperleft, bottomleft, Random.Range(0.0f, 1.0f));
                }

                Instantiate(spawnObject, spawnPoint, Quaternion.identity, transform);
            }
        }
    }

    void UpdateScreenCorners()
    {
        bottomleft = mainCamera.ScreenToWorldPoint(new Vector2(-safety, -safety));
        upperleft = mainCamera.ScreenToWorldPoint(new Vector2(-safety, Screen.height + safety));
        bottomright = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width + safety, -safety));
        upperright = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width + safety, Screen.height + safety));
    }
}
