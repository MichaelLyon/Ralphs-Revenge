using UnityEngine;
using System.Collections;

public class PopUpText : MonoBehaviour
{

    protected Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !PauseButton.paused)
        {
            if (canSkip)
            {
                OffScreen();
            }
        }
    }

    bool triggered;
    public bool Triggered
    {
        get
        {
            return triggered;
        }
        set
        {
            triggered = value;
            if (triggered)
                OnScreen();
        }
    }

    public void OnScreen()
    {
        StartCoroutine(Pause());
        animator.SetBool("OnScreen", true);
        pausing = true;
    }

    public static bool pausing; //used to check if we need to hide the score behind a drop down
    bool canSkip;
    public float pauseDelay;
    IEnumerator Pause()
    {
        yield return new WaitForSeconds(pauseDelay);
        Time.timeScale = 0;
        canSkip = true;
    }

    public void OffScreen()
    {
        pausing = false;
        Time.timeScale = 1;
        animator.SetBool("OnScreen", false);
    }
}
