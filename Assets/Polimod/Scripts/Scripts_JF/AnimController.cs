using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimController : MonoBehaviour
{

	public Animator animator;
	public float AnimationFPS = 30F;

	public void Update ()
	{
		float animationTimeLenght = animator.GetCurrentAnimatorStateInfo (0).length;
		//Debug.Log ("animationTime lenght is " + animationLenght);

		float animationTime = animator.GetCurrentAnimatorStateInfo (0).normalizedTime;
		//Debug.Log ("animationTime (normalized) is " + animationTime);


		Animator myAnimator = animator.GetComponent<Animator> ();
		AnimatorStateInfo animationState = myAnimator.GetCurrentAnimatorStateInfo (0);
		AnimatorClipInfo[] myAnimatorClip = myAnimator.GetCurrentAnimatorClipInfo (0);
		float myTime = myAnimatorClip [0].clip.length * animationState.normalizedTime;
		float animationFrameLenght = animationTimeLenght * AnimationFPS;
		float myFrame = myTime * animationFrameLenght / animationTimeLenght;
		int AnimFrameNumber = Mathf.RoundToInt (myFrame);


		Debug.Log ("AnimatorTime " + myTime + " - animationTimeLenght " + animationTimeLenght + " - animationFrameLenght " + animationFrameLenght + " - AnimatorFrame " + AnimFrameNumber);


		/*
		float nTime = 0.44333f;
		int frames = 10;
		int index = ((int)(nTime * (frames-1)))%(frames-1); // == 3

		float f = (myTime * (frames-1))%(frames-1); // == 3.99
		int lowerIndex = Mathf.FloorToInt(f);
		int upperIndex = lowerIndex + 1;
		float t = f - lowerIndex;

		int dominantFrame = Mathf.RoundToInt((myTime * (frames-1))%(frames-1)); // == 4
		Debug.Log ("dominant frame is " + dominantFrame);
		*/

	}
}