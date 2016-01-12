using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour
{
    SpriteRenderer[] renderers;
    public Vector3 direction;
    public Vector3 velocity;
    public float speed;
    public int startingHitPoints;
    [HideInInspector]
    public int hitPoints;
    [HideInInspector]
    public UIScore uiScore;
    public float scoreValue;
    public GameObject powerUp;
    [HideInInspector]
    public int powerUpChance;
    public float powerUpPercentChance;
    public bool alive;
    [HideInInspector]
    public bool onScreen;
    protected float damageFlashCooldown = -1;
    //GameObject gameManager;
    // Use this for initialization
    // EntityManager entityManagerScript;
    public virtual void Start()
    {
        renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        uiScore = GameObject.Find("PlayerInfo").GetComponent<UIScore>();
        alive = true;

    }

    public virtual void BecomeActive()
    {
        hitPoints = startingHitPoints;
        powerUpChance = Random.Range(1, 101);
        scoreValue = hitPoints;
        gameObject.SetActive(true);
        //  alive = true;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (alive)
        {
            damageFlashCooldown -= Time.deltaTime;
            if (damageFlashCooldown > 0)
            {
                foreach (SpriteRenderer renderer in renderers)
                {

                    renderer.material.color = Color.red;
                }
            }
            else
            {
                foreach (SpriteRenderer renderer in renderers)
                {
                    renderer.material.color = Color.white;
                }

            }

            velocity = direction * speed;
            transform.position += velocity * Time.deltaTime;
            if (hitPoints <= 0)
            {
                if (powerUpChance <= powerUpPercentChance && powerUp != null)
                {
                    GameObject.Instantiate(powerUp, transform.position, Quaternion.identity);
                }
                uiScore.AddScore(scoreValue);
                EntityManager.Instance.enemiesKilled++;

                //Currency Control
                GameCurrencyControl.control.savedScore += scoreValue;

                gameObject.tag = "Untagged";
                alive = false;
                EffectManager.Instance.SpawnCoin(transform.position);
            }
        }
        else
        {
            if (renderers[0].material.color.a > .1f)
            {
                foreach (SpriteRenderer renderer in renderers)
                {
                    if (renderer.sortingOrder > 0)
                    {
                        renderer.material.color *= .60f;
                    }
                    else
                    {
                        renderer.material.color *= .90f;
                    }
                }
            }
            else
                // Destroy(gameObject);
                gameObject.SetActive(false);

        }

    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (alive)
        {
            if (other.gameObject.tag == "PlayerBullet" && other.gameObject.activeSelf)
            {
                other.gameObject.SetActive(false);
                ApplyDamage(1);
                damageFlashCooldown = .05f;
                EffectManager.Instance.waterSplash.transform.position = other.transform.position;
                EffectManager.Instance.waterSplash.Play();

                AudioManager.Instance.PlaySplash();
            }
        }
    }

    public virtual void ApplyDamage(int damage)
    {
        hitPoints -= damage;
    }

    public virtual void OnBecameInvisible()
    {
        // gameObject.SetActive(false);
        //  entityManagerScript.enemiesList.Remove(this.gameObject);
        //  Destroy(this.gameObject);
        //TODO:add a delay for destroying
    }
    public virtual void OnBecameVisible()
    {
        // alive = true;
        // BecomeActive();
        onScreen = true;
        // gameObject.SetActive(true);
    }
}
