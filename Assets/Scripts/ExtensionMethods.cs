using UnityEngine;

namespace Assets.Scripts
{
    public static class ExtensionMethods
    {
        public static bool IsInSightOf(this GameObject fromGo, GameObject toGo)
        {
            Vector3 direction = toGo.transform.position - fromGo.transform.position;
            Ray ray = new Ray(fromGo.transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hit))
                return hit.collider.gameObject == toGo;

            return true;

        }

    }
}
