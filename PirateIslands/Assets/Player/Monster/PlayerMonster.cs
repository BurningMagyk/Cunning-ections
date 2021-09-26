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

    private Vector3 lossyScale;
    private IslandScript currentIsland;
    float speedMultiplier = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        lossyScale = GetComponent<Transform>().lossyScale;
    }


    // Update is called once per frame
    void Update()
    {
        GetComponent<Transform>().localPosition += GetDesiredDirection() * speedMultiplier * playerSpeed;
        if (GetDesiredDirection().x < 0){
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        } else if(GetDesiredDirection().x > 0) {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
    }

    // private bool CanMove(Vector3 newPos)
    // {
    //     return currentIsland.CanMove(this);
    // }
    
    // used for the camera
    public override Vector3 GetSpeed(){
        return desiredDirection;
    }

    Vector3 GetDesiredDirection()
    {
        Vector3 desiredDirection = new Vector3();
        desiredDirection += (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) * rightMove;
        desiredDirection += (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0) * leftMove;
        desiredDirection += (Input.GetKey(KeyCode.DownArrow) ? 1 : 0) * downMove;
        desiredDirection += (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) * upMove;
        desiredDirection = desiredDirection.normalized;
        return desiredDirection;
    }
}
