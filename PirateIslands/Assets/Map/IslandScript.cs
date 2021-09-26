using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandScript : MonoBehaviour
{
    List<Tile> tiles = new List<Tile>();
    Bounds bounds;

    public void AddTile(Tile tile)
    {
        tiles.Add(tile);
    }
    public int CanMove(GenericPlayerInterface player,
        Vector3 newPos, Vector3 lossyScale)
    {
        bool withSlowTile = false, withFastTile = false;

        foreach (Tile tile in tiles)
        {
            if (tile.IsInside(newPos, lossyScale))
            {
                if (tile.CanMoveHere(player) == 0) return 0;
                if (tile.CanMoveHere(player) == 1) withFastTile = true;
                if (tile.CanMoveHere(player) == 2) withSlowTile = true;
            }
        }

        if (withSlowTile || !withFastTile) return 2;
        return 1;
    }

    float left, right, up, down;
    public bool IsInside(Vector3 pos, Vector3 lossyScale)
    {
        float left = pos.x - (lossyScale.x / 2);
        float right = pos.x + (lossyScale.x / 2);
        float up = pos.y + (lossyScale.y / 2);
        float down = pos.y - (lossyScale.y / 2);

        if (left < this.right && right > this.left
            && up > this.down && down < this.up) return true;

        return false;
    }
    public bool IsInside(Bounds bounds)
    {
        return bounds.Intersects(this.bounds);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (All == null)
        {
            GameObject[] objs =  GameObject.FindGameObjectsWithTag("Island");
            All = new IslandScript[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                All[i] = objs[i].GetComponent<IslandScript>();
            }
        }

        Transform tra = GetComponent<Transform>();
        left = tra.position.x - (tra.lossyScale.x / 2);
        right = tra.position.x + (tra.lossyScale.x / 2);
        up = tra.position.y + (tra.lossyScale.y / 2);
        down = tra.position.y - (tra.lossyScale.y / 2);

        bounds = GetComponent<BoxCollider2D>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static IslandScript[] All;
}
