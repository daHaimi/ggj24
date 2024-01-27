using System;
using UnityEngine;

public class InputController : MonoBehaviour
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

    protected void CalculateInput()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        Interact = Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire1");
        Laugh = Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire2");
    }

}
