using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public float rotateSpeed;
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
    }
}
