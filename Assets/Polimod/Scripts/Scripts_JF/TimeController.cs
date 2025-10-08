using UnityEngine;
using System.Collections;

public class TimeController : MonoBehaviour
{

	[Range (0, 10F)]
	public float timefloat = 1.0F;
	 
	// Use this for initialization
	void Start ()
	{

		Time.timeScale = timefloat;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Time.timeScale = timefloat;

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			timefloat = timefloat - 0.05F;
			Debug.Log (Time.timeScale);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			timefloat = timefloat + 0.05F;
			Debug.Log (Time.timeScale);
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			timefloat = 0.0F;
			Debug.Log (Time.timeScale);
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			timefloat = 1.0F;
			Debug.Log (Time.timeScale);
		}
	}
}
