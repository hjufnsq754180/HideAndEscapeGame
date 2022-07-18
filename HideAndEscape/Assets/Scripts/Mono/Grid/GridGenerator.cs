using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] 
    private GridSettings _gridSettings;
    private GridMap _gridMap;

    [Inject]
    private void Construct(GridMap gridMap)
    {
        _gridMap = gridMap;
    }


    private void Awake()
    {
        if (_gridSettings != null) GenerationGrid(_gridSettings);
    }

    private void GenerationGrid(GridSettings settings)
    {
        for (int x = 0; x < settings.width; x += settings.offset)
        {
            for (int z = 0; z < settings.width; z += settings.offset)
            {
                GameObject tile = Instantiate(settings.cellPref, new Vector3(x, 0, z), Quaternion.identity, _gridMap.transform);
                tile.name = $"x:{x} | z:{z}";
            }
        }
        _gridMap.SetGridSize(settings);
    }
}
