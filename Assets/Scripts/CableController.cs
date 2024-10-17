using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

public class CableController : MonoBehaviour
{
    public TransformSynchronizer transformSync;
    public Collider hook;
    private float scaleIncrement = 0.4f;
    private float minScale = 0.04f;

    public void ScaleCable(float cableScale)
    {
        transform.localScale = new Vector3(transform.localScale.x, cableScale, transform.localScale.z);
        transformSync.SyncAllTransforms();
    }

    public IEnumerator AdjustCableScale(Collider target)
    {
        float cableScale = transform.localScale.y;
        float targetPos = target.bounds.center.y;
        float hookPos = hook.bounds.center.y;

        while (hookPos > targetPos)
        {
            cableScale += scaleIncrement * Time.deltaTime;
            ScaleCable(cableScale);
            hookPos = hook.bounds.center.y;
            yield return null;
        }

        yield return new WaitForSeconds(1);

        while (cableScale > minScale)
        {
            cableScale -= scaleIncrement * Time.deltaTime;
            ScaleCable(Mathf.Max(cableScale, minScale));
            yield return null;
        }
    }
}
