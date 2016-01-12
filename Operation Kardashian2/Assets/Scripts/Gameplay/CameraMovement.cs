using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public float speed;
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }
}
