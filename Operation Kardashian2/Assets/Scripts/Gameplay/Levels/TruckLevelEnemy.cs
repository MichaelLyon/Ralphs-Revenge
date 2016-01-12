using UnityEngine;
using System.Collections;

public class TruckLevelEnemy : BaseShootingLevelEnemy
{
    // Vector3 currentPos;
    //Vector3 previousPos;
    Vector3 destination;
    public int burstLength;
    public float timeBetweenShots;
    public float timeBetweenBursts;
    float shootCooldown;
    float burstCooldown;
    int shotsFired;
    GameObject player;


    // Use this for initialization
    void Start()
    {


        base.Start();

        base.Start();
    }

    public override void BecomeAlive()
    {
        base.BecomeAlive();
        shootCooldown = timeBetweenBursts;
        player = GameObject.Find("Player");
        bool leftSideSpawn = false;
        int side = Random.Range(0, 100);
        if (side < 50) leftSideSpawn = true;
        if (leftSideSpawn)
        {
            destination = Functions.GetWorldPointFromScreenPoint((int)(Random.Range(Screen.width * .2f, Screen.width * .45f)), (int)(Screen.height), 0);
        }
        else
        {
            destination = Functions.GetWorldPointFromScreenPoint((int)(Random.Range(Screen.width * .55f, Screen.width * .8f)), (int)(Screen.height), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            if (burstCooldown > 0) burstCooldown -= 1 * Time.deltaTime;

            if (burstCooldown <= 0)
            {
                shootCooldown -= 1 * Time.deltaTime;
                if (shootCooldown <= 0)
                {
                    if (player != null)
                        Shoot(((player.transform.position - player.transform.up) - transform.position).normalized, this.transform.position);
                    shootCooldown = timeBetweenShots;
                    shotsFired++;
                }
            }
            if (shotsFired == 3)
            {
                shotsFired = 0;
                burstCooldown = timeBetweenBursts;
            }

            transform.position = Vector3.Lerp(transform.position, destination, 1.5f * Time.deltaTime);
            if (Vector3.Distance(transform.position, destination) < .05f)
            {
                transform.position = destination;
            }
            if (gameObject.activeSelf == false)
            {
                burstCooldown = timeBetweenBursts;
            }
        }
        base.Update();
        //  previousPos = currentPos;
        //currentPos = transform.position;
    }
}
