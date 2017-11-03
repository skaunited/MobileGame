using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingManager : MonoBehaviour {

    public float speed = -5.0f;

    

    // Update is called once per frame
    void Update() {
       
            LoadingAnimation(speed);
        
    }
    private void LoadingAnimation(float speed) {
        transform.Rotate(Vector3.forward * speed);
    }
}
