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

    public List<TransformPair> transformPairs = new List<TransformPair>();

    public HookController hook;

    private void Start()
    {
        UpdateAllRelativeTransforms();
    }

    private void Update()
    {
        TrolleySync();
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

    public void SyncTransform(TransformPair pair, Transform child, int index)
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

    public void AddTransformPair(Transform parent, Transform[] children)
    {
        TransformPair newPair = new TransformPair
        {
            parent = parent,
            children = children
        };
        transformPairs.Add(newPair);
    }

    // Syncs parts of the trolley
    private void TrolleySync()
    {
        // Syncs cable with trolley
        SyncTransform(transformPairs[1], transformPairs[1].children[0], 0);

        // Syncs hook with cable
        SyncTransform(transformPairs[2], transformPairs[2].children[0], 0);

        // Syncs concrete with hook
        if (hook.isHooked)
        {
            SyncTransform(transformPairs[3], transformPairs[3].children[0], 0);
        }

        UpdateAllRelativeTransforms();
    }

}


