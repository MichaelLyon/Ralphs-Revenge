using UnityEngine;
using System.Collections;

public class DeathBeam : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ticker += Time.deltaTime;
    }

    float ticker;
    public float tickTime;
    void OnTriggerStay(Collider other)
    {
        if (ticker > tickTime)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.GetComponent<BaseEnemy>().hitPoints--;
                ticker = 0;
                Debug.Log("I AM COLLDIDIFNSDIFN WIR SHITS");
            }
        }
    }
}
