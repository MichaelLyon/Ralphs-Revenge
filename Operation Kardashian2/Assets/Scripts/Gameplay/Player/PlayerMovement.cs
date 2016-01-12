using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    void Start()
    {
        speed = Camera.main.GetComponent<CameraMovement>().speed;
        OilSlickCoolDownFrames = (int)(OilSlickCoolDownTime * 60);
        Input.multiTouchEnabled = false;
    }

    Vector3 previousPosition;
    Vector3 currentPosition;
    Vector3 changeInPosition;
    float speed;
    bool canControl = true;
    public float OilSlickCoolDownTime = .5f;
    int OilSlickCoolDownFrames;
    int OilSlickCoolDownCounter = 0;
    public float moveAmplifier = 1;

    void Update()
    {
        Move();
        KeepOnScreen();
        if (!canControl)
        {
            checkControl();
        }
    }

    public float maximumChangeInPosition;
    void Move()
    {
        if (Time.timeScale > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (canControl)
                {
                    currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, -1 * Camera.main.transform.position.z));
                }
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }
            if (Input.GetMouseButton(0))
            {
                if (canControl)
                {
                    previousPosition = currentPosition;
                    currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, -1 * Camera.main.transform.position.z));
                    changeInPosition = currentPosition - previousPosition;
                }
                if (changeInPosition.magnitude < maximumChangeInPosition)
                {
                    transform.position += changeInPosition * moveAmplifier;
                }
            }
            else
            {
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }
        }
    }

    public void hitOil()
    {
        if (canControl == true)
        {
            loseControl();
            OilSlickCoolDownCounter = 0;
        }
    }
    private void checkControl()
    {
        OilSlickCoolDownCounter++;
        if (OilSlickCoolDownCounter >= OilSlickCoolDownFrames)
        {
            resumeControl();
            OilSlickCoolDownCounter = 0;
        }
    }

    private void loseControl()
    {
        canControl = false;
    }
    private void resumeControl()
    {
        canControl = true;
    }

    Vector3 playerScreenPosition;
    void KeepOnScreen() //TODO: MAKE MORE EFFICIENT
    {
        playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (playerScreenPosition.x > Screen.width)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, -1 * Camera.main.transform.position.z)).x, transform.position.y, transform.position.z);
        }
        if (playerScreenPosition.x < 0)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -1 * Camera.main.transform.position.z)).x, transform.position.y, transform.position.z);
        }
        if (playerScreenPosition.y > Screen.height)
        {
            transform.position = new Vector3(transform.position.x, Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, -1 * Camera.main.transform.position.z)).y, transform.position.z);
        }
        if (playerScreenPosition.y < 0)
        {
            transform.position = new Vector3(transform.position.x, Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -1 * Camera.main.transform.position.z)).y, transform.position.z);
        }
    }
}
