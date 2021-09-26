using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Type { LAND, WATER, WOOD, TREASURE }

    [SerializeField]
    private IslandScript island;
    [SerializeField]
    private BridgeScript bridge;

    [SerializeField]
    public Type tileType;

    [SerializeField]
    bool isTree, isDock;
    private bool becomesBridge;

    float left, right, up, down;

    private void Start()
    {
        becomesBridge = (bridge != null);

        if (island != null) island.AddTile(this);
        if (becomesBridge) bridge.AddTile(this);

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

    // Return 0 - can't move, 1 - can move, 2 - can move but with reduced speed
    public int CanMoveHere(GenericPlayerInterface player)
    {
        if (tileType == Type.WOOD)
        {
            if (!becomesBridge) return 0;
            else if (player.gameObject.GetComponent<PlayerMonster>() != null)
                return 0;
            return 1;
        }
        if (tileType == Type.TREASURE) return 0;
        if (tileType == Type.WATER)
        {
            if (player.gameObject.GetComponent<PlayerPirateScript>() != null)
                return 2;
        }

        if (tileType == Type.LAND)
        {
            if (player.gameObject.GetComponent<PlayerMonster>() != null)
                return 0;
            else return 1;
        }

        return 2;
    }
}
