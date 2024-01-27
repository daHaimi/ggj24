using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;

public class NpcView : MonoBehaviour
{
    public float height = 1.7f;
    public float horizontalAngle = 90.0f;
    public float verticalAngle = 20.0f;
    public uint scanDensity = 5;
    public float viewDistance = 12.0f;
    public LayerMask layer;
    public UnityEvent hitCallback;
    public Transform viewField;

    private uint _gotOne = 0;
    private MeshFilter _mesh;
    private float _hStartAngle, _hAnglePerStep, _vStartAngle;

    private void Start()
    {
        _calcValues();
        _mesh = viewField.GetComponent<MeshFilter>();
        _mesh.mesh = _createViewfieldPolygon();
    }

    private void _calcValues()
    {
        _hStartAngle = -(horizontalAngle / 2);
        _hAnglePerStep = horizontalAngle / scanDensity;
        _vStartAngle = -(verticalAngle / 2);
    }
    
    private Mesh _createViewfieldPolygon()
    {
        var forward = transform.forward;
        var tris = new List<int>(); // Every 3 ints represents a triangle
        var points = new List<Vector3>(); // Vertex position in world space
        var uvs = new List<Vector2>(); // Vertex position in 0-1 UV space
        
        // Top/Side Triangles   // Front Triangles
            /*   0                  1  2  3
                 *                  *--*--*
                /|\                 | /| /|
               / | \                |/ |/ |
            3 *--*--* 1             *--*--*
                 2                  4  5  6
            */
        points.Add(Vector3.zero);
            var perRow = (int)scanDensity + 1;
            int i;
            var row = 0;
            for (i = 1; i < perRow; i++)
            {
                    tris.Add(0);
                    tris.Add(i + row * perRow);
                    tris.Add(i + 1 + row * perRow);
                    points.Add(_calcPoint(i - 1, row));
            }
            points.Add(_calcPoint(i - 1, row));
            row = 1;
            for (i = 1; i < perRow; i++)
            {
                    tris.Add(0);
                    tris.Add(i + 1 + row * perRow);
                    tris.Add(i + row * perRow);
                    points.Add(_calcPoint(i - 1, row));
            }
            points.Add(_calcPoint(i-1, row));
            // Sides:
            tris.Add(0);
            tris.Add(1 + perRow);
            tris.Add(1);
            tris.Add(0);
            tris.Add(perRow);
            tris.Add(2 * perRow);
            // Front
            for (int k = 1; k < perRow; k++)
            {
                // Right bottom
                tris.Add(k);
                tris.Add(k + perRow);
                tris.Add(k + perRow + 1);
                // Left top
                tris.Add(k + 1);
                tris.Add(k);
                tris.Add(k + perRow + 1);
            }

            Mesh mesh = new Mesh
            {
                vertices = points.ToArray(),
                triangles = tris.ToArray()
            };
            mesh.RecalculateNormals();
            return mesh;
    }

    private Vector3 _calcPoint(int x, int y)
    {
        Vector3 f = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * new Vector3(_vStartAngle + (y * verticalAngle) ,0, 0);
        f.y = _hStartAngle + (x * _hAnglePerStep);
        var result = Quaternion.Euler(f) * transform.forward * viewDistance;
        return result;
    }

    void FixedUpdate ()
    {
        Vector3 forward = transform.forward;
        for (int i = 0; i <= scanDensity; i++)
        {
            for (var j = 0; j <= 1; j++) 
                if (Physics.Raycast(
                    transform.position + (Vector3.up * height),
                    Quaternion.Euler(_vStartAngle + (j * verticalAngle), _hStartAngle + (i * _hAnglePerStep), 0) * forward,
                    out RaycastHit hitInfo,
                    viewDistance,
                    layer))
                {
                    if (_gotOne <= 0)
                    {
                        hitCallback.Invoke();
                        _gotOne = 5;
                        return;
                    }
                    _gotOne = (_gotOne - 1) % 5;
                    return;
                }
        }

        _gotOne = (_gotOne - 1) % 5;

    }
    
    private void OnDrawGizmos()
    {
        _calcValues();
        var forward = transform.forward;
        for (var i = 0; i <= scanDensity; i++)
        {
            for (var j = 0; j <= 1; j++)
            {
                var f = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * new Vector3(_vStartAngle + (j * verticalAngle) ,0, 0);
                f.y = _hStartAngle + (i * _hAnglePerStep);
                Debug.DrawRay(
                    transform.position + (Vector3.up * height),
                    Quaternion.Euler(f) * forward * viewDistance, Color.green);
            }
        }
    }
}
