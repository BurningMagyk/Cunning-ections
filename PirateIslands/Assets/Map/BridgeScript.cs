using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    List<Tile> tiles = new List<Tile>();

    public void AddTile(Tile tile)
    {
        tiles.Add(tile);
    }
    public bool CanMove(GenericPlayerInterface player,
        Vector3 newPos, Vector3 lossyScale)
    {
        foreach (Tile tile in tiles)
        {
            if (tile.IsInside(newPos, lossyScale))
            {
                if (!tile.CanMoveHere(player)) return false;
            }
        }
        return true;
    }
    float left, right, up, down;
    public bool IsInside(Vector3 newPos, Vector3 lossyScale)
    {
        float left = newPos.x - (lossyScale.x / 2);
        float right = newPos.x + (lossyScale.x / 2);
        float up = newPos.y + (lossyScale.y / 2);
        float down = newPos.y - (lossyScale.y / 2);

        if (left < this.right && right > this.left
            && up > this.down && down < this.up) return true;

        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (All == null)
        {
            GameObject[] objs =  GameObject.FindGameObjectsWithTag("Bridge");
            All = new BridgeScript[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                All[i] = objs[i].GetComponent<BridgeScript>();
            }
        }

        Transform tra = GetComponent<Transform>();
        left = tra.position.x - (tra.lossyScale.x / 2);
        right = tra.position.x + (tra.lossyScale.x / 2);
        up = tra.position.y + (tra.lossyScale.y / 2);
        down = tra.position.y - (tra.lossyScale.y / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static BridgeScript[] All;
}
