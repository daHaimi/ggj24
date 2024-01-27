using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriving : MonoBehaviour
{
    public float Speed = 5f;
    public float DestroyAfterDistance = 120;

    private Vector3 _initialPos;

    void Start()
    {
        _initialPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);

        if (Vector3.Distance(_initialPos, transform.position) > DestroyAfterDistance)
            Destroy(gameObject);
    }
}
