using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading;
using UnityEngine.SceneManagement;

public class LoacalAccountsManager : MonoBehaviour {
    
    #region input Attributes 
    public Text _userName;
    public Text _userEmail;
    public Text _userPassword;
    public Text _userConfirmPassword;
    #endregion
    private string email;
    private string password;
    private string username;
    private localManager localManager;
    // Use this for initialization
    void Start () {
        localManager = localManager._localManager;
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private bool  VerifInputEmailsValue() {
        if(_userEmail.text.Trim() != null) {
            if (_userEmail.text.ToLower().Contains('@')) {
                email = _userEmail.text;
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }
    private bool VerifPassword() {
        if (_userPassword.text.Length < 5) {
            Debug.Log("Password too short ");
            return false;
        }else if(string.Compare(_userPassword.text, _userConfirmPassword.text) == 0) {
            password = _userPassword.text;
            return true;
        } else {
            Debug.Log("verif your password ");
            return false;
        }
    }
    private bool verifUserName() {
        if (_userName.text.Length < 15) {
            username = _userName.text;
            return true;
        } else {
            return false;
        }
    }

    

    public void ExecuteCreateAccountEssay() {
        if (VerifInputEmailsValue() && VerifPassword() && verifUserName()) {
            print("I will call courotine");
           
            if (localManager!= null) { 
                StartCoroutine(localManager.CreateSuperTounsiAcount(username, email, password));
                Thread.Sleep(5000);
                SceneManager.LoadScene("LoadingScene");
            } else {
                print("null bell batata");
            }
            print("I called courotine");
        }
    }
}
