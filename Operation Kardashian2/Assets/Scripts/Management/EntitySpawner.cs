using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EntitySpawner : MonoBehaviour
{
    public Dictionary<GameObject, int> spawnables = new Dictionary<GameObject, int>();
    public GameObject meleeEnemy;
    public GameObject shootingEnemy;
    List<GameObject> meleeEnemies = new List<GameObject>();
    List<GameObject> shootingEnemies = new List<GameObject>();
    // Use this for initialization
    float spawnTimer;
    GameObject gameManager;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        /*  for( int i = 0; i< 4; i++)
          {
              GameObject meleEnemy = (GameObject)GameObject.Instantiate(this.meleEnemy);
              meleEnemies.Add(meleEnemy);
              meleEnemy.SetActive(false);
              GameObject shootingEnemy = (GameObject)GameObject.Instantiate(this.shootingEnemy);
              shootingEnemies.Add(shootingEnemy);
              shootingEnemy.SetActive(false);
          }*/
    }

    // Update is called once per frame
    public float meleeSpawnTime;
    public float shootingSpawnTime;
    public float spawnTimeDecreasePerSecond;
    float spawnTimeDecrease;
    void Update()
    {
        OnSecond();

        spawnTimer++;
        if (Mathf.RoundToInt(spawnTimer) % (shootingSpawnTime - spawnTimeDecrease) == 0)
        {
            GameObject shootingEnemy = (GameObject)GameObject.Instantiate(this.shootingEnemy);
            shootingEnemy.transform.parent = gameManager.transform;
        }
        if (Mathf.RoundToInt(spawnTimer) % (meleeSpawnTime - spawnTimeDecrease) == 0)
        {
            GameObject meleEnemy = (GameObject)GameObject.Instantiate(this.meleeEnemy);
            meleEnemy.transform.parent = gameManager.transform;
        }
    }

    float milliseconds;
    void OnSecond()
    {
        milliseconds += Time.deltaTime;
        if (milliseconds >= 60)
        {
            milliseconds = 0;
            //Once per second logic here
            spawnTimeDecreasePerSecond += spawnTimeDecreasePerSecond;
        }
    }
}
