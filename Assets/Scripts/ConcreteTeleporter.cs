using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.UI.Image;

public class ConcreteTeleporter : MonoBehaviour
{
    public TrolleyController trolley;
    public CraneController crane;

    private float concreteHeightMin = 10f;
    private float concreteHeightMax = 20f;

    void Start()
    {
        Teleport();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Teleport();
        }
    }

 
    public void Teleport()
    {
        Vector3 nearLimit = trolley.nearLimitObject.position;
        Vector3 farLimit = trolley.farLimitObject.position;

        // Generate random position between the trolley limits
        float randomFactor = Random.Range(0f, 1f);
        Vector3 newPos = Vector3.Lerp(farLimit, nearLimit, randomFactor);
        newPos.y = Random.Range(concreteHeightMin, concreteHeightMax);
        
        Vector3 cranePos = crane.transform.position;

        // Calculate radius, ignore Y component
        Vector3 cranePosXZ = new Vector3(cranePos.x, 0f, cranePos.z);
        Vector3 newPosXZ = new Vector3(newPos.x, 0f, newPos.z);

        float radius = Vector3.Distance(cranePosXZ, newPosXZ);

        // Calculate random rotated position around crane
        float randomAngle = Random.Range(0f, 2 * Mathf.PI);

        float newX = cranePos.x + radius * Mathf.Cos(randomAngle);
        float newZ = cranePos.z + radius * Mathf.Sin(randomAngle);

        // Set new position
        newPos = new Vector3(newX, newPos.y, newZ);
        transform.position = newPos;

    }
}
