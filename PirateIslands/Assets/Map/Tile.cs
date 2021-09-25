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

    float left, right, up, down;

    private void Start()
    {
        island.AddTile(this);

        Transform tra = GetComponent<Transform>();
        left = tra.localPosition.x - (tra.localScale.x / 2);
        right = tra.localPosition.x + (tra.localScale.x / 2);
        up = tra.localPosition.y + (tra.localScale.y / 2);
        down = tra.localPosition.y - (tra.localScale.y / 2);
    }

    public void BecomeBridge()
    {
        if (becomesBridge) tileType = Type.WOOD;
    }
    public void Timber()
    {
        if (isTree) tileType = Type.LAND;
    }

    public bool IsInside(GenericPlayerInterface player)
    {
        Transform tra = player.gameObject.GetComponent<Transform>();
        float left = tra.localPosition.x - (tra.localScale.x / 2);
        float right = tra.localPosition.x + (tra.localScale.x / 2);
        float up = tra.localPosition.y + (tra.localScale.y / 2);
        float down = tra.localPosition.y - (tra.localScale.y / 2);

        if (left < this.right && right > this.left
            && up > this.down && down < this.up) return true;

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
