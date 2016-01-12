using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public enum ShootType
{
    normalShot,
    doubleShot,
    tripleShot,
    quadShot
}
public class PlayerShooting : MonoBehaviour
{
    Vector3 direction;
    public float shootCooldown;
    float reloadTimer;
    public GameObject bullet;
    GameObject gameManager;
    public GameObject hydroPump;
    BulletManager bulletManager;
    public ShootType shootType;
    public Slider slider;
    public float waterLevel;
    public float shotSpeedIncreasePerLevel;
    void Start()
    {
        gameManager = GameObject.Find("Game Manager");           // finds the game manager
        bulletManager = gameManager.GetComponent<BulletManager>();    // gets the gameManager's bullet manager Script
        shootType = (ShootType)PlayerPrefs.GetInt("ShotType") - 1; // sets the shooting type to normal by default
        if (shootType < 0)
        {
            shootType = 0;
        }
        shootCooldown -= shotSpeedIncreasePerLevel * PlayerPrefs.GetInt("Speed");
        // Time.timeScale = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = waterLevel;
        if (Input.GetKey(KeyCode.Space))
        {
            if (waterLevel < 1)
                waterLevel += Time.deltaTime;
        }
        if (waterLevel <= 0)
        {
            hydroPump.SetActive(false);

            reloadTimer -= 1 * Time.deltaTime;


            if (reloadTimer <= 0)
            {
                if (shootType == ShootType.normalShot)
                {

                    Shoot(Vector3.up, this.transform.position);
                    // Shoot_0()
                }
                if (shootType == ShootType.doubleShot)
                {

                    DoubleShot();
                    //  Shoot_0();
                }
                if (shootType == ShootType.tripleShot)
                {

                    TripleShot();
                    //Shoot_0();
                }
                if (shootType == ShootType.quadShot)
                {

                    QuadShot();
                    // Shoot_0();
                }
                reloadTimer = shootCooldown;
            }
        }
        else
        {
            waterLevel -= .2f * Time.deltaTime;
            hydroPump.SetActive(true);
        }
    }

    GameObject GetBullet()
    {
        for (int i = 0; i < bulletManager.playerBullets.Count; i++)
        {
            if (bulletManager.playerBullets[i].activeInHierarchy == false && bulletManager.playerBullets[i].name == bullet.name + "(Clone)")
            {
                return bulletManager.playerBullets[i];
            }
        }
        return null;
    }
    GameObject MakeBullet()
    {
        // if (GetBullet() == null)
        //  {
        // for (int i = 0; i < 10; i++)
        //  {
        GameObject bullet = (GameObject)GameObject.Instantiate(this.bullet);
        bullet.transform.parent = gameManager.transform;

        bullet.transform.localScale *= Camera.main.aspect;
        //   bullet.transform.position = new Vector3(bullet.transform.position.x * Camera.main.aspect, bullet.transform.position.y * Camera.main.aspect, bullet.transform.position.z);
        bulletManager.playerBullets.Add(bullet);
        //  Debug.Log("made a bullet");
        return bullet;

        // }
        // }

    }
    void Shoot(Vector3 direction, Vector3 position)
    {
        GameObject bullet = GetBullet();
        if (bullet == null)
        {
            bullet = MakeBullet();
        }

        //bullet.SetActive(true);
        bullet.SetActive(true);

        // Debug.Log(bullet.name + " " + bullet.activeInHierarchy);
        bullet.transform.position = position + Vector3.up;

        PlayerBullet bulletScript = bullet.GetComponent<PlayerBullet>();
        bulletScript.direction = direction;

    }


    void DoubleShot()
    {
        // collider.bounds.
        float x = transform.position.x;
        Vector3 pos = new Vector3(x + .4f * Camera.main.aspect, transform.position.y, transform.position.z);
        Shoot(Vector3.up, pos);
        //direction = new Vector3(transform.forward,transform.forward.y,transform.forward.z);
        pos = new Vector3(x - .4f * Camera.main.aspect, transform.position.y, transform.position.z);
        Shoot(Vector3.up, pos);

    }
    void TripleShot()
    {
        Shoot(Vector3.up, this.transform.position);
        Vector3 direction = Vector3.Normalize(Vector3.RotateTowards(Vector3.up, Vector3.right, .4f, 1));
        Shoot(direction, this.transform.position);
        direction = Vector3.Normalize(Vector3.RotateTowards(Vector3.up, Vector3.left, .4f, 1));
        Shoot(direction, this.transform.position);
    }
    void QuadShot()
    {
        Vector3 direction = Vector3.Normalize(Vector3.RotateTowards(Vector3.up, Vector3.right, .1f, 1));
        Shoot(direction, this.transform.position);
        direction = Vector3.Normalize(Vector3.RotateTowards(Vector3.up, Vector3.right, .3f, 1));
        Shoot(direction, this.transform.position);
        direction = Vector3.Normalize(Vector3.RotateTowards(Vector3.up, Vector3.left, .1f, 1));
        Shoot(direction, this.transform.position);
        direction = Vector3.Normalize(Vector3.RotateTowards(Vector3.up, Vector3.left, .3f, 1));
        Shoot(direction, this.transform.position);
    }
}
