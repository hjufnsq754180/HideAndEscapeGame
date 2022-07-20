using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private int _enemyCount;

    private GridMap _gridMap;
    [SerializeField]
    private WaypointGenerator _waypointGenerator;

    [Inject]
    private DiContainer _container;

    [Inject]
    private void Construct(GridMap gridMap)
    {
        _gridMap = gridMap;
    }

    private void Awake()
    {
        _waypointGenerator = GetComponent<WaypointGenerator>();
    }

    private void OnEnable()
    {
        BlockGenerator.OnBlockCreated += GenerationEnemy;
    }

    private void OnDisable()
    {

        BlockGenerator.OnBlockCreated -= GenerationEnemy;
    }

    private void GenerationEnemy()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            List<Waypoint> waypoints = _waypointGenerator.GenerationWaypoint();
            GameObject enemyObject = _container.InstantiatePrefab(_enemyPrefab, waypoints[0].gameObject.transform);
            if (enemyObject.TryGetComponent(out EnemyController a))
            {
                foreach (var item in waypoints)
                {
                    a.targetTransforms.Add(item.transform);
                }
            }
            enemyObject.transform.parent = null;
        }
    }
}
