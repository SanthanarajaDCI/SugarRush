using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject foodPrefab; // object to spawn
    [SerializeField] GameObject goldenPrefab; // object to spawn
    [SerializeField] float spawnPeriod = 4f; // base spawn period for food
    [SerializeField] float goldenSpawnPeriod = 22f; // base spawn period for golden eggs
    [SerializeField] float foodVelocity = -2f; // fall speed for food
    [SerializeField] float goldenVelocity = -1f; // constant slow fall speed for golden eggs
    [SerializeField] float spawnXMin = -8f; // minimum X position for spawning
    [SerializeField] float spawnXMax = 8f; // maximum X position for spawning
    [SerializeField] float goldenSpawnXMin = -6f; // minimum X position for golden eggs
    [SerializeField] float goldenSpawnXMax = 6f; // maximum X position for golden eggs

    void Start()
    {
        StartCoroutine(SpawnContinuously());
        StartCoroutine(GoldenSpawnContinuously());
    }

    IEnumerator SpawnContinuously()
    {
        while (true)
        {
            float randomSpawnPeriod = spawnPeriod + Random.Range(-1f, 1f); // add randomness to spawn period
            yield return new WaitForSeconds(randomSpawnPeriod);
            SpawnObject(foodPrefab, foodVelocity, spawnXMin, spawnXMax);
        }
    }

    IEnumerator GoldenSpawnContinuously()
    {
        while (true)
        {
            float randomGoldenSpawnPeriod = goldenSpawnPeriod + Random.Range(-2f, 2f); // add randomness to golden spawn period
            yield return new WaitForSeconds(randomGoldenSpawnPeriod);
            SpawnObject(goldenPrefab, goldenVelocity, goldenSpawnXMin, goldenSpawnXMax);
        }
    }

    void SpawnObject(GameObject prefab, float fallSpeed, float xMin, float xMax)
    {
        Vector3 pos = new Vector3(Random.Range(xMin, xMax), 6.5f, 0);
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity);
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, fallSpeed); // Set constant fall speed
        rb.gravityScale = 0; // Ensure gravity does not affect the fall speed
    }
}




// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Spawner : MonoBehaviour
// {
//     [SerializeField] GameObject foodPrefab; //object to spawn
//     [SerializeField] GameObject goldenPrefab; //object to spawn
//     [SerializeField] float spawnPeriod = 4f; //spawn period
//     [SerializeField] float goldenspawnPeriod = 20f; //spawn period
//     [SerializeField] float velocity = -1f; //velocity to bottom which makes game harder
//     [SerializeField] float periodAdder = 0.1f; //add more fruits every sec

//     // [SerializeField] Sprite[] sprites; //diffrent sprites for foods

//     float xMin; //min pos to spawn
//     float xMax; //max pos to spawn
//     void Start()
//     {
//         Camera gameCamera = Camera.main; //assign main camera to var
//         xMin = gameCamera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f)).x;
//         xMax = gameCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x;
//         StartCoroutine(SpawnContinuously());
//         StartCoroutine(goldenSpawnContinuously());
//        // StartCoroutine(PeriodAddEverySec());

//     }

//     // Update is called once per frame
//     void Update()
//     {
//     }

//     IEnumerator SpawnContinuously()
//     {
//         while (true) //make spawning endless
//         {
//             yield return new WaitForSeconds(5f);
//             Vector3 pos = new Vector3(Random.Range(-8f, 8f), 6.5f, 0);
//             GameObject food = Instantiate(foodPrefab, pos, Quaternion.identity);
//             food.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocity); // Set velocity

//             // var spawnPosX = Random.Range(xMin, xMax); //pos to spawn randomized beetwen limits
//             // GameObject food = Instantiate(foodPrefab, new Vector2(spawnPosX, 7f), Quaternion.identity); //spawning food object
//             // // food.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)]; //chang sprite to random from array
//             // // food.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocity); //add velocity//add velocity
//             // yield return new WaitForSeconds(spawnPeriod); //wait until new spawn
//         }
//     }

//     IEnumerator goldenSpawnContinuously()
//     {
//         while (true) //make spawning endless
//         {
//             yield return new WaitForSeconds(28f);
//             Vector3 pos = new Vector3(Random.Range(-8f, 8f), 6.5f, 0);
//             GameObject golden = Instantiate(goldenPrefab, pos, Quaternion.identity);
//             golden.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocity); // Set velocity

//             // yield return new WaitForSeconds(28f);
//             // var spawnPosX = Random.Range(xMin, xMax); //pos to spawn randomized beetwen limits
//             // GameObject golden = Instantiate(goldenPrefab, new Vector2(spawnPosX, 7f), Quaternion.identity); //spawning food object
//             // golden.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)]; //chang sprite to random from array
//             // golden.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocity); //add velocity
//         }
//     }

//     IEnumerator PeriodAddEverySec()
//     {
//         while (true) //endless loop
//         {
//             if (spawnPeriod > 0.5f) //if period is greater than 0.5 subtract period
//             {
//                 spawnPeriod = spawnPeriod - spawnPeriod / 50; //alghoritm for decreasing period
//             }
//             else //if not add velocity
//             {
//                 velocity -= 0.1f; //adds negative velocity
//             }
//             yield return new WaitForSeconds(1f); //do it every 1 sec
//         }
//     }
// }
