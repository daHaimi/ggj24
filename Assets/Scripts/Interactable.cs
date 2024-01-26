using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    void Start()
    {
        gameObject.tag = GlobalDataSo.TAG_INTERACTABLE;
    }

    public abstract void Interact();
}
