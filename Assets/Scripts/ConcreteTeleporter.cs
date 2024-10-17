using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using static UnityEngine.UI.Image;

public class ConcreteTeleporter : MonoBehaviour
{
    public TrolleyController trolley;
    public CraneController crane;      
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
        newPos.y = Random.Range(10f, 20f);

        // Calculate rotated position around the crane
        Vector3 cranePos = crane.transform.position;
        float maxReach = Vector3.Distance(cranePos, farLimit);
        float radius = Mathf.Min(maxReach, Vector3.Distance(cranePos, newPos));
        float randomAngle = Random.Range(0f, 2 * Mathf.PI);

        float newX = cranePos.x + radius * Mathf.Cos(randomAngle);
        float newZ = cranePos.z + radius * Mathf.Sin(randomAngle);

        // Set new position
        transform.position = new Vector3(newX, newPos.y, newZ);
    }
}
