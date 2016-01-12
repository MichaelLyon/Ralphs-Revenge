using UnityEngine;
using System.Collections;

public enum EnemyShootType
{
    Normal,
    DoubleShot,
    TrippleShot,
    QuadShot,
    Burst,
    Mine,
    Stall
}
// allows the designers to pick between some directions of shooting
public enum BulletDirection
{
    N, E, S, W, NE, SE, NW, SW, ToPlayer
}

public class BaseShootingEnemy : BaseEnemy
{
    public int? testVar;
    public BulletDirection bulletDirection;
    protected Vector3 fireDirection;
    public float bulletPowerRate;
    public float bulletSpeed;
    public int bulletDamage;
    public GameObject enemyBullet;
    protected GameObject gameManager;
    protected BulletManager bulletManager;
    public BulletPower power;
    protected float shootCooldown = 0;
    public float timeBetweenShots = .2f;
    float burstCooldown = 0;
    public float timeBetweenBursts = 2;
    int shotsFired;
    public int bulletsInBurst = 3;
    public EnemyShootType shootType;
    protected GameObject player;
    public GameObject shootOrigen;
    public override void Start()
    {

        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager");
        bulletManager = gameManager.GetComponent<BulletManager>();
        if (bulletDirection == BulletDirection.N)
        {
            fireDirection = Vector3.up;
        }
        if (bulletDirection == BulletDirection.S)
        {
            fireDirection = Vector3.down;
        }
        if (bulletDirection == BulletDirection.E)
        {
            fireDirection = Vector3.right;
        }
        if (bulletDirection == BulletDirection.W)
        {
            fireDirection = Vector3.left;
        }
        if (bulletDirection == BulletDirection.NE)
        {
            fireDirection = Vector3.Normalize(Vector3.up + Vector3.right);
        }
        if (bulletDirection == BulletDirection.NW)
        {
            fireDirection = Vector3.Normalize(Vector3.up + Vector3.left);
        }
        if (bulletDirection == BulletDirection.SW)
        {
            fireDirection = Vector3.Normalize(Vector3.down + Vector3.left);
        }
        if (bulletDirection == BulletDirection.SE)
        {
            fireDirection = Vector3.Normalize(Vector3.down + Vector3.right);
        }
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (alive)
        {
            if (shootOrigen != null)
                shootOrigen.transform.up = fireDirection;
            if (bulletDirection == BulletDirection.ToPlayer)
            {
                if (player != null)
                    fireDirection = Vector3.Normalize(new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z) - transform.position);
            }
            shootCooldown += 1 * Time.deltaTime;
            if (shootCooldown >= timeBetweenShots)
            {
                if (shootType == EnemyShootType.Normal)
                {

                    Shoot(fireDirection, this.transform.position, bulletSpeed, bulletDamage, power, bulletPowerRate);

                }
                if (shootType == EnemyShootType.DoubleShot)
                {
                    DoubleShot();
                }
                if (shootType == EnemyShootType.TrippleShot)
                {
                    TripleShot();
                }
                if (shootType == EnemyShootType.QuadShot)
                {
                    QuadShot();
                }
                if (shootType == EnemyShootType.Mine)
                {
                    Shoot(Vector3.zero, this.transform.position, 0f, bulletDamage, power, bulletPowerRate);
                }
            }
            if (shootType == EnemyShootType.Burst)
            {
                burstCooldown += 1 * Time.deltaTime;
                // if (burstCooldown > 0) burstCooldown -= 1 * Time.deltaTime;
                if (shootCooldown >= timeBetweenShots && burstCooldown >= timeBetweenBursts)
                {
                    // timeBetweenShotsInBurst -= 1 * Time.deltaTime;
                    if (player != null)
                        Shoot(/*((player.transform.position + player.transform.up) - transform.position).normalized*/fireDirection, this.transform.position, bulletSpeed, bulletDamage, power, bulletPowerRate);
                    //timeBetweenShotsInBurst = burstShotCooldown;
                    shotsFired++;
                    shootCooldown = 0;
                }
                if (shotsFired == bulletsInBurst)
                {
                    shotsFired = 0;
                    burstCooldown = 0;
                    //shootTimer = timeBetweenShots;
                    //burstCooldown = timeBetweenBursts;
                }
            }
        }
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
    }
    public GameObject MakeBullet()
    {
        GameObject bullet = (GameObject)GameObject.Instantiate(this.enemyBullet);

        bullet.transform.localScale *= Camera.main.aspect;
        // bullet.transform.position = new Vector3(bullet.transform.position.x * Camera.main.aspect, bullet.transform.position.y * Camera.main.aspect, bullet.transform.position.z);

        bulletManager.enemyBullets.Add(bullet);
        // Debug.Log("made a bullet");
        return bullet;
    }
    public void Shoot(Vector3 direction, Vector3 position, float speed, int damage, BulletPower power, float powerRate)
    {
        GameObject enemyBullet = GetBullet();
        if (enemyBullet == null)
        {
            enemyBullet = MakeBullet();

        }
        enemyBullet.SetActive(true);
        enemyBullet.transform.position = position;
        EnemyBullet bulletScript = enemyBullet.GetComponent<EnemyBullet>();
        bulletScript.BecomeAlive();
        bulletScript.direction = direction;
        //enemyBullet.transform.position += direction*.2f;
        bulletScript.bulletPower = power;
        bulletScript.damage = damage;
        bulletScript.speed = bulletSpeed;
        bulletScript.powerRate = powerRate;
        shootCooldown = 0;


    }
    public void Shoot(Vector3 direction, Vector3 position, int damage, BulletPower power, BulletPower childBulletPower, float childBulletPowerRate, float childBulletSpeed)
    {
        GameObject enemyBullet = GetBullet();
        if (enemyBullet == null)
        {
            enemyBullet = MakeBullet();
        }
        enemyBullet.SetActive(true);
        enemyBullet.transform.position = position;
        EnemyBullet bulletScript = enemyBullet.GetComponent<EnemyBullet>();
        bulletScript.BecomeAlive();
        bulletScript.direction = direction;
        bulletScript.bulletPower = power;
        bulletScript.damage = damage;
        bulletScript.speed = childBulletSpeed;
        bulletScript.powerRate = childBulletPowerRate;
        bulletScript.childBulletPower = childBulletPower;
        //bulletScript.
        shootCooldown = 0;


    }


    public void DoubleShot()
    {
        // collider.bounds.
        float x = transform.position.x;
        Vector3 pos = new Vector3(x + .5f, transform.position.y, transform.position.z);
        Shoot(fireDirection, pos, bulletSpeed, bulletDamage, power, bulletPowerRate);
        //direction = new Vector3(transform.forward,transform.forward.y,transform.forward.z);
        pos = new Vector3(x - .5f, transform.position.y, transform.position.z);
        Shoot(fireDirection, pos, bulletSpeed, bulletDamage, power, bulletPowerRate);

    }
    public void TripleShot()
    {
        Shoot(fireDirection, this.transform.position, bulletSpeed, bulletDamage, power, bulletPowerRate);
        Vector3 direction = Vector3.Normalize(Vector3.RotateTowards(fireDirection, Vector3.Normalize(fireDirection + Vector3.Cross(fireDirection, Vector3.forward)), .4f, 1));
        Shoot(direction, this.transform.position, bulletSpeed, bulletDamage, power, bulletPowerRate);
        direction = Vector3.Normalize(Vector3.RotateTowards(fireDirection, Vector3.Normalize(fireDirection + Vector3.Cross(Vector3.forward, fireDirection)), .4f, 1));
        Shoot(direction, this.transform.position, bulletSpeed, bulletDamage, power, bulletPowerRate);

    }
    public void QuadShot()
    {
        Vector3 direction = Vector3.Normalize(Vector3.RotateTowards(fireDirection, Vector3.Normalize(fireDirection + Vector3.Cross(fireDirection, Vector3.forward)), .1f, 1));
        Shoot(direction, this.transform.position, bulletSpeed, bulletDamage, power, bulletPowerRate);
        direction = Vector3.Normalize(Vector3.RotateTowards(fireDirection, Vector3.Normalize(fireDirection + Vector3.Cross(fireDirection, Vector3.forward)), .3f, 1));
        Shoot(direction, this.transform.position, bulletSpeed, bulletDamage, power, bulletPowerRate);
        direction = Vector3.Normalize(Vector3.RotateTowards(fireDirection, Vector3.Normalize(fireDirection + Vector3.Cross(Vector3.forward, fireDirection)), .1f, 1));
        Shoot(direction, this.transform.position, bulletSpeed, bulletDamage, power, bulletPowerRate);
        direction = Vector3.Normalize(Vector3.RotateTowards(fireDirection, Vector3.Normalize(fireDirection + Vector3.Cross(Vector3.forward, fireDirection)), .3f, 1));
        Shoot(direction, this.transform.position, bulletSpeed, bulletDamage, power, bulletPowerRate);
    }
    public override void BecomeActive()
    {
        shootCooldown = 0;
        burstCooldown = timeBetweenBursts;

        base.BecomeActive();
    }
}
