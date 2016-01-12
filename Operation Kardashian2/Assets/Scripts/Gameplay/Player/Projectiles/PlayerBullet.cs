using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour
{

    // Use this for initialization
    //GameObject player;
    //float deathTimer=2;
   // public bool activated = false; // bullets set to active at spawn
    public float speed;
    public Vector3 direction;
    void Start()
    {
       // gameObject.SetActive(false);
        //player= GameObject.FindGameObjectWithTag ("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > Functions.ScreenPointTopRight.y)
        {
            gameObject.SetActive(false);

           // activated = false;
        }
       // gameObject.SetActive(activated);

        if (gameObject.activeSelf)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
        
        
        //this.renderer.enabled = false;
       // gameObject.active = activated;
       // renderer.active = false;// activated;
        

    }
}
