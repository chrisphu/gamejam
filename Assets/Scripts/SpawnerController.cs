using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    Camera mainCamera;

    public GameObject[] spawnables;
    public float[] startSpawnTimes;
    public float[] endSpawnTimes;
    float[] iterSpawnTimes;
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
    float screenSafety = 32.0f;
    float playerSafety = 2.0f;
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

        currentTimes = new float[startSpawnTimes.Length];
        iterSpawnTimes = new float[startSpawnTimes.Length];
    }

    void FixedUpdate()
    {
        if (!gameLoopHandler.gameOver)
        {
            UpdateScreenCorners();

            InterpolateSpawnTimes();

            for (int i = 0; i < currentTimes.Length; i++)
            {
                currentTimes[i] += Time.fixedDeltaTime;

                if (currentTimes[i] > iterSpawnTimes[i])
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
                        spawnPoint = new Vector2(Random.Range(-22.0f, 22.0f), Random.Range(-22.0f, 22.0f));
                        spawnParent = tools.transform;
                    }

                    GameObject newInstance = Instantiate(spawnables[i], spawnPoint, Quaternion.identity, spawnParent);

                    Vector3 difference = newInstance.transform.position - player.transform.position;

                    if (difference.magnitude < playerSafety)
                    {
                        newInstance.transform.position += difference * playerSafety;
                    }

                    currentTimes[i] = 0.0f;
                }
            }
        }
    }

    void UpdateScreenCorners()
    {
        bottomleft = mainCamera.ScreenToWorldPoint(new Vector2(-screenSafety, -screenSafety));
        upperleft = mainCamera.ScreenToWorldPoint(new Vector2(-screenSafety, Screen.height + screenSafety));
        bottomright = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width + screenSafety, -screenSafety));
        upperright = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width + screenSafety, Screen.height + screenSafety));
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

    void InterpolateSpawnTimes()
    {
        for (int i = 0; i < iterSpawnTimes.Length; i++)
        {
            iterSpawnTimes[i] = Mathf.Lerp(startSpawnTimes[i], endSpawnTimes[i], Mathf.Clamp(gameLoopHandler.gameTime / gameLoopHandler.maxDifficulty, 0.0f, 1.0f));
        }
    }
}
