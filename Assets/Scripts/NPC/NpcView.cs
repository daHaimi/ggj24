using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private uint _gotOne = 0;
    private MeshFilter _mesh;
    private float _hStartAngle, _hAnglePerStep, _vStartAngle;
    private GameObject _viewField;

    private void Start()
    {
        _viewField = new GameObject();
        _viewField.transform.parent = transform;
        _viewField.transform.localPosition = new Vector3(0, height / 2, 0);
        _mesh = _viewField.AddComponent<MeshFilter>();
        _viewField.AddComponent<MeshRenderer>().material = Resources.Load<Material>("Marker");
        _calcValues();
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
        var tris = new List<int>(); // Every 3 ints represents a triangle
        var points = new List<Vector3>(); // Vertex position in world space
        
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
                    points.Add(_calcPoint(i - 1));
            }
            points.Add(_calcPoint(i - 1));

            var mesh = new Mesh
            {
                vertices = points.ToArray(),
                triangles = tris.ToArray()
            };
            mesh.RecalculateNormals();
            return mesh;
    }

    private Vector3 _calcPoint(int x)
    {
        Vector3 f = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * Vector3.forward;
        f.y = _hStartAngle + (x * _hAnglePerStep);
        return Quaternion.Euler(f) * transform.forward * viewDistance;
    }

    public Vector3 _calcPointUntilRayHit(int x)
    {
        Vector3 f = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * Vector3.forward;
        f.y = _hStartAngle + (x * _hAnglePerStep);
        if (Physics.Raycast(
                transform.position + (Vector3.up * height),
                Quaternion.Euler(f) * transform.forward,
                out RaycastHit hitInfo,
                viewDistance))
        {
            return _viewField.transform.InverseTransformPoint(hitInfo.point);
        }
        return Quaternion.Euler(f) * transform.forward * viewDistance;
    }

    private void _recalcMesh()
    {
        var points = new List<Vector3>(); // Vertex position in world space
        // Ursprung
        points.Add(Vector3.zero);
        var perRow = (int)scanDensity + 1;
        int i;
        for (i = 1; i < perRow; i++)
        {
            points.Add(_calcPointUntilRayHit(i - 1));
        }
        points.Add(_calcPointUntilRayHit(i - 1));
        
        // Set new mesh
        _mesh.mesh = new Mesh
        {
            vertices = points.ToArray(),
            triangles = _mesh.mesh.triangles
        };
        _mesh.mesh.RecalculateNormals();
    }

    private void Update()
    {
        _recalcMesh();
        _viewField.transform.localRotation = Quaternion.Euler(0, -transform.rotation.eulerAngles.y, 0);
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
                    viewDistance))
                { 
                    var x = (1 << hitInfo.collider.gameObject.layer) & layer;
                    Debug.Log(hitInfo.collider.gameObject.layer);
                    Debug.Log(layer);
                    if (x != 0 && _gotOne <= 0)
                    {
                        Debug.Log("asdfasdf");
                        hitInfo.transform.parent.gameObject.SendMessage("Busted", gameObject);
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
        var forward = transform.forward;
        _calcValues();
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
