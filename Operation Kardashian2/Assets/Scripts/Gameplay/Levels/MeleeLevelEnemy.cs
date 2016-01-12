using UnityEngine;
using System.Collections;

public class MeleeLevelEnemy : BaseLevelEnemy
{

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    public override void BecomeAlive()
    {
        direction = Vector3.down;
        base.BecomeAlive();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
