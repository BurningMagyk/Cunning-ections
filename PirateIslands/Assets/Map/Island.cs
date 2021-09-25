using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour
{
    List<Tile> tiles = new List<Tile>();

    public void AddTile(Tile tile)
    {
        tiles.Add(tile);
    }
    public bool CanMove(PlayerMonster player)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.IsInside(player))
            {
                if (!tile.CanMoveHere(player)) return false;
            }
        }
        return true;
    }
    public bool CanMove(PlayerPirateScript player)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.IsInside(player))
            {
                if (!tile.CanMoveHere(player)) return false;
            }
        }
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
