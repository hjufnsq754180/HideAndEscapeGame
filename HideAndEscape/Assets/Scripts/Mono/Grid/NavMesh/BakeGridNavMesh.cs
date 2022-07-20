using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class BakeGridNavMesh : MonoBehaviour
{
    private NavMeshSurface _navMeshSurface;

    private void Start()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
        _navMeshSurface.BuildNavMesh();
    }
}
