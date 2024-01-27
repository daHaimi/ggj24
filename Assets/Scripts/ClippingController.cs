using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class ClippingController : MonoBehaviour
{
    public GameObject player;

    private float _playerDistance;
    private int _clippingLayer;
    private GameObject _clippedObject;

    void Start()
    {
        _playerDistance = Vector3.Distance(player.transform.position, this.transform.position);
        _clippingLayer = LayerMask.NameToLayer("ClipObject");
    }
    
    void LateUpdate()
    {
        var ray = new Ray(this.transform.position, this.transform.forward);
        if (Physics.Raycast(ray, out var hit))
        {
            var other = hit.transform.gameObject;
            if (Vector3.Distance(other.transform.position, this.transform.position) < _playerDistance)
            {
                if (other.layer == _clippingLayer)
                {
                    if (_clippedObject != other)
                    {
                        _clippedObject = other;
                        HideObject();
                    }
                }
                else
                {
                    ShowObject();
                    _clippedObject = null;
                }
            }
            else
            {
                ShowObject();
                _clippedObject = null;
            }
        }
    }

    void HideObject()
    {
        if (!_clippedObject)
            return;
        
        _clippedObject.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly ;
        
        // var objColor = new Color(1, 1, 1, 0.3f);
        // _clippedObject.GetComponent<MeshRenderer>().material.color = objColor;


        // foreach (var material in _clippedObject.GetComponent<MeshRenderer>().materials)
        // {
        //     var objColor = new Color(material.color.r, material.color.g, material.color.b, 0.3f);
        //     material.color = objColor;
        // }
    }

    void ShowObject()
    {
        if (!_clippedObject)
            return;

        _clippedObject.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.On ;
        
        // var objColor = new Color(1, 1, 1, 1f);
        // _clippedObject.GetComponent<MeshRenderer>().material.color = objColor;
        
        // foreach (var material in _clippedObject.GetComponent<MeshRenderer>().materials)
        // {
        //     var objColor = new Color(material.color.r, material.color.g, material.color.b, 1f);
        //     material.color = objColor;
        // }
    }
}
