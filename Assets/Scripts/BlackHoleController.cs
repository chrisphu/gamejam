using UnityEngine;

public class BlackHoleController : MonoBehaviour
{
    // public GameObject enemySpawnController;
    GameObject enemies;
    GameObject tools;
    float eventHorizonRadius = 3.0f / 2.0f * 1.5f;
    int state = 0;  // 0 = grow when first in scene, 1 = stable life, 2 = shrink and disappear
    float state0Lifespan = 1.0f;
    float state0Age = 0.0f;
    float state1Lifespan = 20.0f;
    float state1Age = 0.0f;
    float state2Lifespan = 1.0f;
    float state2Age = 0.0f;
    ValidTargetHandler validTargetHandler;
    AudioSource audioSource;

    void Start()
    {
        transform.localScale = new Vector2(0.0f, 0.0f);
        enemies = GameObject.FindGameObjectWithTag("Enemies");
        tools = GameObject.FindGameObjectWithTag("Tools");
        validTargetHandler = GameObject.FindGameObjectWithTag("ValidTargetHandler").GetComponent<ValidTargetHandler>();
        audioSource = transform.GetComponent<AudioSource>();

        /*
        GameObject newSpawner = Instantiate(enemySpawnController,
            transform.position + new Vector3(7.5f, 0.0f, 0.0f),
            Quaternion.identity,
            enemies.transform);
        newSpawner.GetComponent<EnemySpawnController>().pointOfRotation = gameObject;
        */
    }

    void FixedUpdate()
    {
        if (state == 0)
        {
            if (state0Age > state0Lifespan)
            {
                transform.localScale = new Vector2(3.0f, 3.0f);
                state = 1;
            }
            else
            {
                transform.localScale = Vector2.Lerp(new Vector2(0.0f, 0.0f), new Vector2(3.0f, 3.0f), state0Age / state0Lifespan);
                state0Age += Time.fixedDeltaTime;
            }
        }
        else if (state == 1)
        {
            foreach (Transform enemy in enemies.GetComponentInChildren<Transform>())
            {
                if (enemy.GetComponent<WandererController>() is WandererController wandererController)
                {
                    float distance = (enemy.transform.position - transform.position).magnitude;

                    if (distance < eventHorizonRadius)
                    {
                        if (!wandererController.externalControl)
                        {
                            audioSource.Play();
                            wandererController.externalControl = true;
                            wandererController.GetComponent<Collider2D>().enabled = false;

                            LerpTowardsThenDieAction assignedAction = wandererController.gameObject.AddComponent<LerpTowardsThenDieAction>();
                            assignedAction.lerpTowards = gameObject;
                        }
                    }
                }

                else if (enemy.GetComponent<EnemySpawnController>() is EnemySpawnController enemySpawnerController)
                {
                    float distance = (enemy.transform.position - transform.position).magnitude;

                    if (distance < eventHorizonRadius)
                    {
                        if (!enemySpawnerController.externalControl)
                        {
                            audioSource.Play();
                            enemySpawnerController.externalControl = true;
                            enemySpawnerController.GetComponent<Collider2D>().enabled = false;

                            LerpTowardsThenDieAction assignedAction = enemySpawnerController.gameObject.AddComponent<LerpTowardsThenDieAction>();
                            assignedAction.lerpTowards = gameObject;
                        }
                    }
                }
            }

            foreach (Transform tool in tools.GetComponentInChildren<Transform>())
            {
                if (tool.GetComponent<TNTController>() is TNTController tntController)
                {
                    float distance = (tool.transform.position - transform.position).magnitude;

                    if (distance < eventHorizonRadius)
                    {
                        if (!tntController.externalControl)
                        {
                            audioSource.Play();
                            tntController.externalControl = true;
                            tntController.GetComponent<Collider2D>().enabled = false;

                            LerpTowardsThenDieAction assignedAction = tntController.gameObject.AddComponent<LerpTowardsThenDieAction>();
                            assignedAction.lerpTowards = gameObject;
                        }
                    }
                }
                else if (tool.GetComponent<BowlingBallController>() is BowlingBallController bowlingBallController)
                {
                    float distance = (tool.transform.position - transform.position).magnitude;

                    if (distance < eventHorizonRadius)
                    {
                        if (!bowlingBallController.externalControl)
                        {
                            audioSource.Play();
                            bowlingBallController.externalControl = true;
                            bowlingBallController.GetComponent<Collider2D>().enabled = false;

                            LerpTowardsThenDieAction assignedAction = bowlingBallController.gameObject.AddComponent<LerpTowardsThenDieAction>();
                            assignedAction.lerpTowards = gameObject;
                        }
                    }
                }
            }

            if (state1Age > state1Lifespan)
            {
                state = 2;
            }
            else
            {
                state1Age += Time.fixedDeltaTime;
            }
        }
        else
        {
            if (state2Age > state2Lifespan)
            {
                transform.localScale = new Vector2(0.0f, 0.0f);
                validTargetHandler.DestroyObjAndJoints(gameObject);
            }
            else
            {
                transform.localScale = Vector2.Lerp(new Vector2(3.0f, 3.0f), new Vector2(0.0f, 0.0f), state2Age / state2Lifespan);
                state2Age += Time.fixedDeltaTime;
            }
        }
    }
}
