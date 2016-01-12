using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class WinOnDeath : MonoBehaviour
{

    // Use this for initialization
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

    public List<GameObject> triggerObjects;
    List<PopUpText> popUps;
    void OnDestroy()
    {
        if (popUps != null)
            if (popUps.Count > 0)
            {
                foreach (PopUpText popUp in popUps)
                {
                    if (popUp != null)
                        popUp.Triggered = true;
                }
            }
    }
}
