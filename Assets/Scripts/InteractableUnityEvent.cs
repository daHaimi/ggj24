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

    /// <summary>
    /// Player can interact and trigger a UnityEvent.
    /// </summary>
    public override void Interact()
    {
        OnInteraction.Invoke();
    }
}
