using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphScript : MonoBehaviour {

	public GameObject MotionView;

	//public float Value;
	public float Width = 1;
	public float Lengh = 2;
	public int FrameResolution = 500;

	public int MaxValue = 1000;
	public int MinValue = 0;

	//Data Viz Property
	public Color AxisColor = Color.black;

	public bool SpeedActive = false;
	//[Range (0,100)]
	public float SpeedScale = 1;
	public Color SpeedColor = Color.green;
	public float CurrentSpeedMag;

	public bool AccActive = false;
	//[Range (0,100)]
	public float AccScale = 1;
	public Color AccColor = Color.red;
	public float CurrentAccMag;

	public bool JerkActive = false;
	//[Range (0,100)]
	public float JerkScale = 1;
	public Color JerkColor = Color.blue;
	public float CurrentJerkMag;

	public bool OrientationActive = false;
	//[Range (0,100)]
	public float QuatScale = 1;
	public Color QuatColor = Color.yellow;
	public float CurrentQuatAngle;

	//Lines width
	public float AxisWidth;
	public float LinesWidth;

	//Trail 
	GameObject Speed_Trail;
	GameObject Acc_Trail; 
	GameObject Jerk_Trail;
	GameObject Quat_Trail;

	//Values
	List<float> Speeds;
	List<float> Accs;
	List<float> Jerks;
	List<float> Quats;

	//Offsets
	public float speedOffset;
	public float accOffset;
	public float jerkOffset;
	public float quatsOffset;

	//Axis
	GameObject XAxis; 
	GameObject YAxis;

	public Material LinesMaterials;

	public List<LineRenderer> lineRenderersTrails = new List<LineRenderer>();
	public List<LineRenderer> lineRenderersVectors = new List<LineRenderer>();

	// Use this for initialization
	void Start () {

		Speeds = new List<float> ();
		Accs = new List<float> ();
		Jerks = new List<float> ();
		Quats = new List<float> ();
	
		Speed_Trail = CreatTrailObject("Speed",SpeedColor);
		Acc_Trail = CreatTrailObject("Acc",AccColor);
		Jerk_Trail = CreatTrailObject("Jerk",JerkColor);
		Quat_Trail = CreatTrailObject("Quat",QuatColor); 

		XAxis = CreatVectObject("XAxis",AxisColor );
		YAxis = CreatVectObject("YAxis",AxisColor );
	}
	
	// Update is called once per frame
	void Update () {
		UpdateValues ();
		UpdateGraph ();
	}

	void UpdateValues (){

		MotionView TrackedObj = MotionView.GetComponent<MotionView> ();

		Speeds.Add (TrackedObj.CurrentSpeedMag);
		Accs.Add (TrackedObj.CurrentAccMag);
		Jerks.Add (TrackedObj.CurrentJerkMag);
		Quats.Add (TrackedObj.CurrentQuatAngle);
	}



	void UpdateGraph (){

		DrawGraph (Speed_Trail,Speeds,SpeedScale,speedOffset);
		DrawGraph (Acc_Trail,Accs,AccScale,accOffset);
		DrawGraph (Jerk_Trail,Jerks,JerkScale,jerkOffset);
		DrawGraph (Quat_Trail,Quats,QuatScale,quatsOffset);

		DrawVector (XAxis,Vector3.right*Lengh,AxisWidth);
		DrawVector (YAxis,Vector3.up*Width,AxisWidth);
	}




	void DrawGraph (GameObject Obj,List<float> Values,float Scale, float offset){
		LineRenderer GraphLine = Obj.GetComponent<LineRenderer> ();
		Matrix4x4 InvMat = Obj.transform.localToWorldMatrix;
	
		float Step = Lengh / FrameResolution;
		Vector3 GraphPos = Vector3.zero; 

		int Length = ( Values.Count < FrameResolution) ? Values.Count : FrameResolution ;
		GraphLine.positionCount = Length;

		for (int i = 0; i < Length; i++) {
			GraphPos.y = Map(Values [Values.Count - 1 - i]*Scale,MinValue ,MaxValue,0,Width) + offset;
			GraphPos.x = i * Step;
			Vector3 TPos = InvMat * (GraphPos);
			GraphLine.SetPosition (i, TPos + gameObject.transform.position);

		}

		GraphLine.startWidth = LinesWidth;
		GraphLine.endWidth = LinesWidth;
	
	}


	float Map (float x, float x1, float x2, float y1,  float y2)
	{
		var m = (y2 - y1) / (x2 - x1);
		var c = y1 - m * x1; // point of interest: c is also equal to y2 - m * x2, though float math might lead to slightly different results.
		return m * x + c;
	}



	//Draw Vectors with Line render
	void DrawVector(GameObject Obj,Vector3 CurrentVect,float VectLinesWidth) {

		LineRenderer VectLine = Obj.GetComponent<LineRenderer> ();

		Vector3 TVect = Obj.transform.localToWorldMatrix * CurrentVect;

		VectLine.positionCount = 2;
		VectLine.SetPosition (0, Obj.transform.position);
		VectLine.SetPosition (1, Obj.transform.position + TVect);

		VectLine.startWidth = VectLinesWidth;
		VectLine.endWidth = 0.01f*VectLinesWidth;
		//	VectLine.widthMultiplier = 0.5f;
	}	


	//Creat Vector Object Parent to track Object
	GameObject CreatVectObject(string Name,Color Col){

		GameObject VectObject = new GameObject ();
		VectObject.layer = this.gameObject.layer;
		VectObject.transform.position = gameObject.transform.position;
		VectObject.transform.rotation = gameObject.transform.rotation;
		VectObject.name = Name+ "_Vect";
		VectObject.transform.parent = gameObject.gameObject.transform;
		LineRenderer Lines = VectObject.AddComponent<LineRenderer>();
		Lines.material = LinesMaterials;

		Lines.colorGradient = SetVectColorGradient(Col);

		Lines.receiveShadows = false;
		Lines.useWorldSpace = true;

		lineRenderersVectors.Add(Lines);

		return VectObject;
	} 
		

	//Creat Trail Object Parent to track Object
	GameObject CreatTrailObject(string Name,Color Col){

		GameObject TrailObject = new GameObject ();
		TrailObject.layer = this.gameObject.layer;
		TrailObject.transform.position = gameObject.transform.position;
		TrailObject.transform.rotation = gameObject.transform.rotation;
		TrailObject.name = Name+ "_Trail";
		TrailObject.transform.parent = gameObject.gameObject.transform;
		LineRenderer Lines = TrailObject.AddComponent<LineRenderer>();
		Lines.material = LinesMaterials;

		Lines.colorGradient = SetColorGradient(Col);

		Lines.receiveShadows = false;
		Lines.useWorldSpace = true;

		//TrailObjects.Add (TrailObject);
		lineRenderersTrails.Add(Lines);

		return TrailObject;
	} 


	//Compute Gradiant for Trail Fade Color
	Gradient SetColorGradient(Color Col){
		Gradient ColGrad = new Gradient ();
		GradientColorKey[] KeysColor = new GradientColorKey[2];
		KeysColor [0].color = Col;
		KeysColor [0].time = 0;
		KeysColor [1].color = Col;
		KeysColor [1].time = 1;

		GradientAlphaKey[] KeysAlf= new GradientAlphaKey[2];
		KeysAlf [0].alpha = 1;
		KeysAlf [0].time = 0;
		KeysAlf [1].alpha = 0;
		KeysAlf [1].time = 1;

		ColGrad.SetKeys (KeysColor, KeysAlf);
		return ColGrad;
	}

	//Compute Gradiant for Vector Full Color
	Gradient SetVectColorGradient(Color Col){
		Gradient ColGrad = new Gradient ();
		GradientColorKey[] KeysColor = new GradientColorKey[2];
		KeysColor [0].color = Col;
		KeysColor [0].time = 0;
		KeysColor [1].color = Col;
		KeysColor [1].time = 1;

		GradientAlphaKey[] KeysAlf= new GradientAlphaKey[2];
		KeysAlf [0].alpha = 1;
		KeysAlf [0].time = 0;
		KeysAlf [1].alpha = 1;
		KeysAlf [1].time = 1;

		ColGrad.SetKeys (KeysColor, KeysAlf);
		return ColGrad;
	}



}
