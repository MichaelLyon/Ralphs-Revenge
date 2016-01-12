using UnityEngine;
using System.Collections;

public class PauseButton : MonoBehaviour
{
    public static bool paused;
    Animator animator;

    public GameObject resumeButton;
    public GameObject quitButton;
    void Start()
    {
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PauseGame()
    {
        resumeButton.SetActive(true);
        quitButton.SetActive(true);
        animator.SetBool("OnScreen", true);
        Time.timeScale = 0;
        paused = true;
    }

    public void UnPauseGame()
    {
        resumeButton.SetActive(false);
        quitButton.SetActive(false);
        Time.timeScale = 1;
        animator.SetBool("OnScreen", false);
        paused = false;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        Application.LoadLevel("MainMenu");
        paused = false;
    }
}
