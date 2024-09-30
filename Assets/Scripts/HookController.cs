using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookController : MonoBehaviour
{
    public TransformSynchronizer transformSync;
    public bool isHooked = false;

    private void OnTriggerEnter(Collider concrete)
    {
        if (!isHooked)
        {
            Transform[] concreteTransform = { concrete.transform };
            transformSync.AddTransformPair(transform, concreteTransform);
            isHooked = true;
        }
    }
}
