using System.Collections.Generic;
using TowerDefence.Core;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] private TreePool _treePool;
    [SerializeField] private List<Transform> _transforms;

    private void Awake()
    {
        foreach(var treeTransform in _transforms)
        {
            var tree = _treePool.Get();
            tree.transform.position = treeTransform.position;
            tree.gameObject.SetActive(true);
        }

        // Physics2D.SyncTransforms(); // Ensure that the physics engine is aware of the new positions
    }

}
