using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
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
        
    }
}
