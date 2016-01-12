using UnityEngine;
using System.Collections;

public class DuplicateMovement : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        speed = Camera.main.GetComponent<CameraMovement>().speed;
        player = GameObject.Find("Player");
        transform.position = player.transform.position + Vector3.right;
    }

    Vector3 previousPosition;
    Vector3 currentPosition;
    Vector3 changeInPosition;
    float speed;
    void Update()
    {
        Move();
    }

    [HideInInspector]
    public Vector3 position;
    void Move()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
        if (Input.GetMouseButton(0))
        {
            transform.position = player.transform.position + position;
        }
        else
        {
            transform.position += new Vector3(0, speed * Time.deltaTime, 0);
        }
    }
}
