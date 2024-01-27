using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MovePath : MonoBehaviour
{

    public Transform[] waypoints;
    public LineRenderer line;
    
    private int waypointIdx;
    private Vector3 target;
    
    private NavMeshAgent agent;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target) < 1)
        {
            IterateIndex();
            UpdateDestination();
        }
    }

    public void UpdateDestination()
    {
        target = waypoints[waypointIdx].position;
        agent.SetDestination(target);
    }

    public void IterateIndex()
    {
        if (waypoints.Length < 1) return; 
        waypointIdx = (waypointIdx + 1) % waypoints.Length;
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