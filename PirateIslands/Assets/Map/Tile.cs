using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Type { LAND, WATER, WOOD, TREASURE }

    [SerializeField]
    private IslandScript island;

    [SerializeField]
    private Type tileType;

    [SerializeField]
    bool becomesBridge, isTree, isDock;

    float left, right, up, down;

    private void Start()
    {
        island.AddTile(this);

        Transform tra = GetComponent<Transform>();
        left = tra.position.x - (tra.lossyScale.x / 2);
        right = tra.position.x + (tra.lossyScale.x / 2);
        up = tra.position.y + (tra.lossyScale.y / 2);
        down = tra.position.y - (tra.lossyScale.y / 2);
    }

    public void BecomeBridge()
    {
        if (becomesBridge) tileType = Type.WOOD;
    }
    public void Timber()
    {
        if (isTree) tileType = Type.LAND;
    }

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
