using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    [SerializeField]
    private Tile[] woodTiles;
    [SerializeField]
    private SpriteRenderer spriteChild, spriteHalo;
    public static TreeScript[] All;
    // Start is called before the first frame update
    void Start()
    {
        if (All == null)
        {
            GameObject[] objs =  GameObject.FindGameObjectsWithTag("Tree");
            All = new TreeScript[objs.Length];
            for (int i = 0; i < objs.Length; i++)
            {
                All[i] = objs[i].GetComponent<TreeScript>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        spriteHalo.enabled = false;
    }

    public void ShowHalo()
    {
        spriteHalo.enabled = spriteChild.enabled;
    }

    public void Timber()
    {
        foreach (Tile tile in woodTiles) { tile.tileType = Tile.Type.LAND; }
        spriteChild.enabled = false;
    }
}
