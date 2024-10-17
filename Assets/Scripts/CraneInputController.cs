using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CraneInputController : MonoBehaviour
{
    public CraneController crane;
    public TrolleyController trolley;
    public CableController cable;


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

        yield return crane.RotateTowards(targetTransform);
        yield return trolley.MoveTrolleyTowards(targetTransform);
        yield return cable.AdjustCableScale(targetSphereCollider);
    }
}

   