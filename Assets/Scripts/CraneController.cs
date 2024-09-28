using GameMath.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CraneController : MonoBehaviour
{
    private Vector3 yAxis = new(0, 1, 0);
    public HoldableButton leftButton;
    public HoldableButton rightButton;
    private int craneSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RotateCrane();
    }

    private void RotateCrane()
    {
        if (leftButton.IsHeldDown)
        {
            transform.Rotate(yAxis, craneSpeed * Time.deltaTime);
        }
        if (rightButton.IsHeldDown) 
        {
            transform.Rotate(yAxis, -craneSpeed * Time.deltaTime);
        }
    }
}
