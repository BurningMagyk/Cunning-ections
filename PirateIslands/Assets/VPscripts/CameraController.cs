using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject targetObject;
    public Vector3 CamOffset = new Vector3(0,0,-1000000);

    // Update is called once per frame
    void Update()
    {
        CamOffset = new Vector3(0,0,-1000000) + targetObject.GetSpeed();
        gameObject.transform.position = targetObject.transform.position + CamOffset;
    }
}
