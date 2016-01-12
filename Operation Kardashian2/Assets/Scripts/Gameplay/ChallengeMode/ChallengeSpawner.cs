using UnityEngine;
using System.Collections;

public class ChallengeSpawner : MonoBehaviour
{
    public GameObject[] easyPresets;
    // Use this for initialization
    void Start()
    {

    }

    public float timeBetweenSpawns;
    float spawnTimer;
    public bool canSpawnEasy;
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (timeBetweenSpawns < spawnTimer)
        {
            spawnTimer = 0;
            if (canSpawnEasy)
            {
                Instantiate(easyPresets[Random.Range(0, easyPresets.Length)], TopOfScreen(), Quaternion.identity);
            }
        }
    }

    Vector3 TopOfScreen()
    {
        return new Vector3(Camera.main.transform.position.x, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, -1 * Camera.main.transform.position.z)).y, 0);
    }
}
