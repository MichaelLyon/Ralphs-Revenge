using UnityEngine;
using System.Collections;

public class BaseShootingLevelEnemy : BaseLevelEnemy
{

    // Use this for initialization
    public GameObject enemyBullet;
    GameObject gameManager;
    BulletManager bulletManager;
    public void Start()
    {
        gameManager = GameObject.Find("Game Manager");
        bulletManager = gameManager.GetComponent<BulletManager>();
        base.Start();
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();

    }
    public GameObject GetBullet()
    {
        foreach (GameObject enemyBullet in bulletManager.enemyBullets)
        {
            if (enemyBullet.activeSelf == false)
            {
                return enemyBullet;
            }
        }
        return null;
        /* for (int i = 0; i < bulletManager.enemyBullets.Count; i++)
         {
             if (bulletManager.enemyBullets[i].activeInHierarchy == false)
             {
                 return bulletManager.enemyBullets[i];
             }
         }
         return null;*/
    }
    public GameObject MakeBullet()
    {
        GameObject bullet = (GameObject)GameObject.Instantiate(this.enemyBullet);
        bulletManager.enemyBullets.Add(bullet);
        // Debug.Log("made a bullet");
        return bullet;
    }
    public void Shoot(Vector3 direction, Vector3 position)
    {
        GameObject enemyBullet = GetBullet();
        if (enemyBullet == null)
        {
            enemyBullet = MakeBullet();
        }
        enemyBullet.SetActive(true);
        enemyBullet.transform.position = position;
        EnemyBullet bulletScript = enemyBullet.GetComponent<EnemyBullet>();
        bulletScript.direction = direction;

    }
}
