using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeScript : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer middlePart, semitransparent;
    private bool isBuilt = false;
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

    // Start is called before the first frame update
    void Start()
    {
        middlePart.enabled = false;
        semitransparent.enabled = false;

        if (All == null)
        {
            GameObject[] objs =  GameObject.FindGameObjectsWithTag("Bridge");
            All = new BridgeScript[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                All[i] = objs[i].GetComponent<BridgeScript>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        semitransparent.enabled = false;
    }

    public void Build()
    {
        isBuilt = true;
        middlePart.enabled = true;
        semitransparent.enabled = false;
        foreach (Tile tile in tiles) { tile.BecomeBridge(); }
    }
    public void Show()
    {
        if (!isBuilt) semitransparent.enabled = true;
    }

    public static BridgeScript[] All;
}
