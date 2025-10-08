//=====================================
//           Gesture Recorder
// Create by Vincent MEYRUEIS 2018
// INREV Dept ATI University Paris8
//            Version 1.0
//=====================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GestureRecorder : MonoBehaviour {

    //Locutor
    public Locutor Locutor;
    public MocapPlayer MocapPlay;
    //Gesture CurrentGesture;

    //Arms 
    public GameObject LeftHandObj;
    HandData LeftHand = new HandData();
    MotionViewRibbon LeftHandMotion;

    public GameObject RightHandObj;
    HandData RightHand = new HandData();
    MotionViewRibbon RightHandMotion;

    //Ref
    public GameObject Ref;


    //Data Tag
    public string Gesture_ID_Tag    = "GestureID";
    public string Gesture_Type_Tag  = "GestureType";
    public string LeftHand_Tag      = "LeftHand";
    public string RightHand_Tag     = "RightHand";
    public string Speed_Tag         = "Speed";
	public string Acc_Tag 	        = "Acc";
	public string Jerk_Tag 	        = "Jerk";
	public string Pos_Tag 	        = "Pos";
	public string Quat_Tag 	        = "Orientation";
	public string Time_Tag	        = "Time";

    //Record
    public bool Record              = true;
    public string path              = "Assets/Data/Export/";
    public string GestureFileName   = "Gesture.csv";
    public string SpectrumFileName  = "Spectrum.csv";
    public string Separator         = "\t";
    public int FrameCount           = 0;
    public int SpectrumResolution   = 1024;


    //Material
    public Material LinesMaterial;
    public Material RibbonMaterial; 


    //save
    bool Save = true;


	// Use this for initialization
	public void Start(){
		RecordInit ();
	}
	
	// Update is called once per frame
	void Update () {


        RecordData();
        /*
        if(Locutor.GestureID != -1)
        {
            Record = true;
            RecordData();
        }
        else
        {
            Record = false;
        }
        */


        if (Input.GetKeyDown (KeyCode.S)|| MocapPlay.VideoNormalizedTime == 1)
        {
            if (Save) {
                SaveGestureData();
                SaveSpectrumData();
                SaveSpectrumData2();
                Save = false;
            }
        }


    }



	void RecordInit () {

        //Get Locutor
        Locutor = gameObject.GetComponent<Locutor>();
        if (!Locutor)
        {
            Locutor = gameObject.AddComponent<Locutor>();
        }


        // Allow Save
        Save = true;

        //Get Object
        RightHandObj = GameObject.Find(gameObject.name+"/Robot_References/Robot_Reference/Robot_Hips/Robot_Spine/Robot_Spine1/Robot_Spine2/Robot_Spine3/Robot_LeftShoulder/Robot_LeftArm/Robot_LeftForeArm/Robot_LeftHand");
        LeftHandObj = GameObject.Find(gameObject.name + "/Robot_References/Robot_Reference/Robot_Hips/Robot_Spine/Robot_Spine1/Robot_Spine2/Robot_Spine3/Robot_RightShoulder/Robot_RightArm/Robot_RightForeArm/Robot_RightHand");
        Ref = GameObject.Find(gameObject.name +"/Robot_References/Robot_Reference/Robot_Hips/Robot_Spine/Robot_Spine1/Robot_Spine2/Robot_Spine3");



        //Set Record on Hands
        LeftHand.Name = LeftHand_Tag;
        RightHand.Name = RightHand_Tag;

        //Set Recorder on Object
        LeftHandMotion = LeftHandObj.AddComponent<MotionViewRibbon>();
        LeftHandMotion.MotionRef = Ref;
        LeftHandMotion.Record = true;
        LeftHandMotion.Vector = false;
        LeftHandMotion.Trail = false;
        LeftHandMotion.LinesMaterials = LinesMaterial;
        LeftHandMotion.RibbonMaterials = RibbonMaterial;

        RightHandMotion = RightHandObj.AddComponent<MotionViewRibbon>();
        RightHandMotion.MotionRef = Ref;
        RightHandMotion.Record = true;
        RightHandMotion.Vector = false;
        RightHandMotion.Trail = false;
        RightHandMotion.LinesMaterials = LinesMaterial;
        RightHandMotion.RibbonMaterials = RibbonMaterial;

        //init
        FrameCount = 0;

	}


	// Record and Compute Data
	void RecordData () {

        if (Locutor.GestureNb == -1) 
            return;

        Gesture CurrentGesture = Locutor.GestureList[Locutor.GestureNb];

        //Record Data
        CurrentGesture.LeftHand.SetData(Locutor.CurrentFrame, LeftHandMotion.CurrentPos, LeftHandMotion.CurrentSpeed, LeftHandMotion.CurrentAcc, LeftHandMotion.CurrentJerk, LeftHandMotion.CurrentQuat);
        CurrentGesture.RightHand.SetData(Locutor.CurrentFrame, RightHandMotion.CurrentPos, RightHandMotion.CurrentSpeed, RightHandMotion.CurrentAcc, RightHandMotion.CurrentJerk, RightHandMotion.CurrentQuat);

        FrameCount++;
	}


	void SaveGestureData () {

        //SetFile Name
        GestureFileName = gameObject.name + "_Data_Gesture" + ".csv";
        StreamWriter writer = new StreamWriter(path+ GestureFileName, true);
        
        //Log 
        Debug.Log("Start Export Data to: " + path + GestureFileName);

        //Init Msg
		string Msg = "";

		//Header
        Msg += Gesture_ID_Tag + Separator;
        Msg += Gesture_Type_Tag + Separator;
        Msg += SetHandDataHeader(Locutor.GestureList[0].LeftHand) + Separator;  //DEBUG
        Msg += SetHandDataHeader(Locutor.GestureList[0].RightHand);             //DEBUG

        writer.WriteLine (Msg);

        //Log
        Debug.Log(Msg);



        //Write Data
        Msg = "";
        for (int g = 0; g < Locutor.GestureList.Count; g++)
        {
            for (int i = 0; i < Locutor.GestureList[g].LeftHand.Speeds.Count; ++i)  //DEBUG Take count on speed (Retake later)
            {

                //init Line
                Msg = "";

                //Data
                Msg += Locutor.GestureList[g].ID.ToString() + Separator;
                Msg += Locutor.GestureList[g].Type + Separator;
                Msg += SetHandDataValues(i, Locutor.GestureList[g].LeftHand) + Separator;
                Msg += SetHandDataValues(i, Locutor.GestureList[g].RightHand);

                writer.WriteLine(Msg);
            }
        }
		writer.Close();

    }

    //Gesture Data Export
    string SetHandDataHeader(HandData Hand)
    {
        string Msg = "";

        //Time
        Msg += Hand.Name;
        Msg += "_";
        Msg += Time_Tag;
        Msg += Separator;

        //Speed Magnitude
        Msg += Hand.Name;
        Msg += "_";
        Msg += Speed_Tag;
        Msg += Separator;

        //Acc Magnitude
        Msg += Hand.Name;
        Msg += "_";
        Msg += Acc_Tag;
        Msg += Separator;

        //Jerk Magnitude
        Msg += Hand.Name;
        Msg += "_";
        Msg += Jerk_Tag;
        Msg += Separator;

        return Msg;
    }

    string SetHandDataValues(int i, HandData Hand)
    {
        string Msg = "";

        //Time
        Msg += Hand.Times[i].ToString();
        Msg += Separator;

        //Speed Magnitude
        Msg += Hand.Speeds[i].magnitude.ToString();
        Msg += Separator;

        //Acc Magnitude
        Msg += Hand.Accs[i].magnitude.ToString();
        Msg += Separator;

        //Jerk Magnitude
        Msg += Hand.Jerks[i].magnitude.ToString();
        Msg += Separator;

        return Msg;
    }


    //Gesture Spectrum Export
    void SaveSpectrumData()
    {
        //Speed
        WriteSpectrumSpeedData(Locutor);

        //Acc
        WriteSpectrumAccData(Locutor);

        //Jerk
        WriteSpectrumJerkData(Locutor);
    }

    void WriteSpectrumSpeedData(Locutor Locutor)
   {
        
        //SetFile Name
        SpectrumFileName = gameObject.name + "_Data_" + Speed_Tag +".csv";

        StreamWriter writer = new StreamWriter(path + SpectrumFileName, true);

        //Log 
        Debug.Log("Start Export Data to: " + path + SpectrumFileName);

        //Init Msg
        string Msg = "";

        for (int g = 0; g < Locutor.GestureList.Count; ++g)  //DEBUG Take count on speed (Retake later)
        {

            //Norm Value for Left Hand
            DataAnalysis LeftData = new DataAnalysis();
            for (int i = 0; i < Locutor.GestureList[g].LeftHand.Speeds.Count; i++)
            {
                Vector2 Vect = new Vector2(Locutor.GestureList[g].LeftHand.Times[i], Locutor.GestureList[g].LeftHand.Speeds[i].magnitude);
                LeftData.Values.Add(Vect);
            }
            LeftData.NormalizedValues();

            //Norm Value for Right Hand
            DataAnalysis RightData = new DataAnalysis();
            for (int i = 0; i < Locutor.GestureList[g].RightHand.Speeds.Count; i++)
            {
                Vector2 Vect = new Vector2(Locutor.GestureList[g].RightHand.Times[i], Locutor.GestureList[g].RightHand.Speeds[i].magnitude);
                RightData.Values.Add(Vect);
            }
            RightData.NormalizedValues();

            //Init
            Msg = "";

            //Locutor 
            Msg += gameObject.name + Separator;

            //Gesture ID 
            Msg += Locutor.GestureList[g].ID.ToString() + Separator;

            //Gesture type
            Msg += Locutor.GestureList[g].Type + Separator;

            //Gesture quality
            Msg += Locutor.GestureList[g].Quality.ToString() + Separator;


            //Spectrum Left Hand Data 
            Msg += LeftHand_Tag + Separator;
            for (int i = 0 ; i <= SpectrumResolution ; i++)
            {
                Msg += LeftData.NormValues.Evaluate((float)i/ SpectrumResolution).ToString() + Separator;
            }

            //Spectrum Left Hand Data 
            Msg += RightHand_Tag + Separator;
            for (int i = 0; i <= SpectrumResolution; i++)
            {
                Msg += RightData.NormValues.Evaluate((float)i / SpectrumResolution).ToString() + Separator;
            }


            writer.WriteLine(Msg);
        }

        writer.Close();
   }

    void WriteSpectrumAccData(Locutor Locutor)
    {

        //SetFile Name
        SpectrumFileName = gameObject.name + "_Data_" + Acc_Tag + ".csv";

        StreamWriter writer = new StreamWriter(path + SpectrumFileName, true);

        //Log 
        Debug.Log("Start Export Data to: " + path + SpectrumFileName);

        //Init Msg
        string Msg = "";

        for (int g = 0; g < Locutor.GestureList.Count; ++g)  //DEBUG Take count on speed (Retake later)
        {

            //Norm Value for Left Hand
            DataAnalysis LeftData = new DataAnalysis();
            for (int i = 0; i < Locutor.GestureList[g].LeftHand.Accs.Count; i++)
            {
                Vector2 Vect = new Vector2(Locutor.GestureList[g].LeftHand.Times[i], Locutor.GestureList[g].LeftHand.Accs[i].magnitude);
                LeftData.Values.Add(Vect);
            }
            LeftData.NormalizedValues();

            //Norm Value for Right Hand
            DataAnalysis RightData = new DataAnalysis();
            for (int i = 0; i < Locutor.GestureList[g].RightHand.Accs.Count; i++)
            {
                Vector2 Vect = new Vector2(Locutor.GestureList[g].RightHand.Times[i], Locutor.GestureList[g].RightHand.Accs[i].magnitude);
                RightData.Values.Add(Vect);
            }
            RightData.NormalizedValues();
            
            //Init
            Msg = "";

            //Locutor 
            Msg += gameObject.name + Separator;

            //Gesture ID 
            Msg += Locutor.GestureList[g].ID.ToString() + Separator;

            //Gesture type
            Msg += Locutor.GestureList[g].Type + Separator;

            //Gesture quality
            Msg += Locutor.GestureList[g].Quality.ToString() + Separator;


            //Spectrum Data 
            Msg += LeftHand_Tag + Separator;
            for (int i = 0; i <= SpectrumResolution; i++) {
                Msg += LeftData.NormValues.Evaluate((float)i / SpectrumResolution).ToString() + Separator;
            }


            Msg += RightHand_Tag + Separator;
            for (int i = 0; i <= SpectrumResolution; i++) {
                Msg += RightData.NormValues.Evaluate((float)i / SpectrumResolution).ToString() + Separator;
            }

            writer.WriteLine(Msg);
        }

        writer.Close();
    }

    void WriteSpectrumJerkData(Locutor Locutor)
    {

        //SetFile Name
        SpectrumFileName = gameObject.name + "_Data_" + Jerk_Tag + ".csv";

        StreamWriter writer = new StreamWriter(path + SpectrumFileName, true);

        //Log 
        Debug.Log("Start Export Data to: " + path + SpectrumFileName);

        //Init Msg
        string Msg = "";

        for (int g = 0; g < Locutor.GestureList.Count; ++g)  //DEBUG Take count on speed (Retake later)
        {

            //Norm Value for Left Hand
            DataAnalysis LeftData = new DataAnalysis();
            for (int i = 0; i < Locutor.GestureList[g].LeftHand.Jerks.Count; i++)
            {
                Vector2 Vect = new Vector2(Locutor.GestureList[g].LeftHand.Times[i], Locutor.GestureList[g].LeftHand.Jerks[i].magnitude);
                LeftData.Values.Add(Vect);
            }
            LeftData.NormalizedValues();

            //Norm Value for Right Hand
            DataAnalysis RightData = new DataAnalysis();
            for (int i = 0; i < Locutor.GestureList[g].RightHand.Jerks.Count; i++)
            {
                Vector2 Vect = new Vector2(Locutor.GestureList[g].RightHand.Times[i], Locutor.GestureList[g].RightHand.Jerks[i].magnitude);
                RightData.Values.Add(Vect);
            }
            RightData.NormalizedValues();

            //Init
            Msg = "";

            //Locutor 
            Msg += gameObject.name + Separator;

            //Gesture ID 
            Msg += Locutor.GestureList[g].ID.ToString() + Separator;

            //Gesture type
            Msg += Locutor.GestureList[g].Type + Separator;

            //Gesture quality
            Msg += Locutor.GestureList[g].Quality.ToString() + Separator;


            //Spectrum Data 
            Msg += LeftHand_Tag + Separator;
            for (int i = 0; i <= SpectrumResolution; i++)
            {
                Msg += LeftData.NormValues.Evaluate((float)i / SpectrumResolution).ToString() + Separator;
            }


            Msg += RightHand_Tag + Separator;
            for (int i = 0; i <= SpectrumResolution; i++)
            {
                Msg += RightData.NormValues.Evaluate((float)i / SpectrumResolution).ToString() + Separator;
            }

            writer.WriteLine(Msg);
        }

        writer.Close();
    }




    void SaveSpectrumData2()
    {

        //SetFile Name
        SpectrumFileName ="Global_Data" + ".csv";

        StreamWriter writer = new StreamWriter(path + SpectrumFileName, true);

        //Log 
        Debug.Log("Start Export Data to: " + path + SpectrumFileName);

        //Init Msg
        string Msg = "";


        for (int g = 0; g < Locutor.GestureList.Count; ++g)  //DEBUG Take count on speed (Retake later)
        {
            //Init
            Msg = "";

            //Locutor 
            Msg += gameObject.name + Separator;

            //Gesture ID 
            Msg += Locutor.GestureList[g].ID.ToString() + Separator;

            //Gesture type
            Msg += Locutor.GestureList[g].Type + Separator;

            //Gesture quality
            Msg += Locutor.GestureList[g].Quality.ToString() + Separator;



            //Speed
            DataAnalysis LeftDataSpeed = new DataAnalysis();
            for (int i = 0; i < Locutor.GestureList[g].LeftHand.Speeds.Count; i++)
            {
                Vector2 Vect = new Vector2(Locutor.GestureList[g].LeftHand.Times[i], Locutor.GestureList[g].LeftHand.Speeds[i].magnitude);
                LeftDataSpeed.Values.Add(Vect);
            }
            LeftDataSpeed.NormalizedValues();

            DataAnalysis RightDataSpeed = new DataAnalysis();
            for (int i = 0; i < Locutor.GestureList[g].RightHand.Speeds.Count; i++)
            {
                Vector2 Vect = new Vector2(Locutor.GestureList[g].RightHand.Times[i], Locutor.GestureList[g].RightHand.Speeds[i].magnitude);
                RightDataSpeed.Values.Add(Vect);
            }
            RightDataSpeed.NormalizedValues();

            //Write Speed Data
            Msg += LeftHand_Tag + "Speed" + Separator;
            for (int i = 0; i <= SpectrumResolution; i++)
            {
                Msg += LeftDataSpeed.NormValues.Evaluate((float)i / SpectrumResolution).ToString() + Separator;
            }


            Msg += RightHand_Tag + "Speed" + Separator;
            for (int i = 0; i <= SpectrumResolution; i++)
            {
                Msg += RightDataSpeed.NormValues.Evaluate((float)i / SpectrumResolution).ToString() + Separator;
            }




            //Acc
            DataAnalysis LeftDataAcc = new DataAnalysis();
            for (int i = 0; i < Locutor.GestureList[g].LeftHand.Accs.Count; i++)
            {
                Vector2 Vect = new Vector2(Locutor.GestureList[g].LeftHand.Times[i], Locutor.GestureList[g].LeftHand.Accs[i].magnitude);
                LeftDataAcc.Values.Add(Vect);
            }
            LeftDataAcc.NormalizedValues();

            DataAnalysis RightDataAcc = new DataAnalysis();
            for (int i = 0; i < Locutor.GestureList[g].RightHand.Accs.Count; i++)
            {
                Vector2 Vect = new Vector2(Locutor.GestureList[g].RightHand.Times[i], Locutor.GestureList[g].RightHand.Accs[i].magnitude);
                RightDataAcc.Values.Add(Vect);
            }
            RightDataAcc.NormalizedValues();

            //Write Acc Data
            Msg += LeftHand_Tag + "Acc" + Separator;
            for (int i = 0; i <= SpectrumResolution; i++)
            {
                Msg += LeftDataAcc.NormValues.Evaluate((float)i / SpectrumResolution).ToString() + Separator;
            }


            Msg += RightHand_Tag + "Acc" + Separator;
            for (int i = 0; i <= SpectrumResolution; i++)
            {
                Msg += RightDataAcc.NormValues.Evaluate((float)i / SpectrumResolution).ToString() + Separator;
            }



            //Jerk
            DataAnalysis LeftDataJerk = new DataAnalysis();
            for (int i = 0; i < Locutor.GestureList[g].LeftHand.Jerks.Count; i++)
            {
                Vector2 Vect = new Vector2(Locutor.GestureList[g].LeftHand.Times[i], Locutor.GestureList[g].LeftHand.Jerks[i].magnitude);
                LeftDataJerk.Values.Add(Vect);
            }
            LeftDataJerk.NormalizedValues();

            DataAnalysis RightDataJerk = new DataAnalysis();
            for (int i = 0; i < Locutor.GestureList[g].RightHand.Jerks.Count; i++)
            {
                Vector2 Vect = new Vector2(Locutor.GestureList[g].RightHand.Times[i], Locutor.GestureList[g].RightHand.Jerks[i].magnitude);
                RightDataJerk.Values.Add(Vect);
            }
            RightDataJerk.NormalizedValues();


            //Write Jerk Data
            Msg += LeftHand_Tag + "Jerk" + Separator;
            for (int i = 0; i <= SpectrumResolution; i++)
            {
                Msg += LeftDataJerk.NormValues.Evaluate((float)i / SpectrumResolution).ToString() + Separator;
            }


            Msg += RightHand_Tag + "Jerk" + Separator;
            for (int i = 0; i <= SpectrumResolution; i++)
            {
                Msg += RightDataJerk.NormValues.Evaluate((float)i / SpectrumResolution).ToString() + Separator;
            }

            writer.WriteLine(Msg);
        }

        writer.Close();
    }

}






