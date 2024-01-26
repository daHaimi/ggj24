using UnityEngine;
using UnityEngine.Events;

public class NpcView : MonoBehaviour
{
    public float height = 1.7f;
    public float horizontalAngle = 90.0f;
    public float verticalAngle = 20.0f;
    public uint scanDensity = 5;
    public uint vScanDensity = 2;
    public float viewDistance = 12.0f;
    public LayerMask layer;
    public UnityEvent hitCallback;
    
    void FixedUpdate ()
    {
        float startAngle = -(horizontalAngle / 2);
        float anglePerStep = horizontalAngle / scanDensity;
        var vStartAngle = -(verticalAngle / 2);
        var vAnglePerStep = verticalAngle / vScanDensity;
        Vector3 forward = transform.forward;
        for (int i = 0; i <= scanDensity; i++)
        {
            for (var j = 0; j <= vScanDensity; j++) 
                if (Physics.Raycast(
                    transform.position + (Vector3.up * height),
                    Quaternion.Euler(vStartAngle + (j * vAnglePerStep), startAngle + (i * anglePerStep), 0) * forward,
                    out RaycastHit hitInfo,
                    viewDistance,
                    layer))
                {
                    hitCallback.Invoke();
                }
        }
        
    }
    
    private void OnDrawGizmos()
    {
        var hStartAngle = -(horizontalAngle / 2);
        var hAnglePerStep = horizontalAngle / scanDensity;
        
        var vStartAngle = -(verticalAngle / 2);
        var vAnglePerStep = verticalAngle / vScanDensity;
        
        var forward = transform.forward;
        for (var i = 0; i <= scanDensity; i++)
        {
            for (var j = 0; j <= vScanDensity; j++)
                Debug.DrawRay(transform.position + (Vector3.up * height),
                    Quaternion.Euler(vStartAngle + (j * vAnglePerStep), hStartAngle + (i * hAnglePerStep), 0) * forward *
                    viewDistance, Color.green);
        }
    }
}
