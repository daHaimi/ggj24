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

    public bool IsLaughing
    {
        get;
        private set;
    }
    private GameObject _actualSmiley;

    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Pause();
    }

    void Update()
    {
        if (IsLaughing)
        {
            LaughMeter -= Time.deltaTime;
            if (LaughMeter <= 0)
                StopLaughing();
        }
    }

    public void Refill(float amount)
    {
        LaughMeter += amount;
        if(LaughMeter > LaughMeterMax)
            LaughMeter = LaughMeterMax;

        AudioController.Instance.PlaySound(GlobalDataSo.Instance.SfxRefillLaughMeter);
    }

    public void StartLaughing()
    {
        if (IsLaughing || LaughMeter <= 0) return;

        _audioSource.Play();

        IsLaughing = true;
        _actualSmiley = Instantiate(contents[Random.Range(0, contents.Count)], Vector3.zero, Quaternion.identity, smileyCanvas.GetComponent<RectTransform>());
        var rectTransform = _actualSmiley.GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.one;
    }

    public void StopLaughing()
    {
        if (!IsLaughing) return;

        _audioSource.Pause();

        IsLaughing = false;
        Destroy(_actualSmiley);
    }

}
