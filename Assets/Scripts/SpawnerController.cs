using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    Camera mainCamera;

    public GameObject[] spawnables;
    public float[] spawnTimes;
    float[] currentTimes;
    public bool[] isEnemy;

    /*
    public GameObject basicEnemy;
    public GameObject spawner;
    public GameObject tnt;
    public GameObject bowlingBall;
    public GameObject blackHole;
    */

    GameObject player;
    GameObject enemies;
    GameObject tools;

    Vector2 upperleft = new Vector2();
    Vector2 upperright = new Vector2();
    Vector2 bottomleft = new Vector2();
    Vector2 bottomright = new Vector2();
    float safety = 32.0f;
    float spawnTime = 3.0f;
    GameLoopHandler gameLoopHandler;
    // public int currentLoop;

    void Start()
    {
        // currentLoop = 1;

        gameLoopHandler = GameObject.FindGameObjectWithTag("GameLoopHandler").GetComponent<GameLoopHandler>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        player = GameObject.FindGameObjectWithTag("Player");
        enemies = GameObject.FindGameObjectWithTag("Enemies");
        tools = GameObject.FindGameObjectWithTag("Tools");

        currentTimes = new float[spawnTimes.Length];
    }

    void FixedUpdate()
    {
        if (!gameLoopHandler.gameOver)
        {
            UpdateScreenCorners();

            for (int i = 0; i < currentTimes.Length; i++)
            {
                currentTimes[i] += Time.fixedDeltaTime;

                if (currentTimes[i] > spawnTimes[i])
                {
                    Vector2 spawnPoint;
                    Transform spawnParent;

                    if (isEnemy[i])
                    {
                        spawnPoint = RandomSpawnAroundCamera();
                        spawnParent = enemies.transform;
                    }
                    else
                    {
                        spawnPoint = new Vector2(Random.Range(-31.0f, 31.0f), Random.Range(-31.0f, 31.0f));
                        spawnParent = tools.transform;
                    }

                    Instantiate(spawnables[i], spawnPoint, Quaternion.identity, spawnParent);

                    currentTimes[i] = 0.0f;
                }
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

    Vector2 RandomSpawnAroundCamera()
    {
        Vector2 spawnPoint = new Vector2();
        int spawnSide = Random.Range(0, 4);

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

        return spawnPoint;
    }
}
