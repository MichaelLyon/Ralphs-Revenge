using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EntityManager : MonoBehaviour
{
    [HideInInspector]
   /// public EffectManager effectManager;
    public static EntityManager Instance
    {
        get;
        private set;
    }

    void Awake()
    {
        Instance = this;
    }

    public int enemiesInLevel;
    public int enemiesKilled;
  //  CameraMovement camMove;
    void Start()
    {      
        pos = Functions.ScreenPointTopLeft.y;
     //   effectManager = GetComponent<EffectManager>();
        GameObject[] enemyArray;
        enemyArray = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesInLevel = enemyArray.Length;
        foreach (GameObject enemy in enemyArray)
        {
            enemy.SetActive(false);
            BaseEnemy script = enemy.GetComponent<BaseEnemy>();
            script.alive = true;
            enemiesList.Add(enemy);
        }
        GameObject[] playerArray = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] bulletArray = GameObject.FindGameObjectsWithTag("PlayerBullet");
        GameObject[] enemyBulletArray = GameObject.FindGameObjectsWithTag("EnemyBullet");
        GameObject[] scorePowerUp = GameObject.FindGameObjectsWithTag("ScorePowerUp");
        GameObject[] shieldPowerUP = GameObject.FindGameObjectsWithTag("ShieldPowerUp");
        GameObject[] fireRatePowerUp = GameObject.FindGameObjectsWithTag("FireRatePowerUp");
        GameObject[] duplicatePowerUp = GameObject.FindGameObjectsWithTag("DuplicatePowerUp");
        GameObject[] bulletPowerUp = GameObject.FindGameObjectsWithTag("BulletPowerUp");
        GameObject[] allArray = new GameObject[enemyArray.Length + playerArray.Length + bulletArray.Length + enemyBulletArray.Length + scorePowerUp.Length + shieldPowerUP.Length
            + fireRatePowerUp.Length + duplicatePowerUp.Length + bulletPowerUp.Length];
        enemyArray.CopyTo(allArray, 0);
        playerArray.CopyTo(allArray, enemyArray.Length);
        bulletArray.CopyTo(allArray, enemyArray.Length + playerArray.Length);
        enemyBulletArray.CopyTo(allArray, enemyArray.Length + playerArray.Length + bulletArray.Length);
        scorePowerUp.CopyTo(allArray, enemyArray.Length + playerArray.Length + bulletArray.Length + enemyBulletArray.Length);
        shieldPowerUP.CopyTo(allArray, enemyArray.Length + playerArray.Length + bulletArray.Length + enemyBulletArray.Length + scorePowerUp.Length);
        fireRatePowerUp.CopyTo(allArray, enemyArray.Length + playerArray.Length + bulletArray.Length + enemyBulletArray.Length + scorePowerUp.Length + shieldPowerUP.Length);
        duplicatePowerUp.CopyTo(allArray, enemyArray.Length + playerArray.Length + bulletArray.Length + enemyBulletArray.Length + scorePowerUp.Length + shieldPowerUP.Length + fireRatePowerUp.Length);
        bulletPowerUp.CopyTo(allArray, enemyArray.Length + playerArray.Length + bulletArray.Length + enemyBulletArray.Length + scorePowerUp.Length + shieldPowerUP.Length + fireRatePowerUp.Length + duplicatePowerUp.Length);

        foreach (GameObject entity in allArray)
        {
            entity.transform.localScale *= Camera.main.aspect;
            entity.transform.position = new Vector3(entity.transform.position.x * Camera.main.aspect, entity.transform.position.y, entity.transform.position.z);
        }

    }

    [HideInInspector]
    public List<GameObject> enemiesList = new List<GameObject>();
    float pos;
    void Update()
    {
     //   pos += camMove.speed * Time.deltaTime;
        for (int i = enemiesList.Count - 1; i >= 0; i--)
        {
            if (enemiesList[i].activeSelf == false && enemiesList[i].transform.position.y <= Camera.main.transform.position.y + 7)
            {
                enemiesList[i].SetActive(true);
                enemiesList[i].SendMessage("BecomeActive");
                enemiesList.RemoveAt(i);
            }
        }
    }
}
