using UnityEngine;
using System.Collections;


public class PlayerCollisions : MonoBehaviour
{
    public int hitPoints = 5;
    public SpriteRenderer renderer;
    PlayerShooting playerShootingScript;
    UIScore score;
    // Use this for initialization
    void Start()
    {
        for (int i = PlayerPrefs.GetInt("Duplicate") - 1; i > 0; --i)
        {
            CreateDuplicate();
        }
        playerShootingScript = GetComponent<PlayerShooting>();
        lives = GameObject.Find("Lives").GetComponent<UILives>(); //eww
        score = GameObject.Find("PlayerInfo").GetComponent<UIScore>(); //double eww
    }

    // Update is called once per frame
    void Update()
    {
        if (shield)
        {
            renderer.color = Color.blue;
            shdur -= Time.deltaTime;
            if (shdur <= 0)
            {
                shield = false;
                renderer.color = Color.white;
            }
        }
        if (hitPoints <= 0)
        {
            //Currency Saver
            GameCurrencyControl.control.SaveGold();
            Application.LoadLevel("MainMenu");
            gameObject.SetActive(false);
        }

    }

    /// <summary>
    /// Duplicate Power Up
    /// </summary>
    GameObject duplicate;
    void CreateDuplicate() //TODO: Make this not cancer
    {
        int dupCount = GameObject.FindGameObjectsWithTag("Player").Length - 1;
        if (dupCount < 2)
        {
            duplicate = (GameObject)GameObject.Instantiate(this.gameObject);
            duplicate.GetComponent<PlayerMovement>().enabled = false;
            duplicate.GetComponent<PlayerCollisions>().enabled = false;
            Destroy(duplicate.GetComponent<PlayerMovement>());
            Destroy(duplicate.GetComponent<PlayerCollisions>());
            duplicate.AddComponent<DuplicateMovement>();
            duplicate.AddComponent<DuplicateCollision>();
            DuplicateMovement dupMove = duplicate.GetComponent<DuplicateMovement>();
            if (dupCount == 0)
            {
                duplicate.transform.position = this.gameObject.transform.position + Vector3.right;
                dupMove.position = Vector3.right;
            }
            else if (dupCount == 1)
            {
                duplicate.transform.position = this.gameObject.transform.position + Vector3.left;
                dupMove.position = Vector3.left;
            }
        }
    }

    /// <summary>
    /// Shield Power Up Duration
    /// </summary>
    public float shieldPowerUpDuration;
    public float justHitShieldDuration;
    float shdur;
    [HideInInspector]
    public bool shield;
    void ShieldUp(float duration)
    {
        shield = true;
        shdur = duration;
    }

    UILives lives;
    //TODO: Make this not cancer
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Enemy")
        {
            if (!shield)
            {
                if (other.gameObject.tag == "EnemyBullet")
                {
                    other.gameObject.SetActive(false);
                    EnemyBullet bulletScript = other.gameObject.GetComponent<EnemyBullet>();
                    hitPoints -= (int)bulletScript.damage;
                }
                else
                {
                    hitPoints--;
                }
                lives.CheckLives();
                ShieldUp(justHitShieldDuration);
            }
        }
        else if (other.gameObject.tag == "BulletPowerUp")
        {
            PlayerShooting playerShooting = this.gameObject.GetComponent<PlayerShooting>();
            if (playerShooting.shootType == ShootType.normalShot)
            {
                playerShooting.shootType = ShootType.doubleShot;
            }
            else if (playerShooting.shootType == ShootType.doubleShot)
            {
                playerShooting.shootType = ShootType.tripleShot;
            }
            else if (playerShooting.shootType == ShootType.tripleShot)
            {
                playerShooting.shootType = ShootType.quadShot;
            }
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "DuplicatePowerUp")
        {
            CreateDuplicate();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Environment")
        {
            hitPoints--;
            ShieldUp(justHitShieldDuration);
        }
        else if (other.gameObject.tag == "Win")
        {
            //LEVEL ENDED
        }
        else if (other.gameObject.tag == "ShieldPowerUp")
        {
            ShieldUp(shieldPowerUpDuration);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "FireRatePowerUp")
        {
            playerShootingScript.shootCooldown -= playerShootingScript.shotSpeedIncreasePerLevel;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "ScorePowerUp")
        {
            score.scoreMultiplier *= 2;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "LifePowerUp")
        {
            hitPoints++;
            lives.AddLife();
            Destroy(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "WaterPool")
        {

            if (this.GetComponent<PlayerShooting>().waterLevel < 1)
            {
                this.GetComponent<PlayerShooting>().waterLevel += Time.deltaTime;
            }
        }
    }
}
