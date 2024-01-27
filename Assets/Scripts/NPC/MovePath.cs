using System.Collections.Generic;
using UnityEngine;

public class MovePath : MonoBehaviour
{
    [HideInInspector] public float _walkPointThreshold = 0.5f;
    [HideInInspector] public int w;
    [HideInInspector] public bool forward = true;
    [HideInInspector] public Vector3 finishPos;
    [HideInInspector] public int targetPointsTotal;
    [HideInInspector] public bool loop;
    public LinkedList<Vector3> points = new();

    private int _nextTargetIndex;

    public void InitStartPosition(int _w, int _i)
    {
        forward = true;

        w = _w;
        targetPointsTotal = points.Count - 2;

        if (_i < targetPointsTotal && _i > 0)
        {
            if (forward)
            {
                _nextTargetIndex = _i + 1;
                //finishPos = points.Find(w, _i + 1);
            }
            else
            {
                _nextTargetIndex = _i;
                //finishPos = _WalkPath.getNextPoint(w, _i);
            }
        }
        else
        {
            if (forward)
            {
                _nextTargetIndex = 1;
                //finishPos = _WalkPath.getNextPoint(w, 1);
            }
            else
            {
                _nextTargetIndex = targetPointsTotal;
               // finishPos = _WalkPath.getNextPoint(w, targetPointsTotal);
            }
        }
    }

    public void SetLookPosition()
    {
        Vector3 targetPos = new Vector3(finishPos.x, transform.position.y, finishPos.z);
        transform.LookAt(targetPos);
    }
}