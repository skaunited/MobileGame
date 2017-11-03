using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NetworkManager : MonoBehaviour {

    static public NetworkManager Instance;

    const string VERSION = "v0.0.5";
    public string roomName = "STR";
    public string playerPrefabName = "running_inPlace";
    public GameObject spawnPointObj;
    Transform spawnPoint;
    private bool connected = false ;
    PhotonView pv = new PhotonView();

    bool isInstantiated = false;
    public GameObject player_local;
    public GameObject player_Net;

    void Awake(){
        Instance = this;
    }

    void Start () {
        DontDestroyOnLoad(this.gameObject);
        PhotonNetwork.ConnectUsingSettings(VERSION);
        spawnPoint = spawnPointObj.transform;

    }

	void OnJoinedLobby() {
        Debug.Log("Joining lobby");

         PhotonNetwork.JoinRandomRoom();
        
    }
    void Update() {
        if (connected && PhotonNetwork.playerList.Length == 2 && !isInstantiated) {
            StartCoroutine(StartGame());
            isInstantiated = true;

        }

    }

    IEnumerator StartGame() {
        SceneManager.LoadScene("AvenueHabibBorguiba");

        yield return new WaitForSecondsRealtime(2);
        
        PhotonNetwork.Instantiate(playerPrefabName, spawnPoint.position, spawnPoint.rotation, 0);
       
    }

    void OnPhotonRandomJoinFailed(object[] codeAndMsg) {
        Debug.Log("Joining room failed");
        RoomOptions roomOptions = new RoomOptions() { isVisible = false, maxPlayers = 2 };
        
        PhotonNetwork.CreateRoom("null");
    }

    void OnJoinedRoom() {
        Debug.Log("Connected to room");
        connected = true;
    }
}
