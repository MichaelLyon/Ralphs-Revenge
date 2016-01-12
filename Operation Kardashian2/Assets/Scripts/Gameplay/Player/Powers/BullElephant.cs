using UnityEngine;
using System.Collections;

public class BullElephant : MonoBehaviour
{

    // Use this for initialization
    Vector3 pos;
    public int damage;
    void Start()
    {
        pos = Functions.GetWorldPointFromScreenPoint(Screen.width, 0, 0);
        //gameObject.SetActive(false);
    }
    public float speed;
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * 2 * Time.deltaTime;
        transform.position += Vector3.right * speed * Time.deltaTime;
        if(transform.position.x>pos.x+1)
        {
            gameObject.SetActive(false);
        }
    }
    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Enemy")
        {
          //  BaseEnemy script = (BaseEnemy)other.gameObject.GetComponent<BaseEnemy>();
        //    script.hitPoints -= damage;
        }
    }
}
