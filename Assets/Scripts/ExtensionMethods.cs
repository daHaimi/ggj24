using UnityEngine;

namespace Assets.Scripts
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Check if one GameObject is in sight of another,
        /// without physcical obstacles in between.
        /// </summary>
        /// <param name="fromGo">From GameObject</param>
        /// <param name="toGo">To GameObject</param>
        /// <returns>True, if there's no collider in between.</returns>
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
