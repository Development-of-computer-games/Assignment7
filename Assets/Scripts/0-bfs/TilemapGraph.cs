using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * A graph that represents a tilemap, using only the allowed tiles.
 */


public class TilemapGraph: IGraph<Vector3Int> {
    public static int bushesSpeed = 5;
    public static int grassSpeed = 50;
    public static int hillsSpeed = 14;
    public static int swampSpeed = 1;
    private Tilemap tilemap;
    private TileBase[] allowedTiles;
    private int[] weights = { 30, 6, 5, 4 };
    private int []  speeds = { bushesSpeed, grassSpeed, hillsSpeed, swampSpeed };


    public TilemapGraph(Tilemap tilemap, TileBase[] allowedTiles) {
        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
       
    }

    static Vector3Int[] directions = {
            new Vector3Int(-1, 0, 0),
            new Vector3Int(1, 0, 0),
            new Vector3Int(0, -1, 0),
            new Vector3Int(0, 1, 0),
    };

    public IEnumerable<Vector3Int> Neighbors(Vector3Int node) {
        foreach (var direction in directions) {
            Vector3Int neighborPos = node + direction;
            TileBase neighborTile = tilemap.GetTile(neighborPos);
            if (allowedTiles.Contains(neighborTile))
                yield return neighborPos;
        }
    }

    public int Weights(Vector3Int node)
    {
        // this function gets a Vector3int position of a tile.
        // in weights array we put weight for every tile
        // when we get the tile name according to the allowedTiles array
        // we will give it the corresponding weight.
        for (int i = 0; i < allowedTiles.Length; i++)
        {
            if(allowedTiles[i].name.Equals(tilemap.GetTile(node).name))
            {
                 return weights[i];
            }
        }
        return 0;

        
    }


    public int tileSize()
    {
        return tilemap.size.x*tilemap.size.y;
    }

    public int Speed(Vector3Int node , int speed)
    {
        // this function gets a Vector3int position of a tile.
        // in speeds array we put speed for every tile
        // when we get the tile name according to the allowedTiles array
        // we will give it the corresponding speed.

        TileBase neighborTile = tilemap.GetTile(node);
        for (int i = 0; i < allowedTiles.Length; i++)
        {
            if (allowedTiles[i].name.Equals(neighborTile.name))
            {
                return speeds[i]/speed;
            }

        }

        return 1 / speed;


    }

 
}
