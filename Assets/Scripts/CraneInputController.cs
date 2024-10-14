using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CraneInputController : MonoBehaviour
{
    public CraneController crane;
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
            Transform concrete = hit.collider.gameObject.transform;
            StartCoroutine(crane.RotateTowards(concrete));
        }
    }
}
