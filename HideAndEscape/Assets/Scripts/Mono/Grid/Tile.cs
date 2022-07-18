using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum TileType
{
    Path,
    Block,
    Enemy,
    Empty
}

public class Tile : MonoBehaviour
{
    public TileType type;

    private void Awake()
    {
        type = RandomTileType();
    }

    private TileType RandomTileType()
    {
        System.Random random = new System.Random();
        return (TileType)random.Next(Enum.GetNames(typeof(TileType)).Length);
    }
}
