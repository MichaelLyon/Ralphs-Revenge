using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GooglePlayPlayerData : MonoBehaviour
{
    public bool connected =false;
    public string leaderboard = "CgkI96nw_fUBEAIQBg"; //string for the main leaderboard



    // Use this for initialization
    void Start()
    {
        PlayGamesPlatform.Activate();
    }
    void Update()
    {

    }
    public void LogOutOfGooglePlay()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
    }
    public void LogInToGooglePlay()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                connected = true;
             //   Debug.Log("you logged in");
            }
            else
            {
                connected = false;
             //   Debug.Log("log in failed");
            }
        });
    }
    public void PostScore(int value, string board)
    {
        Social.ReportScore(value, board, (bool success) =>
        {

        });
    }
    public void DisplayLeaderBoard(string board)
    {
        
        ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(board);
    }

}
