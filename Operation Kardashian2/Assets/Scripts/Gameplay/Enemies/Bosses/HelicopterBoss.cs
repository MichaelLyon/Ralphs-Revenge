using UnityEngine;
using System.Collections;

public class HelicopterBoss : BaseShootingEnemy
{
    SpriteRenderer renderer;
    // Use this for initialization
    public float missileSpeed = 2;
    public int missileDamage = 2;
    public int damageState = 1; // 1-3
    // int positionDelay;
    bool invulnerable;
    bool swooping = true;
    public float invulTimer = 2;
    float invulTime;
    int stage1HitPoints;
    public int stage2HitPoints = 20;
    public int stage3HitPoints = 20;
    public float timeBetweenActions = 2;
    public float mineSplitTime, mineSplitSpeed;
    float actionTimer;
    int minesDropped;
    float missileCooldown;
    public GameObject mine;

    GameObject miniGun;
    GameObject missileLauncher;

    public override void Start()
    {
        renderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        shootCooldown = timeBetweenShots;
        // bulletManager = GameObject.Find("Game Manager");
        // bulletManagerScript = bulletManager.GetComponent<BulletManager>();
        miniGun = GameObject.Find("Minigun");
        missileLauncher = GameObject.Find("RocketLauncher");
        stage1HitPoints = startingHitPoints;
        player = GameObject.Find("Player");
        // BecomeAlive();
        base.Start();
    }
    public override void OnBecameInvisible()
    {
        //base.OnBecameInvisible();
    }
    // Update is called once per frame
    public override void Update()
    {
        damageFlashCooldown -= Time.deltaTime;
        if (damageFlashCooldown > 0)
            renderer.material.color = Color.red;
        else
            renderer.material.color = Color.white;

        if (stage1HitPoints <= 0 && damageState == 1)
        {
            damageState++;
            invulnerable = true;
            bulletSpeed = 8;
        }
        if (stage2HitPoints <= 0 && damageState == 2)
        {
            damageState++;
            invulnerable = true;
            // bulletSpeed = 6;
            //bulletPowerRate = .5f;
            // bulletDirection = BulletDirection.ToPlayer;
        }
        if (stage3HitPoints <= 0 && damageState == 3)
        {
            //TODO:Make the boss die all cool like
            gameObject.SetActive(false);
            Shoot(Vector3.down, transform.position, 1f, 1, BulletPower.Pop2, .2f);
            power = BulletPower.Pop;
            QuadShot();
            //Shoot(Vector3.down, transform.position, .2f, 1, BulletPower.Pop2, .2f);
            GameObject.Destroy(this.gameObject);
        }
        if (invulnerable) //used to transition back to start position for next attack, boss cannot be shot while this is happening
        {
            centerBossOnScreen(speed);
            if (Vector3.Distance(transform.position, new Vector3(0, Functions.ScreenPointTopLeft.y, 0)) < .2f)
            {
                invulnerable = false;
                // Debug.Log("im here!");
            }
            invulTime += 1 * Time.deltaTime;
            if (invulTime >= invulTimer)
            {
                invulTime = 0;
                invulnerable = false;
            }
        }
        if (damageState == 1)
        {
            // Debug.Log("bombing");
            actionTimer += 1 * Time.deltaTime;
            if (actionTimer >= timeBetweenActions)
            {
                int minesToDrop = Screen.width / 300;
                // Debug.Log(minesToDrop + " mines to drop");

                Vector3 dropPosition = Functions.GetWorldPointFromScreenPoint((int)(300 * (minesDropped + .25f)), Screen.height, 0);
                transform.position = Vector3.MoveTowards(this.transform.position, dropPosition, 3f * Time.deltaTime);

                if (Vector3.Distance(this.transform.position, dropPosition) < 1)
                {

                    Shoot(Vector3.down, this.transform.position, 1, BulletPower.Pop2, BulletPower.Branch, mineSplitTime, mineSplitSpeed);
                    minesDropped++;
                }
                if (minesDropped > minesToDrop)
                {
                    actionTimer = 0;
                    minesDropped = 0;
                }
                //  Debug.Log(minesToDrop);
            }
            else centerBossOnScreen(speed);
        }
        if (damageState == 2 && !invulnerable) //TODO: Swoop And Damage
        {

            // bulletDirection = BulletDirection.ToPlayer;

            // swoopStarted = true;

            actionTimer += 1 * Time.deltaTime;
            if (actionTimer >= timeBetweenActions && !swooping)
            {
                swooping = true;
                direction = player.transform.position - this.transform.position;
                //Shoot()
                QuadShot();
                TripleShot();
            }

            if (swooping)
            {

                if (transform.position.y > player.transform.position.y)
                {
                    direction = Vector3.Normalize(Vector3.RotateTowards(direction, GetSwoopAngle(), .03f, 1));
                }
                speed = 5;
                velocity = direction * speed;
                transform.position += velocity * Time.deltaTime;
            }
            else centerBossOnScreen(speed);

            if (transform.position.y < Functions.ScreenPointBotLeft.y)
            {
                transform.position = new Vector3(0, Functions.ScreenPointTopLeft.y + 1, 0);
                actionTimer = 0;
                swooping = false;
            }



        }
        if (damageState == 3 && !invulnerable)//TODO: go hard in the paint with mini guns.
        {
            bulletSpeed = 4;
            bulletPowerRate = .5f;
            transform.position = new Vector3(transform.position.x, Functions.ScreenPointTopLeft.y, 0);
            if (transform.position.x > player.transform.position.x)
            {
                transform.position += Vector3.left * 2 * Time.deltaTime;
            }
            if (transform.position.x < player.transform.position.x)
            {
                transform.position += Vector3.right * 2 * Time.deltaTime;
            }

            shootCooldown -= 1 * Time.deltaTime;
            if (shootCooldown <= 0)
            {
                Shoot(Vector3.down, miniGun.transform.position, bulletSpeed, bulletDamage, BulletPower.Branch, bulletPowerRate);
                shootCooldown = timeBetweenShots;
            }
            missileCooldown -= 1 * Time.deltaTime;
            if (missileCooldown <= 0)
            {
                Shoot(Vector3.Normalize(player.transform.position - transform.position), missileLauncher.transform.position, missileSpeed, missileDamage, BulletPower.Missile, bulletPowerRate);
                missileCooldown = timeBetweenShots * 5;
            }

        }
        // base.Update();
    }
    public override void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "PlayerBullet" && !invulnerable)
        {
            EffectManager.Instance.waterSplash.transform.position = other.transform.position;
            EffectManager.Instance.waterSplash.Play();
            other.gameObject.SetActive(false);
            damageFlashCooldown = .05f;
            switch (damageState)
            {
                case 1:
                    --stage1HitPoints;
                    break;
                case 2:
                    --stage2HitPoints;
                    break;
                case 3: --stage3HitPoints;
                    break;
            }
        }
        //  base.OnTriggerEnter(other);
    }
    void centerBossOnScreen(float speed)
    {
        Vector3 Destination = new Vector3(0, Functions.ScreenPointTopLeft.y, 0);

        if (Vector3.Distance(transform.position, Destination) < .8)
        {
            Vector3.MoveTowards(transform.position, Destination, this.speed);
        }
        else
            transform.position = Vector3.Lerp(transform.position, Destination, speed * Time.deltaTime);
    }
    Vector3 GetSwoopAngle()
    {
        return direction = player.transform.position - this.transform.position;
    }


}
