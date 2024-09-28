using UnityEngine;
using static Unity.VisualScripting.Metadata;
using static UnityEngine.Rendering.DebugUI;

public class TrolleyController : MonoBehaviour
{
    public TransformSynchronizer transformSync;

    public Transform nearLimitObject, farLimitObject;
    private Vector3 nearLimit, farLimit;

    public Transform crane;

    public void MoveTrolley(float value)
    {
        nearLimit = nearLimitObject.position;
        farLimit = farLimitObject.position;

        Vector3 trolleyPos = Vector3.Lerp(farLimit, nearLimit, value);
        transform.position = trolleyPos;
        transformSync.UpdateRelativeTransform(transformSync.CraneChildren, crane);
    }
}
