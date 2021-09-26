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
    private GameObject ui_c;

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
        IsTouchingTree();

        // Check for entering island
        IslandScript newIsland = IsOnIsland();
        if (newIsland != null)
        {
            currentIsland = newIsland;
            Debug.Log("Entered " + newIsland.gameObject.name);
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
        if (currentIsland == null) return true;
        return currentIsland.CanMove(this, newPos, lossyScale);
    }

    private TreeScript IsTouchingTree()
    {
        foreach (TreeScript item in TreeScript.All)
        {
            if (GetComponent<BoxCollider2D>().bounds.Intersects(item.GetComponent<BoxCollider2D>().bounds)){
                
                ui_c.SetActive(true);

                return item;
            }
        }
        
        ui_c.SetActive(false);
        return null;
    }

    private IslandScript IsOnIsland()
    {
        Transform tra = GetComponent<Transform>();
        Vector3 pos = tra.localPosition;
        foreach (IslandScript item in IslandScript.All)
        {
            if (item.IsInside(pos, lossyScale))
            {
                return item;
            }
        }
        
        return null;
    }
}
