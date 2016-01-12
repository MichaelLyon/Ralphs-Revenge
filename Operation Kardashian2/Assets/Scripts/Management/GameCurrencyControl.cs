using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameCurrencyControl : MonoBehaviour 
{
    public static GameCurrencyControl control;

    public float savedScore;
    

	// Use this for initialization
	void Awake () 
    {
        if (control == null) // If control does not exist, then make it 
        {
            DontDestroyOnLoad(gameObject);
            control = this;
            Load();
        }
        else if(control != this) // If control does exist, use this
        {
            Destroy(gameObject);
        }

	
	}

    void OnGUI()
    {
        //GUI.Label(new Rect(10, 10, 100, 30), "Gold: " + savedScore);
        
    }

    public void SaveGold()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream savedGoldFile = File.Create(Application.persistentDataPath + "/playerCurrency.dat");

        //Personal Use Debug
        //FileStream savedOnMyPC = File.Create("C:/Users/Michael Tobin/Desktop/SavedGold/playerGold.txt");

        
        PlayerData data = new PlayerData();
        data.playerCurrency = savedScore;

        //Personal Use Debug
        //bf.Serialize(savedOnMyPC, data);

        
        bf.Serialize(savedGoldFile, data);
        savedGoldFile.Close();

        //Personal Use Debug
       // savedOnMyPC.Close();

        //savedGoldFile.Dispose(); Do i need?
    }


    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerCurrency.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/playerCurrency.dat", FileMode.Open);
            //Personal Use Debug
            //FileStream savedOnMyPcFILE = File.Open("C:/Users/Michael Tobin/Desktop/SavedGold/playerGold.txt", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            //Personal Use Debug
          //  PlayerData dataSavedOnMyPC = (PlayerData)bf.Deserialize(savedOnMyPcFILE);
            //Personal Use Debug
           // savedOnMyPcFILE.Close();
            file.Close();

            savedScore = data.playerCurrency;
        }
    }
}

[Serializable] //Tells unity that I can save data to this file

class PlayerData //The data in here will become serialized (Organized data that fits in a Binary format)
{
    public float playerCurrency;


}

//Locations 
// UIScore > Addscore
// Playercollision > Update if statement
// BaseEnemy > IfHitpoints statement
// Armory is not a float and not int
// PresistentData > Gold is now a float.