using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Metadata;
using static UnityEngine.Rendering.DebugUI;

public class TrolleyController : MonoBehaviour
{
    public TransformSynchronizer transformSync;
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

            yield return null;
        }
    }

}
