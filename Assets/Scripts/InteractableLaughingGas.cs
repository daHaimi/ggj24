using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableLaughingGas : Interactable
{
    public float RefillAmount = 10;

    public override void Interact()
    {
        CharController.Instance.Laughing.Refill(RefillAmount);
        Destroy(gameObject);
    }
}
