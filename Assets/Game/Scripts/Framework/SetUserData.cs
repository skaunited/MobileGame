using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;

public class SetUserData : MonoBehaviour {
    public GameObject DialogProfilePic;
    
    // Use this for initialization
    void Start () {
        if (FB.IsLoggedIn) {
            Image profilePic = DialogProfilePic.GetComponent<Image>();
            profilePic.sprite = FBLoginScript.facebookistance.user.UserPicture;
        }
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
