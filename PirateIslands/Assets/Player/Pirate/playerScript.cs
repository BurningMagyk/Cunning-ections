using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    Vector3 rightMove = new Vector3(1, 0, 0);
    Vector3 leftMove = new Vector3(-1, 0, 0);
    Vector3 downMove = new Vector3(0, -1, 0);
    Vector3 upMove = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredDirection = new Vector3();
        desiredDirection += (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) * rightMove;
        desiredDirection += (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0) * leftMove;
        desiredDirection += (Input.GetKey(KeyCode.DownArrow) ? 1 : 0) * downMove;
        desiredDirection += (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) * upMove;
        desiredDirection = desiredDirection.normalized;

        GetComponent<Transform>().localPosition += desiredDirection * 0.005f;
    }
    
    // used for the camera
    Vector3 GetSpeed(){
        return desiredDirection;
    }
}
