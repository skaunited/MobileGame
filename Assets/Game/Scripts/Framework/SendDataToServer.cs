using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Facebook.Unity;
using System;
using UnityEngine.UI;

public class SendDataToServer : MonoBehaviour {

    public static SendDataToServer _SendDataToServerInstance;
    private string userName ="";
    private string lastName ="";
    private string userFacebookId;

    private int _coinsCollected;
    public int coinsCollected {
        get { return _coinsCollected; }
        set { _coinsCollected = value; }
    }
    public string externalSubrequest;
    private int _chachiaCollected;
    public int chachiaCollected {
        get { return _chachiaCollected; }
        set { _chachiaCollected = value; }
    }

    private string parseURL= "http://41.231.54.57/TounsiRun/AddUser.php?";
    private bool updateDataDone = false;
    void Start() {
        if (FB.IsLoggedIn) {
            name = FBLoginScript.facebookistance.user.UserName;
            userFacebookId = FBLoginScript.facebookistance.user.UserID;
        }
       
        
    }
    void Awake() {
        _SendDataToServerInstance = this;
        //this.coinsCollected = CoinManager._CoinManagerInstance.scoreindex; 
    }

    void Update() {
        print("<<<<<<<>>>>>>>>" + IsLevelDone._isLevelDoneInstance.IsDoneLevel);
        if (updateDataDone == false) {
            if (IsLevelDone._isLevelDoneInstance.IsDoneLevel == true) {
                this.chachiaCollected = PeperManager._PeperManagerInstance.peperIndex;
                this.coinsCollected = CoinManager._CoinManagerInstance.scoreindex;
                updateDataDone = true;
                print("super tounsi 1 st level are done "+IsLevelDone._isLevelDoneInstance.IsDoneLevel);
                StartCoroutine("SendData");
           }
        }
    }


    IEnumerator SendData() {
        string executedURL = parseURL + "UserFacebookID=" + WWW.EscapeURL(userFacebookId) + "&UserName=" + WWW.EscapeURL(name) + "&UserLastName="+ WWW.EscapeURL(name)+"&UserLevel=" + 3 + "&UserCoinsCollected=" + this.coinsCollected + "&UserChechiaCollected=" + this.chachiaCollected;
        Debug.Log("<<<<<<<URL Wish WIll EXECUTED >>>>>>>>"+executedURL);
        WWW hostServer_post = new WWW(executedURL);
        yield return hostServer_post; // Wait until the download is done


        if (hostServer_post.error != null) {
            //print("There was an error posting the high score: " + hs_post.error);
        } else {
            Debug.Log("Form upload complete!");
        }
    }
}
