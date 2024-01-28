using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    public float RotateFrom = 220;
    public float RotateTo = 240;
    public float RotationTime = 30;

    void Start()
    {
        gameObject.LeanValue(RotateFrom, RotateTo, RotationTime)
            .setEaseInOutSine()
            .setLoopPingPong()
            .setOnUpdate((float val) => {
                gameObject.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, val, transform.rotation.eulerAngles.z);
            });

    }
}
