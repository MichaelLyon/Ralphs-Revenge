using UnityEngine;
using System.Collections;

public class EnemySpotlightTower : BaseShootingEnemy
{
    bool playerInVision;
    EnemyShootType initialShootType;
    public GameObject spotLight;
    Transform trans;
    // Use this for initialization
    public override void Start()
    {
      //  direction = Vector3.up;
        //speed = 1f;
        trans = transform;
        base.Start();
        gameObject.SetActive(true);
        initialShootType = shootType;
       // spotLight = GameObject.Find("SpotLightOrigen");
    }

    // Update is called once per frame
    public override void Update()
    {
       
        if (!playerInVision)
        {
            shootType = EnemyShootType.Stall;

        }
        else
        {
           // spotLight.transform.LookAt(player.transform.position,Vector3.back);
            spotLight.transform.LookAt(player.transform.position,Vector3.forward);//forward = Vector3.Normalize(player.transform.position - spotLight.transform.position);
            //spotLight.transform.position = new Vector3(transform.position.x,transform.position.x+.5f,transform.position.z);
            //spotLight.transform.rotation.SetAxisAngle()
            //spotLight.transform.forward =Vector3.RotateTowards(spotLight.transform.forward,Vector3.Normalize(new Vector3(spotLight.transform.position.x,spotLight.transform.position.y-1,transform.position.z) - spotLight.transform.position), .5f, 1);
            shootType = initialShootType;
        }

        base.Update();
    }
    public override void OnTriggerEnter(Collider other) //TODO: possibly reset the burst cooldown when they player moves out of sight
    {
        if (other.gameObject.tag == "Player")
        {
            playerInVision = true;
           
//            Debug.Log(playerInVision);
        }
        // base.OnTriggerEnter(other);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
           // playerInVision = false;
           // Debug.Log(playerInVision);
        }
    }
}
