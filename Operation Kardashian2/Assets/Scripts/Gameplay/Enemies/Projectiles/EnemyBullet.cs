using UnityEngine;
using System.Collections;

public enum BulletPower
{
    normal,
    Branch,
    ZigZag,
    Pop,
    Missile
    ,Pop2
}
public class EnemyBullet : MonoBehaviour
{
    public float maxTimeOnScreen;
    [HideInInspector]
    float timer;
    GameObject player;
    public BulletPower childBulletPower;
    public float childBulletPowerRate;
    public float childBulletSpeed;
    /// <summary>
    /// power rate is how often the power occurs
    /// pop: time before the bullets explode
    /// sign: the rate at which the x changes
    /// branch: time between branches
    /// </summary>
    public float powerRate = 1;
    int maxSpeed;

    /// <summary>
    /// represents the size of the change in x if sign is the pullet power
    /// </summary>
    public float maxChangeInX = 1.5f;
    public Vector3 direction;
    public float speed;
    // float startSpeed;
    public GameObject enemyBullet;
    int shiftingLeft = 1; //1 for true -1 for false
    Vector3 velocity;
    public float damage;
    float initialXValue;
    public BulletPower bulletPower;
    GameObject bulletManager;
    BulletManager bulletManagerScript;
    // Use this for initialization
    void Start()
    {
        // startSpeed = speed;
        player = GameObject.Find("Player");
        initialXValue = transform.position.x;
        timer = 0;
        bulletManager = GameObject.Find("Game Manager");
        bulletManagerScript = bulletManager.GetComponent<BulletManager>();
        transform.parent = bulletManager.transform;
        maxSpeed = (int)speed * 3;
    }

    // Update is called once per frame
    public void Update()
    {
        maxTimeOnScreen += Time.deltaTime;
        if(maxTimeOnScreen>=3)
        {
            gameObject.SetActive(false);
        }
        timer +=  Time.deltaTime;
        if (bulletPower == BulletPower.Branch)
        {

            if (timer >= powerRate)
            {
                Branch();
                timer = 0;
            }
        }
        if (bulletPower == BulletPower.Pop)
        {
            if (timer >= powerRate)
            {
                Pop();
            }
        }
        if (bulletPower == BulletPower.Pop2)
        {
            if (timer >= powerRate)
            {
                Pop(BulletPower.Branch,4f,.5f);
            }
        }
        if (bulletPower == BulletPower.ZigZag)
        {
            if (Mathf.Abs(transform.position.x - initialXValue) > maxChangeInX)
            {
                shiftingLeft *= -1;
            }
            if (shiftingLeft > 0)
            {
                transform.position = new Vector3(transform.position.x - powerRate, transform.position.y, transform.position.z);
            }
            else transform.position = new Vector3(transform.position.x + powerRate, transform.position.y, transform.position.z);

            // if(time)
        }
        if (bulletPower == BulletPower.Missile)
        {
            if (transform.position.y > player.transform.position.y)
            {
                direction = Vector3.RotateTowards(direction, Vector3.Normalize(player.transform.position - transform.position), .02f, 1);
            }
            if (speed < maxSpeed) speed *= 1.1f;
            if (speed > maxSpeed) speed = maxSpeed;

        }

        velocity = direction * speed * Time.deltaTime;
        transform.position += velocity;
        if
            (
          transform.position.x > Functions.ScreenPointTopRight.x
        || transform.position.y > Functions.ScreenPointTopRight.y
        || transform.position.x < Functions.ScreenPointBotLeft.x
        || transform.position.y < Functions.ScreenPointBotLeft.y
            )
        {
            gameObject.SetActive(false);
        }
    }

