using GameMath.UI;
using UnityEngine;

public class CraneController : MonoBehaviour
{
    public TransformSynchronizer transformSync;

    private Vector3 yAxis = new(0, 1, 0);
    public HoldableButton leftButton, rightButton;
    public TrolleyController trolley;
    private int craneSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        transformSync.UpdateRelativeTransform(transformSync.CraneChildren, transform);
    }

    private void Update()
    {
        RotateCrane();
    }

    private void RotateCrane()
    {
        float rotationDirection = 0f;

        if (leftButton.IsHeldDown)
        {
            rotationDirection = craneSpeed * Time.deltaTime;
        }
        else if (rightButton.IsHeldDown)
        {
            rotationDirection = -craneSpeed * Time.deltaTime;
        }

        if (rotationDirection != 0f)
        {
            transform.Rotate(yAxis, rotationDirection);
            transformSync.SyncTransform(transformSync.CraneChildren, transform);
        }
    }
}
