using UnityEngine;
using System.Collections;

public class MotorCycle : BaseShootingEnemy
{

    // Use this for initialization
    public override void Start()
    {
        direction = Vector3.right;
        base.Start();
    }
    Vector3 right, left;
    public GameObject sideCar;
    // public GameObject bike;
    public int damageState;
    public BoxCollider old;
    //  public BoxCollider strechedcollider;
    // Update is called once per frame
    public override void Update()
    {
        right = Functions.GetWorldPointFromScreenPoint(Screen.width, (int)(Screen.height * .96f), 0);
        left = Functions.GetWorldPointFromScreenPoint(0, (int)(Screen.height * .96f), 0);
        transform.position = new Vector3(transform.position.x, right.y, 0);
        // transform.position += Vector3.up * 2 * Time.deltaTime;
        if (Vector3.Distance(transform.position, right) < 1f)
        {
            direction = Vector3.left;
        }
        else if (Vector3.Distance(transform.position, left) < 1f)
        {
            direction = Vector3.right;
        }
        if (hitPoints <= 0 && damageState == 1)
        {
            Destroy(old);
            damageState = 2;
            sideCar.SetActive(false);
            hitPoints = (int)(startingHitPoints * .5f);
            timeBetweenShots *= .25f;
            timeBetweenBursts *= .5f;
            bulletsInBurst = (int)(bulletsInBurst * .5f);
            speed *= 2;
        }
        if (hitPoints <= 0)
        {
            Destroy(this.gameObject);
        }
        base.Update();
    }
}
