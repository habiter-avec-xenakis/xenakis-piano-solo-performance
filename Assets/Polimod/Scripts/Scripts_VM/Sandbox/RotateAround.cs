using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

	public Vector3 Center;
	public Vector3 Axis;
	public float angle;
	public Vector3 translate;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.RotateAround (Center, Axis, angle * Time.deltaTime );
		transform.Translate (translate);

	}
}
