using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class MovePath : MonoBehaviour
{
    public Transform[] waypoints;
    public LineRenderer line;
    public float waitTimeAtWaypoint = 0;

    public AnimatorController idleAnimation;
    public AnimatorController walkAnimation;
    
    private int waypointIdx;
    private Vector3 target;
    private float _yetToWait = 0;
    private bool walking = false;
    
    private NavMeshAgent agent;
    private Animator _animator;


    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        target = waypoints[waypointIdx].position;
        UpdateDestination();
    }

    private void Update()
    {
        // Nicht wartend
        if (_yetToWait <= 0)
        {
            // Angekommen
            if (Vector3.Distance(transform.position, target) < 1)
            {
                Debug.Log("Reached Waypoint");
                _animator.runtimeAnimatorController = idleAnimation;
                IterateIndex();
                _yetToWait = waitTimeAtWaypoint;
                walking = false;
            }
            else if (!walking)
            {
                Debug.Log("To next Waypoint");
                walking = true;
                _animator.runtimeAnimatorController = walkAnimation;
                UpdateDestination();
            }
        } 
        else
        {
            _yetToWait -= Time.deltaTime;
        }
    }

    public void UpdateDestination()
    {
        agent.SetDestination(target);
    }

    public void IterateIndex()
    {
        if (waypoints.Length < 1) return; 
        waypointIdx = (waypointIdx + 1) % waypoints.Length;
        target = waypoints[waypointIdx].position;
    }
    private void OnDrawGizmos()
    {
        if(line == null || waypoints.Length < 2) //if the path has 1 or no corners, there is no need
            return;

        line.positionCount = waypoints.Length; //set the array of positions to the amount of corners

        for(var i = 0; i < waypoints.Length; i++){
            line.SetPosition(i, waypoints[i].position);
        }
    }
}