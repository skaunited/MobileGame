using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localManager : MonoBehaviour {

    public static localManager _localManager;
    private string parseURL = "http://41.231.54.57/TounsiRun/AddUser.php?";
    // Use this for initialization
    void Start () {
		
	}
    void Awake() {
       _localManager = this;
    }

    public IEnumerator CreateSuperTounsiAcount(string username, string email, string password) {
        string externalSubrequest = "UserName=" + username + "&UserEmail=" + email + "&Userpassword=" + password;
        string execteQuery = parseURL + externalSubrequest + "&UserLevel=" + 3 + "&UserCoinsCollected=" + 0 + "&UserChechiaCollected=" + 0;
        print(execteQuery);
        WWW hostLocal = new WWW(execteQuery);
        yield return hostLocal;
        if (hostLocal.error != null) {
            print("There was an error posting: " + hostLocal.error);
        } else {
            Debug.Log("Form upload complete!");
        }
    }
}
