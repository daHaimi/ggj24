using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMarker : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        var forward = transform.forward;
        Debug.DrawRay(transform.position,forward * 2, Color.red);
    }
}
