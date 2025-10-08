using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ArmAnalysis : MonoBehaviour {

	public GameObject Shoulder; 
	public GameObject Arm; 
	public GameObject ForArm; 
	public GameObject Hand; 

	public float BeginTime;
	public float TimeFrame;

	public int CurrentFrame;
	public int FrameCount; 
	public bool RealTime;

	public bool DataExport;
	public string path = "Assets/Data/test.csv";
	public string Separator	= "\t";

	List<Vector3> ArmEuler;
	List<Vector3> ForArmEuler;
	List<Vector3> HandEuler;


	public Vector3 CurrentArmEuler;
	public Vector3 CurrentForArmEuler;
	public Vector3 CurrentHandEuler;

	//Speed
	List<Vector3> ArmEulerSpeed;
	List<Vector3> ForArmEulerSpeed;
	List<Vector3> HandEulerSpeed;

	//Acc
	List<Vector3> ArmEulerAcc;
	List<Vector3> ForArmEulerAcc;
	List<Vector3> HandEulerAcc;

	//Jerk
	List<Vector3> ArmEulerJerk;
	List<Vector3> ForArmEulerJerk;
	List<Vector3> HandEulerJerk;

	List<float> TimeStamp;

	float ShoulderEulerSpeedMag;
	float ArmEulerSpeedMag;
	float ForArmSpeedMag;
	float HandEulerSpeedMag;

	string Msg;


	// Use this for initialization
	void Start () {

		RecordInit ();

	}
	
	// Update is called once per frame
	void Update () {

		if (RealTime)
			FrameUpdate ();


		if (Input.GetKeyDown (KeyCode.S))
			DataExpt ();
		
	}

	void RecordInit () {

		CurrentFrame = 0;

		TimeStamp = new List<float>();

		ArmEuler = new List<Vector3> ();
		ForArmEuler = new List<Vector3> ();
		HandEuler = new List<Vector3> ();

		ArmEulerSpeed= new List<Vector3> ();
		ForArmEulerSpeed= new List<Vector3> ();
		HandEulerSpeed= new List<Vector3> ();

		ArmEulerAcc= new List<Vector3> ();
		ForArmEulerAcc= new List<Vector3> ();
		HandEulerAcc= new List<Vector3> ();

		ArmEulerJerk= new List<Vector3> ();
		ForArmEulerJerk= new List<Vector3> ();
		HandEulerJerk= new List<Vector3> ();

		//BeginTime = Time.time;

	}



	void FrameUpdate () {

		//Compute Local Coor
		CurrentArmEuler = Arm.transform.localEulerAngles;
		CurrentForArmEuler = ForArm.transform.localEulerAngles;
		CurrentHandEuler = Hand.transform.localEulerAngles;


		ArmEuler.Add (CurrentArmEuler);
		ForArmEuler.Add (CurrentForArmEuler); 
		HandEuler.Add (CurrentHandEuler);

		ArmEulerSpeed.Add (Deriv (ArmEuler, TimeFrame));
		ForArmEulerSpeed.Add (Deriv (ForArmEuler, TimeFrame)); 
		HandEulerSpeed.Add (Deriv (HandEuler, TimeFrame));

		ArmEulerAcc.Add (Deriv(ArmEulerSpeed,TimeFrame));
		ForArmEulerAcc.Add (Deriv(ForArmEulerSpeed,TimeFrame)); 
		HandEulerAcc.Add (Deriv(HandEulerSpeed,TimeFrame));

		ArmEulerJerk.Add (Deriv(ArmEulerAcc,TimeFrame));
		ForArmEulerJerk.Add (Deriv(ForArmEulerAcc,TimeFrame)); 
		HandEulerJerk.Add (Deriv(HandEulerAcc,TimeFrame));

		if (RealTime)
			TimeFrame = Time.deltaTime*1000;

		if (TimeStamp.Count < 1)
			TimeStamp.Add (0.0f);
		else
			TimeStamp.Add (TimeStamp[TimeStamp.Count-1] + TimeFrame);

		CurrentFrame++;
	}

	Vector3 Deriv(List<Vector3> Value,float DTime){
		if (Value.Count<2)
			return Vector3.zero;
		return ( Value[Value.Count - 1] - Value [Value.Count - 2] ) / DTime;
	}


	void DataExpt () {


		StreamWriter writer = new StreamWriter(path, true);

		//Header
		Msg = "";

		Msg += "TimeStamp" + Separator;
		WriteObjectHeader("ArmEuler");
		WriteObjectHeader("ForArmEuler");
		WriteObjectHeader("HandEuler");

		writer.WriteLine (Msg);

		//Data
		for (int i = 0; i < FrameCount ; i++) {
			Msg = "";

			int MillisTime = (int) (TimeStamp [i]); 
			Msg += MillisTime.ToString() + Separator;

			WriteVector (ArmEuler[i]);
			WriteVector (ArmEulerSpeed[i]);
			WriteVector (ArmEulerAcc[i]);
			WriteVector (ArmEulerJerk[i]);

			WriteVector (ForArmEuler[i]);
			WriteVector (ForArmEulerSpeed[i]);
			WriteVector (ForArmEulerAcc[i]);
			WriteVector (ForArmEulerJerk[i]);

			WriteVector (HandEuler[i]);
			WriteVector (HandEulerSpeed[i]);
			WriteVector (HandEulerAcc[i]);
			WriteVector (HandEulerJerk[i]);

			writer.WriteLine (Msg);
		}
		writer.Close();
	}

	void WriteVector(Vector3 Vect) {
		Msg	+= Vect.x.ToString() + Separator;
		Msg	+= Vect.y.ToString() + Separator;
		Msg	+= Vect.z.ToString() + Separator;
	}

	void WriteVectorHeader(string VectHeader) {
		Msg	+= VectHeader + "-x" + Separator;
		Msg	+= VectHeader + "-y" + Separator;
		Msg	+= VectHeader + "-z" + Separator;
	}

	void WriteObjectHeader(string ObjectHeader) {
		WriteVectorHeader (ObjectHeader + "-Angles");
		WriteVectorHeader (ObjectHeader + "-Speed");
		WriteVectorHeader (ObjectHeader + "-Acc");
		WriteVectorHeader (ObjectHeader + "-Jerk ");
	}



}
