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

    private void Teleport()
    {
        Vector3 newPos = GetRandomPosition();
        Vector3 rotatedPos = GetRotatedPosition(newPos);
        transform.position = rotatedPos;

    }

    // Generates a random position between trolley limits
    private Vector3 GetRandomPosition()
    {

        Vector3 nearLimit = trolley.nearLimitObject.position;
        Vector3 farLimit = trolley.farLimitObject.position;

        float randomFactor = Random.Range(0f, 1f);

        Vector3 newPos = Vector3.Lerp(farLimit, nearLimit, randomFactor);
        newPos.y = Random.Range(10f, 20f);

        return newPos;
    }

    // Calculates a rotated position around the crane based on a new position
    private Vector3 GetRotatedPosition(Vector3 newPos)
    {
        Vector3 cranePos = crane.transform.position;

        transform.position = newPos;

        float maxReach = Vector3.Distance(cranePos, trolley.farLimitObject.position);
        float radius = Mathf.Min(maxReach, Vector3.Distance(cranePos, newPos));

        float randomAngle = Random.Range(0f, 2 * Mathf.PI);

        float newX = cranePos.x + radius * Mathf.Cos(randomAngle);
        float newZ = cranePos.z + radius * Mathf.Sin(randomAngle);

        Vector3 rotatedPos = new Vector3(newX, newPos.y, newZ);

        return rotatedPos;
    }
}
