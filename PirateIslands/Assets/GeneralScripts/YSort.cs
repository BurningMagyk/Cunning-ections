using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSort : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = GetComponent<Transform>().localPosition;
        pos.z = -pos.y;
        GetComponent<Transform>().localPosition = pos;
    }
}
