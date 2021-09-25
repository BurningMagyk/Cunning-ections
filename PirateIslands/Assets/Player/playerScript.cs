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
        Vector3 currentPosition = GetComponent<Transform>().localPosition;
        Vector3 desiredDirection = new Vector3();

        if (Input.GetKey(KeyCode.RightArrow)) {
            desiredDirection += rightMove;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            desiredDirection += leftMove;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            desiredDirection += downMove;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            desiredDirection += upMove;
        }

        GetComponent<Transform>().localPosition += desiredDirection.normalized * 0.001f;

    }
}
