using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Armory : MonoBehaviour
{
    public float currencyCheat;
    public Text currencyText;
    void Start()
    {
        GameCurrencyControl.control.savedScore = currencyCheat;
        currencyText.text = currencyCheat.ToString();
    }

    public void PurchaseMade()
    {
        currencyText.text = GameCurrencyControl.control.savedScore.ToString();
    }
}
