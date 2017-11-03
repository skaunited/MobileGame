using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions :MonoBehaviour {

   

    //private Touch touch;
    public Vector2 _externalForce;
    public Vector2 _speed;
    public static PlayerActions _PlayerActionInstance;
    public float rayDistance = 80;
    
    public float jumpPowerSimplePress = 10;
    int jumptCounter = 0;


    // Use this for initialization
    void Start () {
        Debug.Log("<Color=green>" + gameObject.name + "</color>");
        _PlayerActionInstance = this;
    }

	// Update is called once per frame
	void Update () {
		
	}
    public void Jump(float value) {
                Rigidbody rb = GetComponent<Rigidbody>();
                if (RayCastForward.Instance.isGrounded) {
                jumptCounter = 0;
                rb.AddForce(transform.up * value);
                }else if (jumptCounter==1) {
                rb.AddForce(transform.up * value);
                }
                jumptCounter++;
        
    }
    public bool IsGrounded(){
        return Physics.Raycast(transform.position, Vector3.down, rayDistance);
    }
    
}
    

