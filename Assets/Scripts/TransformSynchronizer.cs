using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

// The TransformSynchronizer class syncs the positions 
// and rotations of specified child transforms in relation to a parent transform.

public class TransformSynchronizer : MonoBehaviour
{
    [System.Serializable]
    public class TransformPair
    {
        public Transform parent;
        public Transform[] children;

        internal List<Vector3> relativePositions = new List<Vector3>();
        internal List<Quaternion> relativeRotations = new List<Quaternion>();
    }

    public TransformPair[] transformPairs;

    private void Start()
    {
        UpdateAllRelativeTransforms();
    }

    public void UpdateRelativeTransform(TransformPair pair, Transform child)
    {
        pair.relativePositions.Add(pair.parent.InverseTransformPoint(child.position));
        pair.relativeRotations.Add(Quaternion.Inverse(pair.parent.rotation) * child.rotation);
    }

    public void UpdateAllRelativeTransforms()
    {
        foreach (var pair in transformPairs)
        {
            pair.relativePositions.Clear();
            pair.relativeRotations.Clear();

            foreach (var child in pair.children)
            {
                UpdateRelativeTransform(pair, child);
            }
        }
    }

    private void SyncTransform(TransformPair pair, Transform child, int index)
    {
        if (index >= 0 && index < pair.relativePositions.Count)
        {
            child.position = pair.parent.TransformPoint(pair.relativePositions[index]);
            child.rotation = pair.parent.rotation * pair.relativeRotations[index];
        }
    }

    public void SyncAllTransforms()
    {
        foreach (var pair in transformPairs)
        {
            for (int i = 0; i < pair.children.Length; i++)
                {
                    var child = pair.children[i];
                    SyncTransform(pair, child, i);
                }
            }
        }
    }
