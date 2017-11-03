using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManagers : MonoBehaviour {
    #region Classes Attributes
        public static ObstacleManagers _ObstacleManagerInstance;
    #endregion
    #region Getter and Setter Attributes 
        private bool _isCollisionWithObstacleHappened;
        public bool IsObstacleCollision {
            get { return _isCollisionWithObstacleHappened; }
            set { _isCollisionWithObstacleHappened = value; }
        }
    #endregion

    #region GameObjects Attributes
        public GameObject[] items;
        public GameObject terminus;
    #endregion
    #region Private Attributes
        private Vector3 terminalTrainPosition;
        private Vector3 beginTrainPosition;
    #endregion


    // Use this for initialization
    void Start () {
        terminalTrainPosition = terminus.transform.position;
        for (int i = 0; i < items.Length; i++) {
            beginTrainPosition = items[i].transform.position;
        }
    }

	void Awake() {
        _ObstacleManagerInstance = this;
    }
	// Update is called once per frame
	void Update () {
        AnimateObstacleItems(beginTrainPosition, terminalTrainPosition);
    }

    void OnTriggerEnter(Collider colliderInfo) {
            if (colliderInfo.tag == "Obstacle") {
                this.IsObstacleCollision = true;
               // print("I detect player >>");
                return;
            }
    }
    void OnTriggerExit(Collider colliderInfo) {
        if (colliderInfo.tag == "Obstacle") {
            this.IsObstacleCollision = false;
            AnimationController._AnimationControllerInstance.WalkingAnimation();
                //print("I don't detect any player >>");
        }
    }

    void AnimateObstacleItems(Vector3 StartPostion , Vector3 EndPosition) {
        //Calculate scalar distance between point Begin to point End
        Vector3 moveDirection = (StartPostion - EndPosition).normalized;
        float distance  = (EndPosition - StartPostion).magnitude;
        //use Mathf.PingPong(...) interpolate this distance over time
        float goBackDistance = distance * Mathf.PingPong(Time.time,1);
        // calculate position of object

        for (int i = 0; i<items.Length; i++) {
            items[i].transform.position = StartPostion + moveDirection * goBackDistance;
        }
    }

}
