using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    #region Variables
        // principal Panels

        public GameObject LevelsPanel;
        public GameObject UIpanel;
        public GameObject OnlinePanel;
        public GameObject AchatPanel;
        public GameObject LogInPanel;
        public GameObject CreateAccountPanel;
        public InputField Email;
        public InputField Password;
        public Text textResult;
    #endregion
    public void Reset() {
        Email.text = "";
        Password.text = "";
        textResult.text = "";
    }
    public void CreateAccount() {
        LogInPanel.SetActive(false);
        CreateAccountPanel.SetActive(true);
    }
    public void ConnectUser() {
        LogInPanel.SetActive(true);
        CreateAccountPanel.SetActive(false);
    }
    public void ReturnToUIpanel() {

        LevelsPanel.SetActive(false);
        UIpanel.SetActive(true);
    }

    public void ActiveAchatPanel() {
        AchatPanel.SetActive(true);
    }
    public void CloseAchatPanel() {
        AchatPanel.SetActive(false);
    }

    public void LookingOnOpponent() {
        OnlinePanel.SetActive(true);
    }
    public void CloseLookingOnOpponent() {
        OnlinePanel.SetActive(false);
        UIpanel.SetActive(true);
    }
    public void PlayLevelOne() {
        SceneManager.LoadScene("AvenueHabibBorguiba");
    }
    public void PlayLevelTow() {
        SceneManager.LoadScene("AvenueHabibBorguiba");
    }
}
