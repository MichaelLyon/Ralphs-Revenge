using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILives : MonoBehaviour
{
    public Image[] lives;
    PlayerCollisions player;
    int maxHitPoints;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerCollisions>();
        maxHitPoints = player.hitPoints;
    }

    //TODO: Call only on hitpoint changes
    void Update()
    {
        // CheckLives();
    }

    public void CheckLives()
    {
        if (player.hitPoints < maxHitPoints)
        {
            for (int i = lives.Length; i > player.hitPoints; --i)
            {
                if (i > 0)
                    lives[i - 1].enabled = false;
            }
        }
    }

    public void AddLife()
    {
        lives[player.hitPoints - 1].enabled = true;
    }
}
