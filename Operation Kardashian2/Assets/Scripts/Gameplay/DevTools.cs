using UnityEngine;
using System.Collections;

public class DevTools : MonoBehaviour
{
    GameObject player;
    bool buttons;
    void Start()
    {
        player = GameObject.Find("Player");
        buttons = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject BulletUp;
    public GameObject Shield;
    public GameObject Dup;
    public GameObject ScoreMult;
    public GameObject FireRate;
    public void BulletUpSpawn()
    {
        GameObject.Instantiate(BulletUp, player.transform.position, Quaternion.identity);
    }

    public void ShieldSpawn()
    {
        GameObject.Instantiate(Shield, player.transform.position, Quaternion.identity);
    }

    public void DupSpawn()
    {
        GameObject.Instantiate(Dup, player.transform.position, Quaternion.identity);
    }

    public void ScoreMultSpawn()
    {
        GameObject.Instantiate(ScoreMult, player.transform.position, Quaternion.identity);
    }

    public void FireRateSpawn()
    {
        GameObject.Instantiate(FireRate, player.transform.position, Quaternion.identity);
    }

    public GameObject peanutGun;
    public void PeanutGun()
    {
        GameObject gun = (GameObject)GameObject.Instantiate(peanutGun, player.transform.position, Quaternion.identity);
        gun.transform.parent = player.transform;
    }

    void OnGUI()
    {
        if (buttons)
        {
            if (GUI.Button(new Rect(0, 50, 100, 50), "Shield"))
            {
                ShieldSpawn();
            }
            if (GUI.Button(new Rect(0, 100, 100, 50), "Fire Rate"))
            {
                FireRateSpawn();

            }
            if (GUI.Button(new Rect(0, 150, 100, 50), "Bullet Up"))
            {
                BulletUpSpawn();
            }
            if (GUI.Button(new Rect(0, 200, 100, 50), "Dup"))
            {
                DupSpawn();
            }
            if (GUI.Button(new Rect(0, 250, 100, 50), "Score"))
            {
                ScoreMultSpawn();
            }
            if (GUI.Button(new Rect(0, 300, 100, 50), "Peanut Gun"))
            {
                PeanutGun();
            }
            if (GUI.Button(new Rect(0, 400, 100, 50), "Back"))
            {
                Application.LoadLevel("MainMenu");
                Time.timeScale = 1;
            }
        }

        if (GUI.Button(new Rect(0, 0, 100, 50), "Dev Tools"))
        {
            if (Time.timeScale == 0)
            {
                buttons = false;
                Time.timeScale = 1;
            }
            else
            {
                buttons = true;
                Time.timeScale = 0;
            }
        }

        if (GUI.Button(new Rect(Screen.width - 100, 0, 100, 50), "Restart"))
        {
            Application.LoadLevel(Application.loadedLevelName);
        }
    }
}
