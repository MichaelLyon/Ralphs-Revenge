using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stat : MonoBehaviour
{
    public int[] costs;
    public Text costText;

    public Text upgradeText;

    [Tooltip("ShotType, Speed, Duplicate")]
    public string upgradeName;
    public int UpgradeLevel
    {
        get
        {
            return PlayerPrefs.GetInt(upgradeName);
        }
        set
        {
            PlayerPrefs.SetInt(upgradeName, value);
        }
    }

    public void Start()
    {
        UpgradeLevel = 1; //FOR TESTING ONLY
        if (UpgradeLevel == 0)
        {
            costText.text = "0";
        }
        else
        {
            costText.text = costs[UpgradeLevel - 1].ToString();
        }
        upgradeText.text = UpgradeLevel.ToString();
    }


    public void Upgrade()
    {
        if (UpgradeLevel < costs.Length)
            if (GameCurrencyControl.control.savedScore >= costs[UpgradeLevel - 1])
            {
                GameCurrencyControl.control.savedScore -= costs[UpgradeLevel - 1];
                UpgradeLevel++;
                costText.text = costs[UpgradeLevel - 1].ToString();
                upgradeText.text = UpgradeLevel.ToString();
                GameCurrencyControl.control.SaveGold();
                GameCurrencyControl.control.Load();
                AudioManager.Instance.PlayButtonPurchase();
            }
    }
}
