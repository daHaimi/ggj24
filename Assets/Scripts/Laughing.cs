using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

/// <summary>
/// Simple Interactable that invokes a Unity Event.
/// </summary>
public class Laughing : MonoBehaviour
{
    public Canvas smileyCanvas;
    public List<GameObject> contents;

    private bool _laughingActive = false;
    private GameObject _actualSmiley;

    public void StartLaughing()
    {
        if (_laughingActive) return;

        _laughingActive = true;
        _actualSmiley = Instantiate(contents[Random.Range(0, contents.Count)], Vector3.zero, Quaternion.identity, smileyCanvas.GetComponent<RectTransform>());
        var rectTransform = _actualSmiley.GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
    }

    public void StopLaughing()
    {
        if (!_laughingActive) return;
        
        _laughingActive = false;
        Destroy(_actualSmiley);
    }

}
