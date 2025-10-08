using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLoadAnimationController : MonoBehaviour {

	public string AnimatorControllerFileName;

	// Use this for initialization
	void Start () {



		Animator ContainerAnimator = this.GetComponent<Animator>();
		//ContainerAnimator.runtimeAnimatorController = Resources.Load("Assets/Cube") as RuntimeAnimatorController;
		ContainerAnimator.runtimeAnimatorController = Resources.Load("AnimatorController/" + AnimatorControllerFileName) as RuntimeAnimatorController;
		//ContainerAnimator.Play("ContainerMoveUp");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
