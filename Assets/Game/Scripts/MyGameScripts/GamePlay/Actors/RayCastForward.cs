using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastForward : MonoBehaviour {

    public static RayCastForward Instance;
    public float minPossibleDistanceToJump = 0.2f;
    [HideInInspector]public bool isGrounded = true;


    void Awake() {
        Instance = this;
    }

    // Update is called once per frame
    void Update () {
        RaycastHit hit;
        float theDistance;
        //Debug RayCast in the Editor SO WE CAN SEE IT !! 
        Vector3 down = transform.TransformDirection(Vector3.down)*5f;
        Debug.DrawRay(transform.position, down, Color.green);
        if (Physics.Raycast(transform.position,(down), out hit)) {
            theDistance = hit.distance;
           // Debug.Log("<Color=red>3amar :"+ theDistance + "</Color>");
            if (theDistance > minPossibleDistanceToJump) {
                isGrounded = false;
            } else {
                isGrounded = true;
            }
            //print(theDistance + " >>>> " + hit.collider.gameObject.name);
        }
	}
}
