using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _blockPrefabs;
    private GridMap _gridMap;

    [Inject]
    private void Construct(GridMap gridMap)
    {
        _gridMap = gridMap;
    }

    private void OnEnable()
    {
        PathGenerator.OnPathCreated += GenerationBlock;
    }

    private void OnDisable()
    {
        PathGenerator.OnPathCreated -= GenerationBlock;
    }

    public void GenerationBlock()
    {
        foreach (var item in _gridMap.GridTiles)
        {
            if (item.type == TileType.Block)
            {
                int blockId = Random.Range(0, _blockPrefabs.Count);
                Instantiate(_blockPrefabs[blockId], item.gameObject.transform);
            }
        }
    }
}
