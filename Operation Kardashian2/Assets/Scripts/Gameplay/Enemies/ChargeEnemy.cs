using UnityEngine;
using System.Collections;

public class ChargeEnemy : BaseEnemy
{
    GameObject player;
    public float maxSpeed = 6;
    public float speedMult = 1f;
    float startSpeed=1;
    // Use this for initialization
    public override void Start()
    {
        startSpeed = speed;
        player = GameObject.Find("Player");
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (alive)
        {
            if (player != null&& transform.position.y>player.transform.position.y)
            {
                direction = Vector3.RotateTowards(direction, Vector3.Normalize(player.transform.position - transform.position), .01f, 1);
            }
            speed = speed *= speedMult;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
            gameObject.transform.up = direction;
        }
        base.Update();
    }
    public override void BecomeActive( )
    {
        speed = startSpeed;
        base.BecomeActive();
    }
}