    void Branch()
    {
        //   direction = new(Vector3.down);
        // direction = Vector3.down;
        direction = Vector3.Normalize(Vector3.RotateTowards(direction,Vector3.Normalize(Vector3.Cross(Vector3.forward,direction)), .4f, 1));
        FireBullet(Vector3.Normalize(Vector3.RotateTowards(direction,Vector3.Normalize(Vector3.Cross(direction,Vector3.forward)), .8f, 1)), this.transform.position, BulletPower.Branch);

    }
    void Pop()
    {
        bulletPower = BulletPower.normal;
        FireBullet(Vector3.Normalize(Vector3.down + Vector3.right), this.transform.position, BulletPower.normal);
        FireBullet(Vector3.Normalize(Vector3.right), this.transform.position, BulletPower.normal);
        FireBullet(Vector3.Normalize(Vector3.right + Vector3.up), this.transform.position, BulletPower.normal);
        FireBullet(Vector3.Normalize(Vector3.up), this.transform.position, BulletPower.normal);
        FireBullet(Vector3.Normalize(Vector3.left + Vector3.up), this.transform.position, BulletPower.normal);
        FireBullet(Vector3.Normalize(Vector3.left), this.transform.position, BulletPower.normal);
        FireBullet(Vector3.Normalize(Vector3.down + Vector3.left), this.transform.position, BulletPower.normal);
    }
    void Pop(BulletPower power,float speed, float powerRate)
    {
        bulletPower = BulletPower.normal;
        FireBullet(Vector3.Normalize(Vector3.down + Vector3.right), this.transform.position,power,speed,powerRate);
        FireBullet(Vector3.Normalize(Vector3.right), this.transform.position, power,speed,powerRate);
        FireBullet(Vector3.Normalize(Vector3.right + Vector3.up), this.transform.position, power,speed,powerRate);
        FireBullet(Vector3.Normalize(Vector3.up), this.transform.position, power,speed,powerRate);
        FireBullet(Vector3.Normalize(Vector3.left + Vector3.up), this.transform.position, power,speed,powerRate);
        FireBullet(Vector3.Normalize(Vector3.left), this.transform.position, power,speed,powerRate);
        FireBullet(Vector3.Normalize(Vector3.down + Vector3.left), this.transform.position, power,speed,powerRate);
    }
  
    void FireBullet(Vector3 direction, Vector3 position, BulletPower power)
    {
        GameObject bullet = GetBullet();
        if (bullet == null)
        {
            bullet = MakeBullet();
        }
        bullet.transform.position = position;
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
        bulletScript.BecomeAlive();
        bulletScript.direction = direction;
        bulletScript.bulletPower = power;
        bulletScript.speed = speed;
        bulletScript.powerRate = powerRate;
        //  bulletScript.direction = Vector3.Normalize(Vector3.RotateTowards(Vector3.down, Vector3.left, .4f, 1));
    }
    void FireBullet(Vector3 direction, Vector3 position, BulletPower power,float speed, float powerRate)
    {
        GameObject bullet = GetBullet();
        if (bullet == null)
        {
            bullet = MakeBullet();
        }
        bullet.transform.position = position;
        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();
        bulletScript.BecomeAlive();
        bulletScript.direction = direction;
        bulletScript.bulletPower = power;
        bulletScript.speed = speed;
        bulletScript.powerRate = powerRate;
        //  bulletScript.direction = Vector3.Normalize(Vector3.RotateTowards(Vector3.down, Vector3.left, .4f, 1));
    }
    GameObject GetBullet()
    {
        foreach (GameObject bullet in bulletManagerScript.enemyBullets)
        {
            if (bullet.activeSelf == false)
            {
                bullet.SetActive(true);
                return bullet;


                // CEnemyBaseBullet enemyBullet = bullet.GetComponent<CEnemyBaseBullet>();
            }
        }
        return null;

    }
    GameObject MakeBullet()
    {
        GameObject enemyBullet = (GameObject)GameObject.Instantiate(this.enemyBullet);

         //   enemyBullet.transform.localScale *= Camera.main.aspect;
        
        bulletManagerScript.enemyBullets.Add(enemyBullet);
        enemyBullet.SetActive(true);
        return enemyBullet;
    }
    public void BecomeAlive()
    {
        // speed = startSpeed;
        maxTimeOnScreen = 0;
        shiftingLeft = 1;
        initialXValue = transform.position.x;
        timer = 0;
        gameObject.SetActive(true);

        // Start();
    }
}
