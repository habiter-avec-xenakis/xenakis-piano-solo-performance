//=====================================
//           Motion Recorder
// Create by Vincent MEYRUEIS 2017
// INREV Dept ATI University Paris8
//            Version 1.0
//=====================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class MotionRecorder : MonoBehaviour {

	//Data
	List<float> Times 		= new List<float>();
	List<Vector3> Pos 		= new List<Vector3> ();
	List<Vector3> Speed 	= new List<Vector3> ();
	List<Vector3> Acc 		= new List<Vector3> ();
	List<Vector3> Jerk 		= new List<Vector3> ();
	List<Quaternion> Quat 	= new List<Quaternion> ();

	//Data Tag
	public string Speed_Tag = "Speed";
	public string Acc_Tag 	= "Acc";
	public string Jerk_Tag 	= "Jerk";
	public string Pos_Tag 	= "Pos";
	public string Quat_Tag 	= "Orientation";
	public string Times_Tag	= "Time";
	public string Separator	= "\t";

	//Record
	public string path = "Assets/Data/test.csv";
	public bool Record;
	public int FrameCount;
	public float BeginTime;
	public float DTime;



	// Use this for initialization
	void Start () {
		RecordInit ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Record)
			RecordData ();

		if (Input.GetKeyDown (KeyCode.A))
			SaveData ();
	}



	void RecordInit () {

		Pos.Clear ();
		Speed.Clear ();
		Acc.Clear ();
		Jerk.Clear ();
		Quat.Clear ();
		Times.Clear ();

		FrameCount = 0;

		BeginTime = Time.time;

	}


	// Record and Compute Data
	void RecordData () {

		Times.Add(Time.time - BeginTime);

		DTime = Times [Times.Count-1] - Times [Times.Count - 2];
		//DTime = Time.deltaTime;
		//DTime = 0.016f;


		//record Position 
		Pos.Add(transform.position);

		//record Quaternion
		Quat.Add(transform.rotation);


		//record Speed
		if (Speed.Count < 1)
			Speed.Add( Vector3.zero ); 
		else
			Speed.Add( (Pos [Pos.Count-1] - Pos [Pos.Count - 2]) / DTime );

		//record Acc
		if (Acc.Count < 2)
			Acc.Add(Vector3.zero);
		else
			Acc.Add((Speed [Speed.Count-1] - Speed [Speed.Count - 2]) / DTime);

		//record Jerk
		if (Jerk.Count < 3)
			Jerk.Add(Vector3.zero);
		else
			Jerk.Add((Acc[Acc.Count-1]-Acc[Acc.Count-2])/DTime);

		FrameCount++;
	}


	void SaveData () {

		StreamWriter writer = new StreamWriter(path, true);


		string Msg = "";

		//Header
		Msg += Times_Tag + Separator;
		Msg	+= Pos_Tag + Separator;
		Msg += Speed_Tag + Separator;
		Msg += Acc_Tag + Separator;
		Msg += Jerk_Tag;

		writer.WriteLine (Msg);
		Msg = "";


		//Data
		for (int i = 0; i < FrameCount ; ++i) {

			Msg = "";

			int MillisTime = (int) (Times [i] * 1000); 

			Msg += MillisTime.ToString() + Separator;
			Msg	+= Pos [i].x.ToString() + Separator;
			Msg	+= Pos [i].y.ToString() + Separator;
			Msg	+= Pos [i].z.ToString() + Separator;


			//Msg += Speed [i].ToString() + Separator;
			//Msg += Acc [i].ToString() + Separator;
			//Msg += Jerk [i].ToString();

			writer.WriteLine (Msg);
		}

		writer.Close();

	}



}
