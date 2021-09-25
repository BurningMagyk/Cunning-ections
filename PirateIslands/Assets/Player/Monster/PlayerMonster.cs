using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonster : GenericPlayerInterface
{
    [Range(5.0f,20.0f)]
    public float playerSpeed = 10.0f;
    Vector3 rightMove = new Vector3(1, 0, 0);
    Vector3 leftMove = new Vector3(-1, 0, 0);
    Vector3 downMove = new Vector3(0, -1, 0);
    Vector3 upMove = new Vector3(0, 1, 0);
    Vector3 desiredDirection = new Vector3();

    private Island currentIsland;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        desiredDirection = new Vector3();
        desiredDirection += (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) * rightMove;
        desiredDirection += (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0) * leftMove;
        desiredDirection += (Input.GetKey(KeyCode.DownArrow) ? 1 : 0) * downMove;
        desiredDirection += (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) * upMove;
        desiredDirection = desiredDirection.normalized * playerSpeed * 0.01f;

        GetComponent<Transform>().localPosition += desiredDirection;
    }

    // private bool CanMove(Vector3 newPos)
    // {
    //     return currentIsland.CanMove(this);
    // }
    
    // used for the camera
    public override Vector3 GetSpeed(){
        return desiredDirection;
    }
}
