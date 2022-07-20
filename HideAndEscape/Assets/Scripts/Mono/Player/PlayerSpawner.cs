using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PlayerSpawner : MonoBehaviour
{
    private GridMap _gridMap;
    private Vector3 _spawnPos;
    [SerializeField]
    private GameObject _playerPref;

    [Inject]
    private DiContainer _container;
    [Inject]
    private void Construct(GridMap gridMap)
    {
        _gridMap = gridMap;
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Tile firstTile = _gridMap.GridTiles.First();
        _spawnPos = firstTile.GetComponent<Transform>().position;
        _container.InstantiatePrefab(_playerPref, _spawnPos + new Vector3(0.5f, 0, 0.5f), Quaternion.identity, null);
    }
}
