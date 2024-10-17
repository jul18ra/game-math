using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CraneActionManager : MonoBehaviour
{
    public CraneController crane;
    public TrolleyController trolley;
    public CableController cable;
    public HookController hook;
    public ConcreteTeleporter concrete;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
        }
    }

    private void CastRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        // Check if ray hits an object
        if (Physics.Raycast(ray, out hit))
        {
            HandleHitObject(hit);
        }
    }

    private void HandleHitObject(RaycastHit hit)
    {
        if (hit.collider.CompareTag("Concrete") && !crane.isRotating)
        {
            GameObject concrete = hit.transform.gameObject;
            StartCoroutine(CraneSequence(concrete));
        }
    }

    private IEnumerator CraneSequence(GameObject target)
    {
        Transform targetTransform = target.transform;
        Collider targetSphereCollider = target.GetComponent<SphereCollider>();

        yield return crane.RotateCraneTowards(targetTransform);
        yield return trolley.MoveTrolleyTowards(targetTransform);
        yield return cable.AdjustCableScale(targetSphereCollider);

        yield return new WaitForSeconds(1);

        hook.Unhook();
        concrete.Teleport();
    }
}

   