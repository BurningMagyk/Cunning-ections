using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Type { LAND, WATER, WOOD, TREASURE }

    [SerializeField]
    private Island island;

    [SerializeField]
    private Type tileType;

    [SerializeField]
    bool becomesBridge, isTree;

    private void Start()
    {
        island.AddTile(this);
    }

    public void BecomeBridge()
    {
        if (becomesBridge) tileType = Type.WOOD;
    }
    public void Timber()
    {
        if (isTree) tileType = Type.LAND;
    }

    public bool isInside(GenericPlayerInterface player)
    {
        return false;
    }

    public bool CanMoveHere(GenericPlayerInterface player)
    {
        if (tileType == Type.WOOD)
        {
            if (!becomesBridge) return false;
            else if (player.gameObject.GetComponent<PlayerMonster>() != null)
                return false;
        }
        if (tileType == Type.TREASURE) return false;
        return true;
    }
}
