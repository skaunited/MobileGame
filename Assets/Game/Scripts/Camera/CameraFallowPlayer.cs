using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFallowPlayer : MonoBehaviour {
    public GameObject player;
    public static CameraFallowPlayer _CameraFallowPlayer;
    private Vector3 offset;
    public float smotheness=1;
    private Vector3 startPosition;

    void Start() {
        //offset = transform.position - player.transform.position;
    }
    void Awake() {
        _CameraFallowPlayer = this;
    }

    void LateUpdate () {
        if (player != null) {
            if (!IsLevelDone._isLevelDoneInstance.IsDoneLevel) {
                transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + offset.z);
            }
        }
    }
}
