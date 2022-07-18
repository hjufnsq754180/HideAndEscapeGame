using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class PathGenerator : MonoBehaviour
{
    private GridMap _gridMap;
    private List<Tile> _gridTiles;

    private List<Tile> tilesPath = new List<Tile>();
    private int currentIndex = 0;
    private int nextIndex = 0;

    private Tile startTile;
    private Tile endTile;
    private Tile currentTile;

    private bool reachedX = false;
    private bool reachedZ = false;

    private int _sizeGrid;

    [SerializeField]
    private Material pathMat;

    public int saveCounter;

    public static Action OnPathCreated;


    [Inject]
    private void Construct(GridMap gridMap)
    {
        _gridMap = gridMap;
    }

    private void Start()
    {
        InitPathGeneration();
    }

    private void InitPathGeneration()
    {
        _sizeGrid = _gridMap.GridSize;
        _gridTiles = _gridMap.GridTiles;

        startTile = _gridTiles[0];
        endTile = _gridTiles.Last();
        ColoringPathTile(endTile);
        endTile.type = TileType.Path;

        currentTile = startTile;

        GenerationPath();
    }

    private void GenerationPath()
    {
        while (true)
        {
            saveCounter++;
            if (saveCounter > 100)
            {
                break;
            }
            if (reachedX && reachedZ)
            {
                break;
            }
            int rnd = UnityEngine.Random.Range(0, 2);

            if (rnd == 0)
            {
                MoveUp();
            }
            if (rnd == 1)
            {
                MoveRight();
            }
        }

        foreach (var item in tilesPath)
        {
            ColoringPathTile(item);
            item.type = TileType.Path;
        }

        OnPathCreated?.Invoke();
    }

    private void MoveUp()
    {
        if (currentTile.transform.position.z < endTile.transform.position.z)
        {
            tilesPath.Add(currentTile);
            currentIndex = _gridTiles.IndexOf(currentTile);
            nextIndex = currentIndex + 1;
            currentTile = _gridTiles[nextIndex];
        }
        else
        {
            reachedZ = true;
        }
    }

    private void MoveRight()
    {
        if (currentTile.transform.position.x < endTile.transform.position.x)
        {
            tilesPath.Add(currentTile);
            currentIndex = _gridTiles.IndexOf(currentTile);
            nextIndex = currentIndex + _sizeGrid;
            currentTile = _gridTiles[nextIndex];
        }
        else
        {
            reachedX = true;
        }
    }

    private void ColoringPathTile(Tile tile)
    {
        tile.gameObject.GetComponent<MeshRenderer>().material = pathMat;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
