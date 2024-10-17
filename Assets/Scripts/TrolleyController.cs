using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Metadata;
using static UnityEngine.Rendering.DebugUI;

public class TrolleyController : MonoBehaviour
{
    public TransformSynchronizer transformSync;
    public HookController hook;
    public Transform concrete;

    public Transform nearLimitObject, farLimitObject;
    private Vector3 nearLimit, farLimit;
    private float maxDistanceError = 0.1f;

    private float trolleySpeed = 5f;

    public void MoveTrolley(float value)
    {
        nearLimit = nearLimitObject.position;
        farLimit = farLimitObject.position;

        Vector3 trolleyPos = Vector3.Lerp(farLimit, nearLimit, value);
        transform.position = trolleyPos;

        TrolleySync();
    }

    public IEnumerator MoveTrolleyTowards(Transform target)
    {
        nearLimit = nearLimitObject.position;
        farLimit = farLimitObject.position;

        Vector3 trolleyPosProjected = Vector3.ProjectOnPlane(transform.position, Vector3.up);
        Vector3 targetPosProjected = Vector3.ProjectOnPlane(concrete.position, Vector3.up);

        float distance = Vector3.Distance(trolleyPosProjected, targetPosProjected);

        while (distance > maxDistanceError)
        {
            Vector3 direction = (targetPosProjected - trolleyPosProjected).normalized;

            Vector3 newTrolleyPos = trolleyPosProjected + direction * trolleySpeed * Time.deltaTime;
            newTrolleyPos.y = transform.position.y;

            // Clamp position between limits
            newTrolleyPos = Vector3.ClampMagnitude(newTrolleyPos - nearLimit, Vector3.Distance(farLimit, nearLimit)) + nearLimit;
            transform.position = newTrolleyPos;

            // Update position and distance
            trolleyPosProjected = Vector3.ProjectOnPlane(transform.position, Vector3.up);
            distance = Vector3.Distance(trolleyPosProjected, targetPosProjected);

            TrolleySync();

            yield return null;
        }
    }

    private void TrolleySync()
    {
        // Syncs cable with trolley
        transformSync.SyncTransform(transformSync.transformPairs[1], transformSync.transformPairs[1].children[0], 0);

        // Syncs hook with cable
        transformSync.SyncTransform(transformSync.transformPairs[2], transformSync.transformPairs[2].children[0], 0);

        // Syncs concrete with hook
        if (hook.isHooked)
        {
            transformSync.SyncTransform(transformSync.transformPairs[3], transformSync.transformPairs[3].children[0], 0);
        }

        transformSync.UpdateAllRelativeTransforms();
    }
}
