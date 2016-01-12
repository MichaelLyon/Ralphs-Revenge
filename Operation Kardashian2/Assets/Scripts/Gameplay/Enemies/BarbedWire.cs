using UnityEngine;
using System.Collections;

public class BarbedWire : BaseEnemy
{

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "BullElephant")
        {
            hitPoints--;
        }
    }
}
