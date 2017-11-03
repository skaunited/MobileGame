using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsLevelDone : MonoBehaviour {
    public static IsLevelDone _isLevelDoneInstance;
    private bool _isDoneLevel;

    public bool IsDoneLevel {
        get { return _isDoneLevel; }
        set { _isDoneLevel = value; }
    }
    void Awake() {
        _isLevelDoneInstance = this;
    }
    // Use this for initialization
    void Start () {
        this.IsDoneLevel = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other) {
        if (other.tag=="TounsiRun") {
            this.IsDoneLevel = true;
            //print("Player detected enter "+ IsDoneLevel);
        }
    }
}
