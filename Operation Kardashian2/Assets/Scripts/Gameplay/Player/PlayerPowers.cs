using UnityEngine;
using System.Collections;

public class PlayerPowers : MonoBehaviour
{
    bool powerInUse = false;
    bool powerPrimed = false;
    public GameObject BullElephantsPrefab;
    GameObject BullElephants;
    public GameObject gameManager;
    EntityManager manager;
   // public ParticleSystem birdPoo;
   // ParticleSystem system;
    // Use this for initialization
    void Start()
    {
       // system = (ParticleSystem)GameObject.Instantiate(birdPoo);
        manager = gameManager.GetComponent<EntityManager>();
        if (BullElephantsPrefab != null)
        {
            BullElephants = (GameObject)GameObject.Instantiate(BullElephantsPrefab);
            BullElephants.SetActive(false);
            if (Camera.main.aspect > 1)
            {
                BullElephants.transform.localScale *= Camera.main.aspect;
                BullElephants.transform.position = new Vector3(BullElephants.transform.position.x * Camera.main.aspect, BullElephants.transform.position.y * Camera.main.aspect, BullElephants.transform.position.z);
            }
            else
            {
                BullElephants.transform.localScale = new Vector3(BullElephants.transform.localScale.x * Camera.main.aspect, BullElephants.transform.localScale.y * Camera.main.aspect, BullElephants.transform.localScale.z);
                BullElephants.transform.position = new Vector3(BullElephants.transform.position.x * Camera.main.aspect, BullElephants.transform.position.y * Camera.main.aspect, BullElephants.transform.position.z);
            }
        }
        //TODO: check player preffs to see what powers the player has purchased and the amount, then create the powers if needed for later use
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && powerPrimed && !powerInUse)
        {
            powerInUse = true;
            powerPrimed = false;
            SpawnBirdDrop();
            // SpawnBullCharge();
        }
        if (BullElephants != null && !BullElephants.activeSelf)
        {
            powerInUse = false;
        }
    }

    void OnGUI() //TODO: replace with not shit GUI
    {

        if (GUI.Button(new Rect(0, Screen.height - 50, 50, 50), "spawn") && !powerInUse)
        {
            if (!powerPrimed)
            {
                powerPrimed = true;
            }
            else
                powerPrimed = false;
            //powerInUse = true;
            //  BullElephants.SetActive(true);
        }
        //  if(GUI.Button())
    }
    public void SpawnBullCharge()
    {
        BullElephants.SetActive(true);
        BullElephants.transform.position = Functions.GetWorldPointFromScreenPoint(0, (int)Input.mousePosition.y, 0);
        //TODO: set elephants to active and move them to the mouse point
    }
    public void SpawnBirdDrop()
    {
        foreach (GameObject enemy in manager.enemiesList)
        {
            if (enemy != null)
            {
                BaseEnemy script = (BaseEnemy)enemy.GetComponent<BaseEnemy>();
                if (script.alive && enemy.activeSelf)
                {
                    gameManager.SendMessage("SpawnPoo", enemy.transform.position);
                    enemy.SendMessage("ApplyDamage", 3);
                    //  script.hitPoints -= 3;
                }
            }
        }
    }
}
