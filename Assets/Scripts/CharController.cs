using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpForce = 500f;

    private const float gravity = 9.81f;
    
    private Vector3 forward, right;
    private Rigidbody body;
    private bool isFalling;

    // Start is called before the first frame update
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.anyKey)
        {
            Move();
        }
    }

    private void Move()
    {
        if (Input.GetButtonDown("Jump") && !isFalling)
        {
            body.AddForce(Vector3.up * jumpForce,ForceMode.Impulse);
            isFalling = true;
        }
        var rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal Key");
        var upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("Vertical Key");
        var heading = Vector3.Normalize(rightMovement + upMovement);
        //transform.forward = heading;
        body.AddForce((heading * moveSpeed) - body.velocity, ForceMode.VelocityChange);
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        isFalling = false;
    }
}
