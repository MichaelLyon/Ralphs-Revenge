using UnityEngine;
using System.Collections;

public class TruckEnemy : BaseShootingEnemy
{
    public GameObject movePoint;

    public override void BecomeActive()
    {
        movePoint.transform.parent = null;
        moving = true;
        base.BecomeActive();
    }

    bool moving;
    public override void Update()
    {
        if (alive)
        {
            transform.position = Vector3.Lerp(transform.position, movePoint.transform.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, movePoint.transform.position) < .05f && moving)
            {
                transform.position = movePoint.transform.position;
                moving = false;
            }
            if (moving)
                gameObject.transform.up = Vector3.RotateTowards(direction, Vector3.Normalize(movePoint.transform.position - transform.position), .01f, 1);
        }
        base.Update();
    }
}
