using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject targetObject;
    public Vector3 CamOffset = new Vector3(0,0,-100);
    [Range(5.0f,20.0f)]
    public float leadDistance;
    Vector3 camSlerp = new Vector3();
    
    Vector3 PreviousPosition = new Vector3();

    void Start() {
        PreviousPosition = targetObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 transformDiff = targetObject.transform.position - PreviousPosition;
        camSlerp += (transformDiff * leadDistance - camSlerp) * 0.1f;
        
        gameObject.transform.position = targetObject.transform.position + camSlerp + CamOffset;

        PreviousPosition = targetObject.transform.position;
    }
}
