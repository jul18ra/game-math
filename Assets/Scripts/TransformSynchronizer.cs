using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// The TransformSynchronizer class syncs the positions 
// and rotations of specified child transforms in relation to a parent transform.

public class TransformSynchronizer : MonoBehaviour
{
    private Vector3[] relativePositions;
    private Quaternion[] relativeRotations;

    public Transform trolley, nearLimit, farLimit, cable, hook;

    private Transform[] craneChildren;
    public Transform[] CraneChildren { get { return craneChildren; } }

    private void Start()
    {
        craneChildren = new Transform[]
        {
            trolley, nearLimit, farLimit
        };

        relativePositions = new Vector3[craneChildren.Length];
        relativeRotations = new Quaternion[craneChildren.Length];
    }

    public void UpdateRelativeTransform(Transform[] children, Transform parent)
    {
        for (int i = 0; i < children.Length; i++)
        {
            relativePositions[i] = parent.InverseTransformPoint(children[i].position);
            relativeRotations[i] = Quaternion.Inverse(parent.rotation) * children[i].rotation;
        }
    }

    public void SyncTransform(Transform[] children, Transform parent)
    {
        for (int i = 0; i < children.Length; i++)
        {
            children[i].position = parent.TransformPoint(relativePositions[i]);
            children[i].rotation = parent.rotation * relativeRotations[i];
        }

    }

}
