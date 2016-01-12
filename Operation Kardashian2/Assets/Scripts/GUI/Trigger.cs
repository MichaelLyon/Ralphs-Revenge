using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trigger : MonoBehaviour
{
    public bool triggerOnPlayer;
    public bool triggerOnEnemy;
    public bool triggerOnBullet;

    public GameObject[] triggerObjects;

    List<PopUpText> popUps;
    void Start()
    {
        popUps = new List<PopUpText>();
        foreach (GameObject triggerObject in triggerObjects)
        {
            if (triggerObject.GetComponent<PopUpText>() != null)
            {
                popUps.Add(triggerObject.GetComponent<PopUpText>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && triggerOnPlayer)
        {
            Debug.Log("Collided With Player");
            if (popUps.Count > 0)
            {
                foreach (PopUpText popUp in popUps)
                {
                    popUp.Triggered = true;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
