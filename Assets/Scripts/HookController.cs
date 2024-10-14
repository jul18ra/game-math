using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    public TransformSynchronizer transformSync;
    public bool isHooked = false;

    private void OnTriggerEnter(Collider concrete)
    {
        AttachToHook(concrete);
    }

    private void AttachToHook(Collider collider)
    {
        if (!isHooked)
        {
            Transform[] concreteTransform = { collider.transform };
            transformSync.AddTransformPair(transform, concreteTransform);
            isHooked = true;
        }
    }
}
