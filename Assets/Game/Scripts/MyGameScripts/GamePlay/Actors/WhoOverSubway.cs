using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoOverSubway : MonoBehaviour {
    public bool playerOverSubway;
    public static WhoOverSubway _WhoOverSubwayInstance;
    // Use this for initialization
    void Start () {
		
	}
	void Awake() {
        _WhoOverSubwayInstance = this;
    }
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "TounsiRun") {
            playerOverSubway = true;
        }    
    }
    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "TounsiRun") {
            playerOverSubway = false;
        }
    }
}
