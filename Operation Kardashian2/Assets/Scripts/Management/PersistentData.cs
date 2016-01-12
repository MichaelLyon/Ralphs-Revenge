using UnityEngine;
using System.Collections;

public static class PersistentData
{
    //Changed int to float
    public static float Gold
    {
        get
        {
            return PlayerPrefs.GetFloat("Gold");
        }
        set
        {
            PlayerPrefs.SetFloat("Gold", value);
        }
    }

    public static int BirdPooCharges
    {
        get
        {
            return PlayerPrefs.GetInt("BirdPooCharges");
        }
        set
        {
            PlayerPrefs.SetInt("BirdPooCharges", value);
        }
    }

    public static int StampedeCharges
    {
        get
        {
            return PlayerPrefs.GetInt("StampedeCharges");
        }
        set
        {
            PlayerPrefs.SetInt("StampedeCharges", value);
        }
    }

    public static float Volume
    {
        get
        {
            return PlayerPrefs.GetFloat("Volume");
        }
        set
        {
            PlayerPrefs.SetFloat("Volume", value);
        }
    }

    public static int ShotType
    {
        get
        {
            return PlayerPrefs.GetInt("ShotType");
        }
        set
        {
            PlayerPrefs.SetInt("ShotType", value);
        }
    }

    public static int ShotSpeed
    {
        get
        {
            return PlayerPrefs.GetInt("ShotSpeed");
        }
        set
        {
            PlayerPrefs.SetInt("ShotSpeed", value);
        }
    }

    public static int Duplicates
    {
        get
        {
            return PlayerPrefs.GetInt("Duplicates");
        }
        set
        {
            PlayerPrefs.SetInt("Duplicates", value);
        }
    }
}
