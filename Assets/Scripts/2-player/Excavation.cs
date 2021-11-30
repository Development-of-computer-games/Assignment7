using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component just keeps a list of allowed tiles.
 * Such a list is used both for pathfinding and for movement.
 */
public class Excavation : MonoBehaviour
{
    [SerializeField] TileBase[] excavationArray = null;

    public bool Contain(TileBase tile)
    {
        return excavationArray.Contains(tile);
    }

    public TileBase[] Get() { return excavationArray; }
}
