using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMonster : GenericPlayerInterface
{
    [Range(1.0f,10.0f)]
    public float playerSpeed = 10.0f;
    Vector3 rightMove = new Vector3(1, 0, 0);
    Vector3 leftMove = new Vector3(-1, 0, 0);
    Vector3 downMove = new Vector3(0, -1, 0);
    Vector3 upMove = new Vector3(0, 1, 0);
    Vector3 desiredDirection = new Vector3();

    private Vector3 lossyScale;

    [SerializeField]
    private BridgeScript currentBridge;
    private IslandScript currentIsland;
    [SerializeField]
    private float speedMultiplierOrig = 0.005f;
    public float speedMultiplier = 0.005f;

    // Start is called before the first frame update
    void Start()
    {
        lossyScale = GetComponent<Transform>().lossyScale;
    }

    void IdleHandle(bool isIdle){
        gameObject.GetComponentInChildren<SquashStretch>().idle = isIdle;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = GetComponent<Transform>().localPosition += GetDesiredDirection() * speedMultiplier * playerSpeed;

        int canMove = CanMove(newPosition);
        if (canMove == 1)
        {
            speedMultiplier = speedMultiplierOrig;
            GetComponent<Transform>().localPosition = newPosition;
        }
        else if (canMove == 2)
        {
            speedMultiplier = speedMultiplierOrig / 4;
            GetComponent<Transform>().localPosition = newPosition;
        }


        if (GetDesiredDirection().x < 0){
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        } else if(GetDesiredDirection().x > 0) {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
        } 

        if (GetDesiredDirection().x == 0 && GetDesiredDirection().y == 0){
            IdleHandle(true);
        } else {
            IdleHandle(false);
        }

        // Check for entering island
        IslandScript newIsland = IsOnIsland();
        if (newIsland != null)
        {
            if (currentIsland != newIsland)
            {
                currentIsland = newIsland;
                Debug.Log("Entered " + newIsland.gameObject.name);
            }
            
        }

        // Check for entering bridge
        BridgeScript newBridge = IsTouchingBridge();
        if (newBridge != null)
        {
        }
        else currentBridge = null;
    }

    private BridgeScript IsTouchingBridge()
    {
        foreach (BridgeScript item in BridgeScript.All)
        {
            BoxCollider2D bridgeBoxCollider = item.GetComponent<BoxCollider2D>();
            if (GetComponent<BoxCollider2D>().bounds.Intersects(bridgeBoxCollider.bounds)){
                return item;
            }
        }
        
        return null;
    }

    private IslandScript IsOnIsland()
    {
        Bounds bounds = GetComponent<BoxCollider2D>().bounds;
        foreach (IslandScript item in IslandScript.All)
        {
            if (item.IsInside(bounds))
            {
                return item;
            }
        }
        
        return null;
    }

    private int CanMove(Vector3 newPos)
    {
        int islandMove = 0, bridgeMove = 0;

        if (currentBridge != null)
        {
            bridgeMove = currentBridge.CanMove(this, newPos, lossyScale);
            if (bridgeMove == 1) return 1;
        }
        else if (currentIsland != null)
        {
            islandMove = currentIsland.CanMove(this, newPos, lossyScale);
            if (islandMove == 1) return 1;
        }
        
        
        if (currentIsland != null && islandMove == 2) return 2;
        if (currentBridge != null && bridgeMove == 2) return 2;
        
        return 0;
    }
    
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
