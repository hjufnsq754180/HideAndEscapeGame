using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WaypointGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _waypointPrefab;
    [SerializeField]
    private int _waypointCount;

    private GridMap _gridMap;
    private List<Tile> _emptyTilesList = new List<Tile>();

    [Inject]
    private void Construct(GridMap gridMap)
    {
        _gridMap = gridMap;
    }

    private void GetEmptyTiles()
    {
        _emptyTilesList.Clear();
        foreach (var item in _gridMap.GridTiles)
        {
            if (item.type == TileType.Empty)
            {
                _emptyTilesList.Add(item);
            }
        }
    }

    public List<Waypoint> GenerationWaypoint()
    {
        GetEmptyTiles();
        List<Waypoint> _waypointList = new List<Waypoint>();
        for (int i = 0; i < _waypointCount; i++)
        {
            int randomTile = Random.Range(0, _emptyTilesList.Count);
            GameObject waypoint = Instantiate(_waypointPrefab, _emptyTilesList[randomTile].gameObject.transform);
            _emptyTilesList[randomTile].type = TileType.Enemy;
            _waypointList.Add(waypoint.GetComponent<Waypoint>());
            _emptyTilesList.RemoveAt(randomTile);
        }
        return _waypointList;
    }
}
