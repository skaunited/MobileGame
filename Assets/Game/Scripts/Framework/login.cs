using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;
public class login : MonoBehaviour {
    #region variables
    // principal Panels
    public GameObject HomePanel;
    public GameObject LevelsPanel;
    public GameObject UIpanel;
    ///////////////////
    // User information 
    public Text desplayUsername;
    public Text desplayCoins;
    public Text desplayChachia;
    public Text desplayLevel;
    public Text LevelPanelUsername;
    public Text LevelPanelCoins;
    public Text LevelPanelChachia;
    public Text LevelPanelLevel;
    ///////////////////
    // static Variables
    public InputField Email;
    public InputField Password;
    public Text textResult;
    public GameObject LogInPanel;
    public GameObject CreateAccountPanel;
    //public Variables 
    //private Variables
    private string LoginUrl = "41.231.54.57/TounsiRun/UserLogin.php?";
    private string usermail;
    private string userpassword;
    private string username;
    private string coinsCollected;
    private string chachiaCollected;
    private string level;
    private string lastkey = "";
    #endregion 
    // Use this for initialization
    void Start() {
        usermail = Email.text;
        userpassword = Password.text;
        LogInPanel.SetActive(true);
        CreateAccountPanel.SetActive(false);
    }
    public void LoginAttempt() {
        PhotonNetwork.AuthValues = new AuthenticationValues();
        PhotonNetwork.AuthValues.AuthType = CustomAuthenticationType.Custom;
        PhotonNetwork.AuthValues.AddAuthParameter("usermail", usermail);
        PhotonNetwork.AuthValues.AddAuthParameter("password", userpassword);
        PhotonNetwork.ConnectUsingSettings("Photon Login Fos Sho");
    }
    void OnJoinedLobby() {
        Debug.Log("We did it ");
    }


    public void Login() {
        Debug.Log("Email :" + Email.text);
        Debug.Log("Password" + Password.text);
        StartCoroutine("LoginAccount");
    }
    public void OffLineModeLevels() {
        LevelPanelUsername.text = desplayUsername.text;
        LevelPanelCoins.text = desplayCoins.text;
        LevelPanelChachia.text = desplayChachia.text;
        LevelPanelLevel.text = desplayLevel.text;
        UIpanel.SetActive(false);
        LevelsPanel.SetActive(true);
    }
 

    void OnCustomAuthentificationFailed(string debugMassage) {
        textResult.text = "Error Login or password";
        textResult.gameObject.SetActive(true);
    }
   
    void OnGUI() {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
    #region Couroutine section 
    IEnumerator LoginAccount() {
        WWWForm Form = new WWWForm();
        Form.AddField("Email", Email.text);
        Form.AddField("Password", Password.text);
        WWW LoginAccountWWW = new WWW(LoginUrl, Form);
        yield return LoginAccountWWW;
        if (LoginAccountWWW.error != null) {
            Debug.Log("We cannot login ");
        } else {
            Debug.Log("we have result ");

            string encodedStringResult = LoginAccountWWW.text;
            JSONObject jason = new JSONObject(encodedStringResult);

            if (string.Compare(jason["failedAuthentification"].ToString(),"1") == 0) {
                textResult.text = "erreur Email or password ";
            } else {
                JSONObject userName = jason["UserName"];
                JSONObject userCoins = jason["UserCoinsCollected"];
                JSONObject userChachia = jason["UserChechiaCollected"];
                JSONObject userLevel = jason["UserLevel"];
                //////////////////////////////////////////////////////////
                username = userName.ToString();
                username = username.Trim('"');

                coinsCollected = userCoins.ToString();
                coinsCollected = coinsCollected.Trim('"');

                chachiaCollected = userChachia.ToString();
                chachiaCollected = chachiaCollected.Trim('"');

                level = userLevel.ToString();
                level = level.Trim('"');
                ///////////////////////////////////////////////////////////
                Debug.Log("username = " + username + " coins " + coinsCollected + " chachiya " + chachiaCollected + " level " + level);
                //////////////////////////////////////////////////////////
                accessData(jason);
                //Debug.Log("User ID Result " + username);
                textResult.text = "Login Success for " + username;
                desplayCoins.text = "Coins " + coinsCollected;
                desplayChachia.text = "Chachia " + chachiaCollected;
                desplayLevel.text = "Level " + level;
                desplayUsername.text = "Hello :" + username;
                Thread.Sleep(2000);
                HomePanel.SetActive(false);
                UIpanel.SetActive(true);
            }
        }
    }
    #endregion

    #region Methods 
    void accessData(JSONObject obj) {
        switch (obj.type) {
            case JSONObject.Type.OBJECT:
                for (int i = 0; i < obj.list.Count; i++) {
                    string key = (string)obj.keys[i];
                    JSONObject j = (JSONObject)obj.list[i];
                    Debug.Log(key);
                    accessData(j);
                }
                break;
            case JSONObject.Type.ARRAY:
                foreach (JSONObject j in obj.list) {
                    accessData(j);
                }
                break;
            case JSONObject.Type.STRING:
                Debug.Log(obj.str);
                break;
            case JSONObject.Type.NUMBER:
                Debug.Log(obj.n);
                break;
            case JSONObject.Type.BOOL:
                Debug.Log(obj.b);
                break;
            case JSONObject.Type.NULL:
                Debug.Log("NULL");
                break;

        }
        #endregion
    }//End Class
}