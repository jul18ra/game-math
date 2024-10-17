using GameMath.UI;
using System.Collections;
using UnityEngine;

public class CraneController : MonoBehaviour
{
    public TransformSynchronizer transformSync;
    public TrolleyController trolley;


    private Vector3 yAxis = new(0, 1, 0);
    public HoldableButton leftButton, rightButton;
    private int craneSpeed = 20;

    public bool isRotating = false;
    private float maxAngleError = 0.1f;

    private void Update()
    {
        RotateCrane();
    }

    private void RotateCrane()
    {
        float rotationDirection = 0f;

        if (leftButton.IsHeldDown)
        {
            rotationDirection = craneSpeed;
        }
        else if (rightButton.IsHeldDown)
        {
            rotationDirection = -craneSpeed;
        }

        if (rotationDirection != 0f)
        {
            transform.Rotate(yAxis, rotationDirection * Time.deltaTime);
            transformSync.SyncAllTransforms();
        }
    }

    public IEnumerator RotateTowards(Transform target)
    {
        isRotating = true;

        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        Quaternion targetRot = Quaternion.LookRotation(direction);

        // Adjust target rotation to align with crane's orientation
        targetRot *= Quaternion.Euler(0, 90, 0); 

        while (isRotating) 
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, craneSpeed * Time.deltaTime);
            transformSync.SyncAllTransforms();

            if (Quaternion.Angle(transform.rotation, targetRot) < maxAngleError)
            {
                isRotating = false;
            }

            yield return null;

        }
    }
}
