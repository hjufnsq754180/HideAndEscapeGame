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
        type = TileType.Empty;
        type = RandomTileType();
    }

    private TileType RandomTileType()
    {
        System.Random random = new System.Random();
        TileType tileType = (TileType)random.Next(Enum.GetNames(typeof(TileType)).Length);
        if (tileType == TileType.Path || tileType == TileType.Enemy)
        {
            return TileType.Empty;
        }
        else return tileType;
    }
}
