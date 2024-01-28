using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Interactable class for all Interactables.
/// Interactables allow the Player to Interact with
/// with them by a simple method.
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    void Start()
    {
        gameObject.tag = GlobalDataSo.TAG_INTERACTABLE;
    }

    /// <summary>
    /// Interact method, executed by the Player.
    /// </summary>
    public abstract void Interact();
}
