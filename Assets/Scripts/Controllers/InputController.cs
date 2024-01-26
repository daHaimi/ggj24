using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputController : MonoBehaviour
{
    public bool Interact { get;  protected set; }
    public bool Laugh { get; protected set; }
    public float Horizontal { get; protected set; }
    public float Vertical { get; protected set; }

    public bool Moving => Math.Abs(Horizontal) + Math.Abs(Vertical) > 0;

    public void Update()
    {
        CalculateInput();
    }

    protected abstract void CalculateInput();

}
