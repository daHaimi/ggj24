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
        List<GameObject> currentHits = new();
        var ray = new Ray(this.transform.position, this.transform.forward);

        foreach (RaycastHit hit in Physics.RaycastAll(ray, _playerDistance - 1, _clippingLayerMask))
        {
            var other = hit.transform.gameObject;
            currentHits.Add(other);

            if (!_clippedObjects.Contains(other))
            {
                other.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.ShadowsOnly;
                _createBasement(other);
                _clippedObjects.Add(other);
            }

        }

        foreach (GameObject other in _clippedObjects.ToList())
        {
            if (currentHits.Contains(other))
                continue;

            Destroy(other.GetComponentInChildren<ClippingBasement>().gameObject);
            _clippedObjects.Remove(other);
            other.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.On;
        }

    }

    private void _createBasement(GameObject other)
    {
        var basement = GameObject.CreatePrimitive(PrimitiveType.Cube);
        basement.AddComponent<ClippingBasement>();
        var t = basement.transform;
        t.parent = other.transform;
        t.localPosition = new Vector3(0, 0, 0);
        t.localRotation = Quaternion.Euler(0, 0, 0);
        basement.GetComponent<MeshFilter>().mesh = other.GetComponent<MeshFilter>().mesh;
        t.localScale = new Vector3(1, .02f, 1);
        Material baseMat = Resources.Load<Material>("Basement");
        var mats = Enumerable.Repeat(baseMat, other.GetComponent<MeshRenderer>().materials.Length).ToArray();
        basement.GetComponent<MeshRenderer>().receiveShadows = false;
        basement.GetComponent<MeshRenderer>().materials = mats;
    }

}
