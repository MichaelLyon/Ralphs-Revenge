using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ElephantSelect : MonoBehaviour
{
    public static string ElephantSelected
    {
        get
        {
            return PlayerPrefs.GetString("ElephantSelected");
        }
        set
        {
            if (value != "Ralph" && value != "Ellie")
            {
                Debug.Log("YOU BROKE ELEPHANT SELECT");
            }
            PlayerPrefs.SetString("ElephantSelected", value);
        }
    }

    public string name;
    public Outline outline;
    public Outline otherElephantOutline;
    void Start()
    {
        if (ElephantSelected != name)
        {
            outline.enabled = false;
        }
        else
        {
            outline.enabled = true;
        }
        if (ElephantSelected == "" && name == "Ralph")
        {
            outline.enabled = true;
        }
    }

    public void ElephantPressed()
    {
        ElephantSelected = name;
        outline.enabled = true;
        otherElephantOutline.enabled = false;
    }
}
