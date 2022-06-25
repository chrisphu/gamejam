using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawnerController : MonoBehaviour
{
    public GameObject[] shieldBoxes;
    public GameObject shieldBox;
    GameObject player;
    int frameCounter;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(shieldBox, new Vector2(2, 2), Quaternion.identity);
        shieldBoxes.SetValue(shieldBox, shieldBoxes.Length);
        frameCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        frameCounter++;
        if (frameCounter % 300 == 0)
        {
            Instantiate(
                shieldBox, 
                new Vector2(
                    Random.Range(player.transform.position.x + 5, player.transform.position.y + 8), 
                    Random.Range(player.transform.position.x - 7, player.transform.position.y + 8)), 
                Quaternion.identity);

            shieldBoxes.SetValue(shieldBox, shieldBoxes.Length);
            frameCounter = 0;
        }
        if (frameCounter % 300 == 150)
        {
            Instantiate(
                shieldBoxes[shieldBoxes.Length], 
                new Vector2(
                    Random.Range(player.transform.position.x - 7, player.transform.position.y + 8), 
                    Random.Range(player.transform.position.x + 5, player.transform.position.y + 8)), 
                Quaternion.identity);

            shieldBoxes.SetValue(shieldBox, shieldBoxes.Length);
        }
    }
}
