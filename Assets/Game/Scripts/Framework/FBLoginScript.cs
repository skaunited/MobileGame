using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Facebook.Unity;
using System.Threading;
using UnityEngine.SceneManagement;

public class FBLoginScript : MonoBehaviour {
    public GameObject DialogLoggedIn;
    public GameObject DialogLoggedOut;
    public GameObject DialogUserName;
    public GameObject DialogProfilePic;
    public GameObject DialogLoadingLogo;
    public GameObject FBHolder;
    public static FBLoginScript facebookistance;
    private bool loadAreDone = false;
    public User user = new User();

    void Awake() {
        FB.Init(SetInit,onHideUnity);
        DontDestroyOnLoad(FBHolder);
        facebookistance = this;
    }




    void Start() {
        DialogLoadingLogo.SetActive(false);
    }

    void Update() {
        if (loadAreDone == false) {
            if (DialogLoggedIn.activeInHierarchy) {
                loadAreDone = true;
                Thread.Sleep(5000);
                SceneManager.LoadScene("LoadingScene");
            }
        } 
    }



    void SetInit() {
        if (FB.IsLoggedIn) {
            Debug.Log("Facebook is Logged in ");
        }else {
            Debug.Log("Facebook is do not Logged in ");
        }
        DealWithFBMenus(FB.IsLoggedIn);
    }

    void onHideUnity(bool isGameShown) {
        if (!isGameShown) {
            Time.timeScale = 0;
        }else {
            Time.timeScale = 1;
        }
    }
    public void FBLogin() {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");
        FB.LogInWithReadPermissions(permissions,AuthCallBack);
        TimeSpan span = new TimeSpan(0, 0, 0, 5, 0);
        System.Threading.Thread.Sleep(span);
        DialogLoggedOut.SetActive(false);
        DialogLoadingLogo.SetActive(true);
    }
    void AuthCallBack(IResult result) {
        if (result.Error != null) {
            Debug.Log("Result Error is :"+result.Error);
        } else {
            if (FB.IsLoggedIn) {
                Debug.Log("Facebook is Logged in ");
            } else {
                Debug.Log("Facebook is do not Logged in ");
            }
            DealWithFBMenus(FB.IsLoggedIn);
        }
    }
    void DealWithFBMenus(bool isLoggedIn) {
        if (isLoggedIn) {
            DialogLoggedIn.SetActive(true);
            DialogLoggedOut.SetActive(false);
            DialogLoadingLogo.SetActive(false);
            FB.API("/me?fields=id,name", HttpMethod.GET, GetUserIdAndName); 
            FB.API("/me/picture?type=square&height=100&width=100", HttpMethod.GET, DisplayProfilePic);
        } else {
           
            DialogLoggedIn.SetActive(false);
          //  DialogLoggedOut.SetActive(true);    
        }
    }
    void GetUserIdAndName(IResult result) {
        Text UserName = DialogUserName.GetComponent<Text>();
        if (result.Error == null) {
            UserName.text = "Hello " + result.ResultDictionary["name"];
            user.UserName = (string)result.ResultDictionary["name"];
            user.UserID = (string)result.ResultDictionary["id"];
            user.playerIsConnected = FB.IsLoggedIn;
        } else {
            Debug.Log(result.Error);
        }
    }
    void DisplayProfilePic(IGraphResult result) {
        if (result.Texture != null) {
            Image profilePic = DialogProfilePic.GetComponent<Image>();
            profilePic.sprite = Sprite.Create(result.Texture , new Rect(0,0,100,100) ,new Vector2());
            user.UserPicture = profilePic.sprite;
        } else {
            Debug.Log("Profile Picture do not Loaded");
        }
    }

}
