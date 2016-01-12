using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BackgroundTile : MonoBehaviour
{
    public List<GameObject> backgroundTiles;

    void Start()
    {
        if (Camera.main.aspect > 1)
        {
            transform.localScale *= Camera.main.aspect;
            transform.position = new Vector3(0, transform.position.y * Camera.main.aspect, transform.position.z);
        }
        else
            transform.localScale = new Vector3(transform.localScale.x * Camera.main.aspect, transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {

    }

    void OnBecameInvisible()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (20 * transform.localScale.y), transform.position.z);
    }
}
