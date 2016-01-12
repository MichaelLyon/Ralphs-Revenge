using UnityEngine;
using System.Collections;

public class PeanutGun : MonoBehaviour
{

    Vector3 direction;
    public float shootCooldown;
    float reloadTimer;
    public GameObject bullet;
    GameObject gameManager;
    BulletManager bulletManager;
    public ShootType shootType;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager");           // finds the game manager
        bulletManager = gameManager.GetComponent<BulletManager>();    // gets the gameManager's bullet manager Script
    }

    // Update is called once per frame
    void Update()
    {

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
            reloadTimer = shootCooldown;
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
        if (Camera.main.aspect > 1)
        {
            bullet.transform.localScale *= Camera.main.aspect;
            bullet.transform.position = new Vector3(bullet.transform.position.x * Camera.main.aspect, bullet.transform.position.y * Camera.main.aspect, bullet.transform.position.z);
        }
        else
            bullet.transform.localScale = new Vector3(bullet.transform.localScale.x * Camera.main.aspect, bullet.transform.localScale.y * Camera.main.aspect, bullet.transform.localScale.z);
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
        bullet.transform.position = position + Vector3.up / 2;

        PlayerBullet bulletScript = bullet.GetComponent<PlayerBullet>();
        bulletScript.direction = direction;
    }


    void DoubleShot()
    {
        // collider.bounds.
        float x = transform.position.x;
        Vector3 pos = new Vector3(x + .5f, transform.position.y, transform.position.z);
        Shoot(Vector3.up, pos);
        //direction = new Vector3(transform.forward,transform.forward.y,transform.forward.z);
        pos = new Vector3(x - .5f, transform.position.y, transform.position.z);
        Shoot(Vector3.up, pos);

    }
}
