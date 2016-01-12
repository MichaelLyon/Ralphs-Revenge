using UnityEngine;
using System.Collections;

public class DuplicateCollision : MonoBehaviour
{
    public int hitPoints = 1;
    public float invulnerabilityTime = 1;
    float invulnerableCountDown;
    bool invulnerable = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (invulnerable)
        {
            invulnerableCountDown -= 1 * Time.deltaTime;
            //gameObject.renderer.active
        }
        if (invulnerableCountDown <= 0)
        {
            invulnerable = false;
        }
        if (hitPoints <= 0)
        {
            gameObject.SetActive(false);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Enemy" && !invulnerable)
        {
            if (other.gameObject.tag == "EnemyBullet")
            {
                other.gameObject.SetActive(false);
                EnemyBullet bulletScript = other.gameObject.GetComponent<EnemyBullet>();
                hitPoints -= (int)bulletScript.damage;
            }
            else
            {
                hitPoints--;
                invulnerable = true;
                invulnerableCountDown = invulnerabilityTime;
            }

            invulnerable = true;
            invulnerableCountDown = invulnerabilityTime;
        }
        else if (other.gameObject.tag == "Environment")
        {
            hitPoints--;
            invulnerable = true;
            invulnerableCountDown = invulnerabilityTime;
        }
        else if (other.gameObject.tag == "EndLevel")
        {
            //LEVEL ENDED
        }
    }
}
