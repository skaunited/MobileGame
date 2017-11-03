using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {
    private Animator animator;
    public static AnimationController _AnimationControllerInstance;
   
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
       
	}
    void Awake() {
        _AnimationControllerInstance = this;
    }
    public void PlayerEndLevel() {
        animator.SetTrigger("Done");
        //print("animation End Level rung>>>>>>>>>>>>>>>");
    }
    public void WalkingAnimation() {
        animator.SetTrigger("Walking");
        print("animation walking runing>>>>>>>>>>>>>>>");
    }
    public void PlayerWaiting() {
            animator.SetTrigger("Pause");
            print("animation Pause runing>>>>>>>>>>>>>>>");
    }
    public IEnumerator waiting() {
        yield return new WaitForSeconds(5);
    }
}
