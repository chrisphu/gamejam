using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    Camera mainCamera;
    public GameObject basicEnemy;
    public GameObject spawner;
    public GameObject tnt;
    public GameObject bowlingBall;
    public GameObject blackHole;
    GameObject player;

    Vector2 upperleft = new Vector2();
    Vector2 upperright = new Vector2();
    Vector2 bottomleft = new Vector2();
    Vector2 bottomright = new Vector2();
    float safety = 32.0f;
    float spawnTime = 3.0f;
    float currentTime = 0.0f;
    GameLoopHandler gameLoopHandler;
    public int currentLoop;

    // Start is called before the first frame update
    void Start()
    {
        currentLoop = 1;
        gameLoopHandler = GameObject.FindGameObjectWithTag("GameLoopHandler").GetComponent<GameLoopHandler>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        //Instantiate(spawnObject, new Vector2(), Quaternion.identity);
        //shieldBoxes.SetValue(shieldBox, shieldBoxes.Length);
        //frameCounter = 0;
    }

    void FixedUpdate()
    {
        if (!gameLoopHandler.gameOver)
        {
            UpdateScreenCorners();

            currentTime += Time.fixedDeltaTime;

            if (currentTime / currentLoop % 3.02 > spawnTime)
            {
                int spawnSide = Random.Range(0, 4);
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

                Instantiate(basicEnemy, spawnPoint, Quaternion.identity, transform);
            }
            if (currentTime / currentLoop > spawnTime * 5)
            {
                currentTime = 0.0f;

                Instantiate(bowlingBall, new Vector2(Random.Range(-31, 31), Random.Range(-31, 31)), Quaternion.identity, GameObject.Find("Tools").transform);
                Instantiate(tnt, new Vector2(Random.Range(-31, 31), Random.Range(-31, 31)), Quaternion.identity, GameObject.Find("Tools").transform);
                Instantiate(blackHole, new Vector2(Random.Range(-31, 31), Random.Range(-31, 31)), Quaternion.identity, GameObject.Find("Tools").transform);
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
