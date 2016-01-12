using UnityEngine;
using System.Collections;

public class MeleeEnemy : BaseEnemy
{
    // Use this for initialization
    public override void Start()
    {
        direction = Vector3.down;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
