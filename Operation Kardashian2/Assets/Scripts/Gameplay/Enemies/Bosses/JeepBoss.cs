using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JeepBoss : BaseShootingEnemy
{
    //  public float timeBetweenGrenadeThrow;
    public Sprite[] textures;
    public List<GameObject> allyTruckPool;
    public int actionsTakenBeforeGrenades;
    public float timeBetweenGrenades;
    public float stage2ShootCooldown;
    int grenadesToThrow;
    int grenadesThrown;
    int actionsTaken;
    public int damageState = 1;
    bool inPosition;
    public GameObject JeepGuard;
    //  float grenadeStateCooldown;
    bool grenadeStateActive;
    bool startedThrowing;
    // bool performingAction;
    Vector3 screenPointRight, screenPointLeft;
    // Use this for initialization
    SpriteRenderer renderer;
    public override void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        hitPoints = startingHitPoints;
        // alive = true;
        // alive = true;
        base.Start();
    }
    bool movingRight;
    public override void OnBecameInvisible()
    {
        //base.OnBecameInvisible();
    }
    public override void OnBecameVisible()
    {
        //base.OnBecameVisible();
    }
    // Update is called once per frame
    public override void Update()
    {
        if (hitPoints <= 0)
        {
            damageState++;
            hitPoints = startingHitPoints;
            if (damageState > 3)
            {
                GameObject.Destroy(this.gameObject);
            }
        }
        if (damageState == 1 && grenadeStateActive == false)
        {
            renderer.sprite = textures[1];
            //TODO: replace the base.update
            //speed = 3;
            screenPointRight = Functions.GetWorldPointFromScreenPoint((int)(Screen.width * .95f), (int)(Screen.height), 0);
            screenPointLeft = Functions.GetWorldPointFromScreenPoint((int)(Screen.width * .05f), (int)(Screen.height), 0);

            float distance1, distance2;
            distance1 = Vector3.Distance(transform.position, screenPointLeft);
            distance2 = Vector3.Distance(transform.position, screenPointRight);
            //    Debug.Log(distance1);
            //   Debug.Log(distance2);
            if (distance1 < .9f)
            {
                movingRight = true;
            }
            if (distance2 < .9f)
            {
                movingRight = false;
                actionsTaken++;
            }
            if (movingRight)
            {
                direction = Vector3.Normalize(screenPointRight - transform.position);
            }
            else
            {
                direction = Vector3.Normalize(screenPointLeft - transform.position);
            }

            transform.position += direction * speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, Functions.ScreenPointTopLeft.y, 0);
            if (actionsTaken > actionsTakenBeforeGrenades * 2)
            {
                grenadeStateActive = true;
                actionsTaken = 0;
            }
            ///strafe back and forth shooting, every now and then, drive up and through grenades(can only be damaged during the grenade state
            base.Update();
        }
        if (damageState == 2 && grenadeStateActive == false)
        {
            renderer.sprite = textures[2];
            shootCooldown += Time.deltaTime;
            if (shootCooldown > stage2ShootCooldown)
            {
                Shoot(Vector3.right, transform.position, bulletSpeed, bulletDamage, BulletPower.normal, bulletPowerRate);
                Shoot(Vector3.left, transform.position, bulletSpeed, bulletDamage, BulletPower.normal, bulletPowerRate);
                shootCooldown = 0;
            }
            transform.position += Vector3.down * Time.deltaTime;
            if (transform.position.y < Functions.ScreenPointBotRight.y)
            {
                transform.position = new Vector3(0, Functions.ScreenPointTopLeft.y + 1, 0);
                actionsTaken++;
            }
            if (actionsTaken > actionsTakenBeforeGrenades)
            {
                grenadeStateActive = true;
                actionsTaken = 0;
            }
            // bulletDirection = Vector3.right;
            ///drive down the screen and shoot to their side of him, if player shoots jeep, he drops a power up that will protect the player for a short period of time
            ///after 1 or 2 passes repeat grenade behavior,

        }
        if (damageState == 3 && grenadeStateActive == false)
        {
            speed = 1f;
            shootCooldown += Time.deltaTime;
            if (shootCooldown > timeBetweenShots * .5f)
            {
                TripleShot();
                // Shoot(Vector3.right, transform.position, bulletSpeed, bulletDamage, BulletPower.normal, bulletPowerRate);
                //  Shoot(Vector3.left, transform.position, bulletSpeed, bulletDamage, BulletPower.normal, bulletPowerRate);
                shootCooldown = 0;
            }
            transform.position += Vector3.down * Time.deltaTime;
            if (transform.position.y < Functions.ScreenPointBotRight.y)
            {

                transform.position = new Vector3(0, Functions.ScreenPointTopLeft.y + 1, 0);
              //  SpawnWave();
                actionsTaken++;
            }
            if (actionsTaken > actionsTakenBeforeGrenades)
            {
                grenadeStateActive = true;
                actionsTaken = 0;
            }
            ///calls allies and creates a wave of jeeps, that come down the screen shooting, player must kill jeeps to create a safe path to move
            ///after he passes return to throwing grenades
        }

        if (grenadeStateActive)
        {
            renderer.sprite = textures[0];
            grenadesToThrow = damageState;
            Vector3 destination = Functions.GetWorldPointFromScreenPoint((int)(Screen.width * .5f), (int)(Screen.height * .6f), 0);
            if (!startedThrowing)
            {

                MoveToPosition(1, destination);
                float dist = Vector3.Distance(destination, transform.position);
               // Debug.Log(dist);
                // Debug.Log(destination);
                if (dist < .5f)
                {
                    startedThrowing = true;
                    // Debug.Log("im here bitch");
                }
            }
            if (startedThrowing)
            {
                transform.position += Vector3.up * 2 * Time.deltaTime;
                // base.Update();
                shootCooldown += Time.deltaTime;
                if (shootCooldown > timeBetweenGrenades && grenadesThrown < grenadesToThrow)
                {
                    shootCooldown = 0;
                    Shoot(Vector3.Normalize(player.transform.position - transform.position), transform.position, 1f, bulletDamage, BulletPower.Pop, bulletPowerRate);
                    //ShootGrenade();
                    grenadesThrown++;
                }
                if (grenadesThrown >= grenadesToThrow)
                {
                    destination = new Vector3(0, Functions.ScreenPointTopLeft.y, 0);
                    MoveToPosition(1, destination);
                    if (Vector3.Distance(destination, transform.position) < .5f)
                    {
                        grenadesThrown = 0;
                        startedThrowing = false;
                        grenadeStateActive = false;
                    }
                }
            }
        }
        // base.Update();
    }
    void centerBossOnScreen(float speed)
    {
        Vector3 Destination = Functions.GetWorldPointFromScreenPoint((int)(Screen.width * .5f), (int)(Screen.height * .1f), 0);

        if (Vector3.Distance(transform.position, Destination) <= 1.5f)
        {
            transform.position += Vector3.Normalize(Destination - transform.position) * speed;
            //Vector3.MoveTowards(transform.position, Destination, this.speed);
        }
        else
            transform.position = Vector3.Lerp(transform.position, Destination, speed * Time.deltaTime);
    }

    void MoveToPosition(float speed, Vector3 Destination)
    {
        // Destination = Functions.GetWorldPointFromScreenPoint((int)(Screen.width * .5f), (int)(Screen.height * .6f), 0);

        if (Vector3.Distance(transform.position, Destination) <= 1.3f)
        {
            transform.position += Vector3.Normalize(Destination - transform.position) * speed * 2 * Time.deltaTime;
            //Vector3.MoveTowards(transform.position, Destination, this.speed);
        }
        else
            transform.position = Vector3.Lerp(transform.position, Destination, speed * Time.deltaTime);
    }
    void ShootGrenade()
    {
        Shoot(Vector3.Normalize(player.transform.position - transform.position), player.transform.position, 2f, bulletDamage, BulletPower.Pop, bulletPowerRate);
    }
    void SpawnWave()
    {
        //TODO FIX THISSS
        GameObject jeep = (GameObject)GameObject.Instantiate(JeepGuard);
        jeep.transform.position = this.transform.position + Vector3.right * .7f;
        jeep = (GameObject)GameObject.Instantiate(JeepGuard);
        jeep.transform.position = this.transform.position + Vector3.right * 1.4f;
        jeep = (GameObject)GameObject.Instantiate(JeepGuard);
        jeep.transform.position = this.transform.position + Vector3.right * 2.1f;
        jeep = (GameObject)GameObject.Instantiate(JeepGuard);
        jeep.transform.position = this.transform.position + Vector3.right * 2.8f;
        jeep = (GameObject)GameObject.Instantiate(JeepGuard);
        jeep.transform.position = this.transform.position + Vector3.left * .7f;
        jeep = (GameObject)GameObject.Instantiate(JeepGuard);
        jeep.transform.position = this.transform.position + Vector3.left * 1.4f;
        jeep = (GameObject)GameObject.Instantiate(JeepGuard);
        jeep.transform.position = this.transform.position + Vector3.left * 2.1f;
        jeep = (GameObject)GameObject.Instantiate(JeepGuard);
        jeep.transform.position = this.transform.position + Vector3.left * 2.8f;
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (grenadeStateActive && other.gameObject.tag == "PlayerBullet")
        {
            hitPoints--;
            EffectManager.Instance.waterSplash.transform.position = other.transform.position;
            EffectManager.Instance.waterSplash.Play();
            other.gameObject.SetActive(false);
            // GameObject.Destroy(other.gameObject);
        }
        //base.OnTriggerEnter(other);
    }
}
