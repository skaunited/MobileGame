using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Photon.MonoBehaviour {
    public static PlayerManager _PlayerManagerInstance;
    public int playerVelocity;
    private Rigidbody playerRigitBody;
    private Vector3 playerPosition;
    private Vector3 relativePos;
    private Quaternion rotation;
    private bool justOneTime = true;
    private bool rotated = false;
    private bool _playerLeaveGround = false;
    public bool playerLeaveGround {
        get { return _playerLeaveGround; }
        set { _playerLeaveGround = value; }
    }
    public float jumpForce;


    public static RayCastForward Instance;
    public float minPossibleDistanceToJump = 0.2f;
    [HideInInspector] public bool isGrounded = true;
    int jumptCounter = 0;
    // Use this for initialization
    void Start () {
        playerRigitBody = GetComponent<Rigidbody>();
    }
	void Awake() {
        _PlayerManagerInstance = this;
    }
	// Update is called once per frame
	void Update () {
        #region cmpt
        ////////////////////// Fixing the game object retation///////////////////

        ////////////////////// Fixing the game object retation///////////////////
        /*
       switch (ObstacleManagers._ObstacleManagerInstance.IsObstacleCollision) {
            case false:
                if (!IsLevelDone._isLevelDoneInstance.IsDoneLevel) {
                transform.Translate(Vector3.forward * playerVelocity * Time.deltaTime);
                print("start running");
                 }
                 break;

             case true:
                print("Stop Running");
                if (justOneTime) {
                     OnceForPause();
                }
                break;
        }*/
        #endregion
        #region end level
        if (PhotonNetwork.playerList.Length == 2) { 
            if (!IsLevelDone._isLevelDoneInstance.IsDoneLevel) {
                //print("done level verification >>>>>>>>>>>>>>>>"+ !IsLevelDone._isLevelDoneInstance.IsDoneLevel);
                if (!ObstacleManagers._ObstacleManagerInstance.IsObstacleCollision) {
                    if (rotated) {
                        transform.Rotate(0, 0, 0);
                        rotated = false;
                    }
                    transform.Translate(Vector3.forward * playerVelocity * Time.deltaTime);
                    //print("start running");
                }else {
                    OnceForPause();
                    //print("Stop Running");
               
                }
            }else {
                OnceForEndlevel();
                //SendDataToServer._SendDataToServerInstance.Upload();
            }
        }
        #endregion
        #region cmptt
        /*
         if (!IsLevelDone._isLevelDoneInstance.IsDoneLevel) { 
             transform.Translate(Vector3.forward * playerVelocity * Time.deltaTime);
            // GetComponent<Animation>().Play();
         } else {
             print("Stop Running");
             if (justOneTime) {
                 Once();
             }

         }*/

        /* if (Input.touchCount == 1) {
             var touch = Input.GetTouch(0);
             if (touch.phase == TouchPhase.Stationary) {
                 playerActions.Jump(true);
             } else {
                 playerActions.Jump(false);
                 print("You just pressed");
             }
             print("Touch Count == 1");
         }else {
             print("No Touch detected");
         }*/
        #endregion
        #region raycast
        RaycastHit hit;
        float theDistance;
        //Debug RayCast in the Editor SO WE CAN SEE IT !! 
        Vector3 down = transform.TransformDirection(Vector3.down) * 5f;
        Debug.DrawRay(transform.position, down, Color.green);
        if (Physics.Raycast(transform.position, (down), out hit)) {
            theDistance = hit.distance;
            // Debug.Log("<Color=red>3amar :"+ theDistance + "</Color>");
            if (theDistance > minPossibleDistanceToJump) {
                isGrounded = false;
            } else {
                isGrounded = true;
            }
            //print(theDistance + " >>>> " + hit.collider.gameObject.name);
        }
        #endregion



        if (Input.GetMouseButtonDown(0)){ 
            Jump(jumpForce);
        }
    }
    protected Vector3 GetPlayerPosition() {     
        return playerPosition = playerRigitBody.transform.position;      
     }
    private void OnceForPause() {
        playerRigitBody.transform.position = GetPlayerPosition();
        AnimationController._AnimationControllerInstance.PlayerWaiting();
        //transform.Rotate(0, 90, 0);
        rotated = true;
        justOneTime = false;
    }
    private void OnceForEndlevel() {
        playerRigitBody.transform.Translate(Vector3.zero);
        playerRigitBody.velocity = Vector3.zero;
        AnimationController._AnimationControllerInstance.PlayerEndLevel();
        justOneTime = false;
    }
    private void OnCollisionExit(Collision collInfo) {
        if (collInfo.transform.tag =="Ground") {
            _playerLeaveGround = true;
            print("player leave the ground " + _playerLeaveGround);
        }
    }
    private void OnCollisionEnter(Collision collInfo) {
        if (collInfo.transform.tag == "Ground") {
            _playerLeaveGround = false;
        }
    }

    void OnPhotonInstantiate(PhotonMessageInfo info) {
        if (info.sender.IsLocal) {
            Debug.Log("it's me. anyone here ?");
            CameraFallowPlayer._CameraFallowPlayer.player = this.gameObject;
        } else {
            Debug.Log("yeah I'm here , and will kick you ass !!!!");
        }
    }

    public void Jump(float value) {
        if (isGrounded) {
            jumptCounter = 0;
            playerRigitBody.AddForce(transform.up * value);
        } else if (jumptCounter == 1) {
            playerRigitBody.AddForce(transform.up * value);
        }
        jumptCounter++;
    }
} 
