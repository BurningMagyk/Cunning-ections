using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPirateScript : GenericPlayerInterface
{
    Vector3 rightMove = new Vector3(1, 0, 0);
    Vector3 leftMove = new Vector3(-1, 0, 0);
    Vector3 downMove = new Vector3(0, -1, 0);
    Vector3 upMove = new Vector3(0, 1, 0);

    [SerializeField]
    private float speedMultiplierOrig = 0.005f;
    private float speedMultiplier;

    private Vector3 lossyScale;

    [SerializeField]
    private IslandScript currentIsland;
    [SerializeField]
    private BridgeScript currentBridge;
    [SerializeField]
    private GameObject ui_c, ui_v;
    [SerializeField]
    private WoodCollect woodCollect;
    [SerializeField]
    private PlayerMonster monster;

    // Start is called before the first frame update
    void Start()
    {
        lossyScale = GetComponent<Transform>().lossyScale;

        speedMultiplier = speedMultiplierOrig;
    }
    
    void PlayerStateHandle(string state){ // IDLE, WALK, WIN, DEAD
        gameObject.GetComponentInChildren<SquashStretch>().PlayerState = state;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if killed by monster
        if (GetComponent<BoxCollider2D>().bounds.Intersects(monster.GetComponent<BoxCollider2D>().bounds))
            Loss();

        Vector3 newPosition = GetComponent<Transform>().localPosition + (GetDesiredDirection() * speedMultiplier);
        
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

        //Checking for tree collision
        TreeScript tree = IsTouchingTree();
        if (tree != null)
        {
            tree.ShowHalo();
            if (Input.GetKeyDown(KeyCode.C)) tree.Timber();
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
            newBridge.Show();
            if (currentBridge != newBridge)
            {
                currentBridge = newBridge;
                Debug.Log("Entered " + newBridge.gameObject.name);
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                if (woodCollect.Decrement()) newBridge.Build();
            }
        }
        else currentBridge = null;

        // handle sprite flipping
        if (GetDesiredDirection().x < 0){
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        } else if(GetDesiredDirection().x > 0) {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
        } 

        if (GetDesiredDirection().x == 0 && GetDesiredDirection().y == 0){
            PlayerStateHandle("IDLE");
        } else {
            PlayerStateHandle("WALK");
        }
    }
    
    // used for the camera
    override public Vector3 GetSpeed(){
        Vector3 desiredDirection = GetDesiredDirection(); 
        return desiredDirection * speedMultiplier;
    }

    Vector3 GetDesiredDirection()
    {
        Vector3 desiredDirection = new Vector3();
        desiredDirection += (Input.GetKey(KeyCode.D) ? 1 : 0) * rightMove;
        desiredDirection += (Input.GetKey(KeyCode.A) ? 1 : 0) * leftMove;
        desiredDirection += (Input.GetKey(KeyCode.S) ? 1 : 0) * downMove;
        desiredDirection += (Input.GetKey(KeyCode.W) ? 1 : 0) * upMove;
        desiredDirection = desiredDirection.normalized;
        return desiredDirection;
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

    private TreeScript IsTouchingTree()
    {
        foreach (TreeScript item in TreeScript.All)
        {
            BoxCollider2D treeBoxCollider = item.GetComponent<BoxCollider2D>();
            if (treeBoxCollider.enabled
                && GetComponent<BoxCollider2D>().bounds.Intersects(treeBoxCollider.bounds)){
                
                ui_c.SetActive(true);

                return item;
            }
        }
        
        ui_c.SetActive(false);
        return null;
    }

    private BridgeScript IsTouchingBridge()
    {
        foreach (BridgeScript item in BridgeScript.All)
        {
            BoxCollider2D bridgeBoxCollider = item.GetComponent<BoxCollider2D>();
            if (GetComponent<BoxCollider2D>().bounds.Intersects(bridgeBoxCollider.bounds)){
                
                ui_v.SetActive(true);

                return item;
            }
        }
        
        ui_v.SetActive(false);
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

    public void Victory()
    {
        gameObject.GetComponentInChildren<SquashStretch>().Victory();
        PlayerStateHandle("WIN");
    }
    public void Loss()
    {
        gameObject.GetComponentInChildren<SquashStretch>().Loss();
        PlayerStateHandle("LOSE");
    }
}
