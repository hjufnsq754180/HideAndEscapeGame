using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GridMap : MonoBehaviour
{
    [SerializeField]
    private List<Tile> _gridTiles = new List<Tile>();
    public List<Tile> GridTiles => _gridTiles;

    private int _gridSize;
    public int GridSize => _gridSize;

    private void Start()
    {
        GetTilesInScene();
    }

    private void GetTilesInScene()
    {
        _gridTiles = GetComponentsInChildren<Tile>().ToList();
    }

    public void SetGridSize(GridSettings gridSettings)
    {
        _gridSize = gridSettings.width;
    }
}
