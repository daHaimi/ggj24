using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButtonAppear : MonoBehaviour
{
    public float Delay = 0;
    public float Time = 0.5f;

    void Awake()
    {
        gameObject.transform.localScale = Vector3.zero;

        gameObject.LeanScale(Vector3.one, Time)
            .setEaseOutBounce()
            .setDelay(Delay);
    }
}
