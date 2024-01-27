using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMoving : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward * 1 * Time.deltaTime);
    }
}
