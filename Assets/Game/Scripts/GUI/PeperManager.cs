using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]
public class PeperManager : MonoBehaviour {

    public static PeperManager _PeperManagerInstance;
    private bool _isCollisionWithpeperHappened;
    public bool IsPeperCollision {
        get { return _isCollisionWithpeperHappened; }
        set { _isCollisionWithpeperHappened = value; }
    }
    private int peper = 0;
    public int peperIndex {
        get { return peper; }
        set { peper = value; }
    }
    public Text myPeperText;

    public AudioClip chachiaRing;
    private AudioSource audio;
    // Use this for initialization
    void Start() {
        audio = GetComponent<AudioSource>();
    }
    void Awake() {
        _PeperManagerInstance = this;
    }
    // Update is called once per frame
    void Update() {
       // print("_isCollisionWithPeperHappened" + _isCollisionWithpeperHappened);
       // print("Peper Score IS >>>>>>>>>>>" + this.peperIndex);
    }

    void OnTriggerEnter(Collider colliderInfo) {
        if (colliderInfo.tag == "peper") {
            this.IsPeperCollision = true;
            this.peperIndex++;
           
            //print("CHACHIA score is >>>>>>>>>"+ peperIndex);
            UpdateScoreUI();
            audio.PlayOneShot(chachiaRing, 0.1f);
            Destroy(colliderInfo.gameObject);
           // print("I detect PEPER >>");
            return;
        } else {
            this.IsPeperCollision = false;
           // print("I don't detect any PEPER >>");
        }
    }

    void UpdateScoreUI() {
        myPeperText.text = "CHACHIA : " +this.peperIndex.ToString();
    }
}
