﻿using UnityEngine;
using System.Collections;

public class WinPopUpText : PopUpText
{
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Triggered)
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
