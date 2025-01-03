using UnityEngine;

namespace Helpers
{
    public static class MeshHelper
    {
        public static float GetMaxMeshY(Transform parent)
        {
            float maxY = float.MinValue;

            MeshFilter[] meshFilters = parent.GetComponentsInChildren<MeshFilter>(true); // Get all child MeshFilters.

            foreach (MeshFilter meshFilter in meshFilters)
            {
                if (meshFilter.sharedMesh != null && meshFilter.gameObject.activeInHierarchy)
                {
                    Bounds meshBounds = meshFilter.sharedMesh.bounds;

                    // Transform local bounds to world space.
                    Vector3 worldCenter = parent.TransformPoint(meshBounds.center);

                    // Calculate the maximum Y coordinate.
                    float maxYOfMesh = worldCenter.y + meshBounds.extents.y;

                    // Update the maximum Y if this mesh has a higher Y-coordinate.
                    if (maxYOfMesh > maxY)
                    {
                        maxY = maxYOfMesh;
                    }
                }
            }

            return maxY;
        }
    }
}
