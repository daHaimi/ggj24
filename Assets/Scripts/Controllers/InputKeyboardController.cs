using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyboardController : InputController
{
    protected override void CalculateInput()
    {
        // TODO: Integrate Input.GetAxis etc.
        Horizontal = Input.GetKey(KeyCode.D) ? 1 :
            Input.GetKey(KeyCode.A) ? -1 : 0;
        Vertical = Input.GetKey(KeyCode.W) ? 1 :
            Input.GetKey(KeyCode.S) ? -1 : 0;
        Interact = Input.GetKeyDown(KeyCode.Return);
        Laugh = Input.GetKeyDown(KeyCode.Space);
    }
}
