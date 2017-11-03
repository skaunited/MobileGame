using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class CoinManager : MonoBehaviour {

    public static CoinManager _CoinManagerInstance;
    public GameObject[] coins;
    private bool _isCollisionWithCoinHappened;
    public bool IsCoinCollision {
        get { return _isCollisionWithCoinHappened; }
        set { _isCollisionWithCoinHappened = value; }
    }
    private int score = 0;
    public int scoreindex {
        get { return score; }
        set { score = value; }
    }
    private bool destroyed = false;
    public Text myText;
    private int destroyedValue = 0;

    public AudioClip coinsRing;
    private AudioSource audio;
    // Use this for initialization
    void Start() {
        audio = GetComponent<AudioSource>();
    }
    void Awake() {
        _CoinManagerInstance = this;
    }
    // Update is called once per frame
    void Update() {
        // print("_isCollisionWithCoinHappened" + _isCollisionWithCoinHappened);
        // print("Score iS >>>>>>>>>>>>>>" + this.scoreindex);
        CoinAnimator();     
    }

    void OnTriggerEnter(Collider colliderInfo) {
        if (colliderInfo.tag == "coin") {
            this.IsCoinCollision = true;
            this.scoreindex++;
            
            UpdateScoreUI();
            colliderInfo.gameObject.SetActive(false);
            //Destroy(colliderInfo.gameObject);
            audio.PlayOneShot(coinsRing ,0.3f);
            destroyedValue++;
            //print("I detect coin >>");
            return;
        } else {
            this.IsCoinCollision = false;
            //print("I don't detect any coin >>");
        }
    }

    void UpdateScoreUI() {
        myText.text = "COINS $ " +this.scoreindex.ToString();
    }
    private void CoinAnimator() {
        for (int i = 0; i<coins.Length;i++) { 
          coins[i].transform.Rotate(0, 320 * Time.deltaTime, 0);
        }
    }
}
