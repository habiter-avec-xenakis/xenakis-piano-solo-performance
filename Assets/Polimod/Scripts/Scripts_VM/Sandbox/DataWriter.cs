using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataWriter : MonoBehaviour {

	public string path = "Assets/Data/test.txt";


	// Use this for initialization
	void Start () {
		StreamWriter writer = new StreamWriter(path, true);
		writer.WriteLine("Test");
		writer.Close();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
