using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class Laughing : MonoBehaviour
{
    public Canvas smileyCanvas;
    public List<GameObject> contents;

    /// <summary>
    /// Laugh meter from 0-10
    /// </summary>
    public float LaughMeterMax = 10;
    public float LaughMeter = 10;

    private bool _laughingActive = false;
    private GameObject _actualSmiley;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Pause();
    }

    void Update()
    {
        if (_laughingActive)
        {
            LaughMeter -= Time.deltaTime;
            if (LaughMeter <= 0)
                StopLaughing();
        }
    }

    public void StartLaughing()
    {
        if (_laughingActive || LaughMeter <= 0) return;

        _audioSource.Play();

        _laughingActive = true;
        _actualSmiley = Instantiate(contents[Random.Range(0, contents.Count)], Vector3.zero, Quaternion.identity, smileyCanvas.GetComponent<RectTransform>());
        var rectTransform = _actualSmiley.GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
    }

    public void StopLaughing()
    {
        if (!_laughingActive) return;

        _audioSource.Pause();

        _laughingActive = false;
        Destroy(_actualSmiley);
    }

}
