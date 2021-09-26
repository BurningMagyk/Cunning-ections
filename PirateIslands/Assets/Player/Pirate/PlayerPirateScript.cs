using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPirateScript : GenericPlayerInterface
{
    Vector3 rightMove = new Vector3(1, 0, 0);
    Vector3 leftMove = new Vector3(-1, 0, 0);
    Vector3 downMove = new Vector3(0, -1, 0);
    Vector3 upMove = new Vector3(0, 1, 0);

    float speedMultiplier = 0.005f;

    private Vector3 lossyScale;

    [SerializeField]
    private IslandScript currentIsland;
    [SerializeField]
    private BridgeScript currentBridge;
    [SerializeField]
    private GameObject ui_c, ui_v;

    // Start is called before the first frame update
    void Start()
    {
        lossyScale = GetComponent<Transform>().lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = GetComponent<Transform>().localPosition + (GetDesiredDirection() * speedMultiplier);
        if (CanMove(newPosition)) GetComponent<Transform>().localPosition = newPosition;        

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
            if (Input.GetKeyDown(KeyCode.V)) newBridge.Build();
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

    private bool CanMove(Vector3 newPos)
    {
        if (currentIsland != null && currentIsland.CanMove(this, newPos, lossyScale))
            return true;
        else if (currentBridge != null && currentBridge.CanMove(this, newPos, lossyScale))
            return true;
        return false;
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
}
