using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NetworkPlayer : Photon.MonoBehaviour {
    public GameObject myCamera;
	bool isAlive= true;
	Vector3 position;
	Quaternion rotation;
	float lerpSmoothing = 10f;
    Animator animatorControle;
    Rigidbody regitBody;
    public Text playerName;
    Animator anim;
    AudioSource audioS;
    PlayerManager _playerManager;
    PeperManager _peperManager;
    AnimationController _animationController;
    LifeManager _lifeManager;
    CoinManager _coinManager;
    // Use this for initializatio
    void Start() {
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
        _playerManager = GetComponent<PlayerManager>();
        _peperManager = GetComponent<PeperManager>();
        _animationController = GetComponent<AnimationController>();
        _lifeManager = GetComponent<LifeManager>();
        _coinManager = GetComponent<CoinManager>();
        if (photonView.isMine) {
            NetworkManager.Instance.player_local = this.gameObject;
            gameObject.name = "Me";
            playerName.text = "Me";
            gameObject.tag = "TounsiRun";
            myCamera.SetActive(true);
            anim.enabled = true;
            audioS.enabled = true;
            _playerManager.enabled = true;
            _peperManager.enabled = true;
            _animationController.enabled = true;
            _lifeManager.enabled = true;
            _coinManager.enabled = true;
        } else {
            NetworkManager.Instance.player_Net = this.gameObject;
            gameObject.name = "Network Player";
            gameObject.tag = "netPlayer";
            playerName.text = "Other";
            StartCoroutine("Alive");  
        }
    }
    void OnPhotonInstantiate(PhotonMessageInfo info) {
        
    }
	
	void OnPhotonSerializeView(PhotonStream stream ,PhotonMessageInfo info){
		if (stream.isWriting) {
			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);
		} else if(stream.isReading){
			position = (Vector3)stream.ReceiveNext ();
			rotation = (Quaternion)stream.ReceiveNext();
		}
	}
	IEnumerator Alive(){
		while(isAlive){
            NetworkManager.Instance.player_Net.transform.position = Vector3.Lerp (
                NetworkManager.Instance.player_Net.transform.position,
                position,
                Time.deltaTime * lerpSmoothing );
            NetworkManager.Instance.player_Net.transform.rotation = Quaternion.Lerp (
                NetworkManager.Instance.player_Net.transform.rotation,
                rotation,
                Time.deltaTime * lerpSmoothing);
			yield return null;
		}
	}
}
