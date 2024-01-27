using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class ClippingController : MonoBehaviour
{
    public GameObject player;

    private float _playerDistance;
    private LayerMask _clippingLayerMask;
    private List<GameObject> _clippedObjects = new();

    void Start()
    {
        _playerDistance = Vector3.Distance(player.transform.position, this.transform.position);
        _clippingLayerMask = LayerMask.GetMask("ClipObject");
    }

    void LateUpdate()
    {
        var ray = new Ray(this.transform.position, this.transform.forward);
        // if (Physics.RaycastAll(ray, out var hit))

        List<GameObject> currentHits = new();

        foreach (RaycastHit hit in Physics.RaycastAll(ray, _playerDistance - 1, _clippingLayerMask))
        {
            var other = hit.transform.gameObject;
            currentHits.Add(other);

            if (!_clippedObjects.Contains(other))
            {
                other.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;
                _clippedObjects.Add(other);
            }

        }

        foreach (GameObject other in _clippedObjects.ToList())
        {
            if (currentHits.Contains(other))
                continue;

            _clippedObjects.Remove(other);
            other.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.On;
        }

    }

}
