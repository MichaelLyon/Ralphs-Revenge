using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class UIScore : MonoBehaviour
{
    Text text;
    public Text textShadow;
    [HideInInspector]
    public float score;
    public float scoreMultiplier;

    //Saving score
    public float savedScore;
    Rect rec1;
    Rect rec2;
    void Start()
    {
        text = GetComponent<Text>();
        scoreMultiplier = 1;
        style.fontSize = (int)(Screen.width * .06f);
        rec1 = new Rect(Screen.width * .235f, Screen.height * .035f, Screen.width * .26f, Screen.height * .03f);
        rec2 = new Rect(Screen.width * .23f, Screen.height * .03f, Screen.width * .26f, Screen.height * .03f);
    }

    public int maxScoreLength;
    StringBuilder sb = new StringBuilder();
    string scoreAsString = "0000";
    public void AddScore(float addition)
    {
        score += addition * scoreMultiplier;


        scoreAsString = score.ToString();
        sb.Length = 0;

        for (int i = 1; i <= maxScoreLength - scoreAsString.Length; ++i)
        {

            sb.Append('0');
        }

        //TODO: Only save on win / lose
        GameCurrencyControl.control.SaveGold();

        sb.Append(scoreAsString);
        scoreAsString = sb.ToString();
        //text.text = sb.ToString();
    }
    public GUIStyle style;
    void OnGUI()
    {
        if (!PopUpText.pausing)
        {
            style.normal.textColor = Color.black;
            GUI.Label(rec1, scoreAsString, style);
            style.normal.textColor = Color.white;
            GUI.Label(rec2, scoreAsString, style);
        }
    }
}
