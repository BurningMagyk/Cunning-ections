using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPirateScript : GenericPlayerInterface
{
    Vector3 rightMove = new Vector3(1, 0, 0);
    Vector3 leftMove = new Vector3(-1, 0, 0);
    Vector3 downMove = new Vector3(0, -1, 0);
    Vector3 upMove = new Vector3(0, 1, 0);

    Vector3 desiredDirection = new Vector3();
    float speedMultiplier = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().localPosition += GetDesiredDirection() * speedMultiplier;
    }
    
    // used for the camera
    override public Vector3 GetSpeed(){
        Vector3 desiredDirection = GetDesiredDirection(); 
        return desiredDirection * speedMultiplier;
    }

    Vector3 GetDesiredDirection()
    {
        desiredDirection += (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) * rightMove;
        desiredDirection += (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0) * leftMove;
        desiredDirection += (Input.GetKey(KeyCode.DownArrow) ? 1 : 0) * downMove;
        desiredDirection += (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) * upMove;
        desiredDirection = desiredDirection.normalized;
        return desiredDirection;
    }
}
