using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Simple Interactable that invokes a Unity Event.
/// </summary>
public class InteractableUnityEvent : Interactable
{
    public UnityEvent OnInteraction = new();

    void Start()
    {
        gameObject.tag = GlobalDataSo.TAG_INTERACTABLE;
    }

    public override void Interact()
    {
        OnInteraction.Invoke();
    }
}
