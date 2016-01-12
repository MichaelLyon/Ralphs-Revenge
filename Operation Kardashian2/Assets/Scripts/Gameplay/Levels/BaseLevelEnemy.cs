using UnityEngine;
using System.Collections;

public class BaseLevelEnemy : MonoBehaviour
{
    [HideInInspector]
    public Vector3 direction;
    Vector3 velocity;
    public float speed;
    public int startingHitPoints;
    int hitPoints;
    UIScore uiScore;
    public float scoreValue;
    public GameObject powerUp;
    int powerUpChance;
    public float powerUpPercentChance;
    // Use this for initialization
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        if (!alive && transform.position.y <= Camera.main.transform.position.y + 5)
        {
            BecomeAlive();
        }
        if (alive)
        {
            velocity = direction * speed;
            transform.position += velocity * Time.deltaTime;
            if (hitPoints <= 0)
            {
                if (powerUpChance <= powerUpPercentChance && powerUp != null)
                {
                    GameObject.Instantiate(powerUp, transform.position, Quaternion.identity);
                }
                uiScore.AddScore(scoreValue);
                Destroy(this.gameObject);
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBullet" && other.gameObject.activeSelf)
        {
            other.gameObject.SetActive(false);
            hitPoints--;
        }
    }

    protected bool alive;
    public virtual void BecomeAlive()
    {
        uiScore = GameObject.Find("Score").GetComponent<UIScore>();
        powerUpChance = Random.Range(1, 101);
        hitPoints = startingHitPoints;
        alive = true;
    }

    public bool destroyWhenOffScreen;
    void OnBecameInvisible()
    {
        if (destroyWhenOffScreen)
            Destroy(this.gameObject);
    }
}
