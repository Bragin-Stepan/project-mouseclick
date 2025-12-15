using UnityEngine;

public static class RaycastUtils
{
    public static bool TryGetHitWithMask(Camera camera,Vector3 pointPosition, LayerMask layerMask, out RaycastHit hit)
    {
        Ray ray = camera.ScreenPointToRay(pointPosition);
        hit = default;

        if (layerMask > 0 && Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask.value))
        {
            hit = raycastHit;
            return true;
        }

        return false;
    }

    public static bool TryGetHit(Camera camera, out RaycastHit hit)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        hit = default;

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            hit = raycastHit;
            return true;
        }

        return false;
    }
}