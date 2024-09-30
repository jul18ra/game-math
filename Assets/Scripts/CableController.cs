using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CableController : MonoBehaviour
{
    public TransformSynchronizer transformSync;

    public void ScaleCable(float cableScale)
    {
        transform.localScale = new Vector3(transform.localScale.x, cableScale, transform.localScale.z);
        transformSync.SyncAllTransforms();
    }
}
