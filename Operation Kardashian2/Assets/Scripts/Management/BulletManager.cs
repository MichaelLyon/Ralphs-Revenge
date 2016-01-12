using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour
{

    // Use this for initialization
    public GameObject playerBullet;
  //  public GameObject enemyBullet;
    // public IList<CPlayerBullet> playerBulletList = new IList<CPlayerBullet>();
    [HideInInspector]
    public List<GameObject> playerBullets = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> enemyBullets = new List<GameObject>();
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {

    }
}
