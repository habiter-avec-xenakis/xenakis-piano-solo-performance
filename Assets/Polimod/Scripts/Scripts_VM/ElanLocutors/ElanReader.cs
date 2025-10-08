//=====================================
//            Elan Reader
// Create by Vincent MEYRUEIS 2018
// INREV Dept ATI University Paris8
//            Version 1.0
//=====================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ElanReader : MonoBehaviour
{

    //Path
    public string path = "Assets/Data/Elan.";
    public char Separator = '\t';
    string[] Data;

    //Frame
    //public GameObject MocapPlayer; 
    public int CurrentFrame = 0;
    public float FrameTime;

    //Global Gesture
    public List<Gesture> LoadGestureList = new List<Gesture>();
    public int LoadGestureListCount;

    //Locuteur 1
    public string Locutor1Name = "";
    public GameObject Locutor1;
    public List<Gesture> Locutor1GestureList = new List<Gesture>();
    public int Locutor1GestureListCount;

    //Locuteur 2
    public string Locutor2Name = "";
    public GameObject Locutor2;
    public List<Gesture> Locutor2GestureList = new List<Gesture>();
    public int Locutor2GestureListCount;

    //Quality
    public string BestQualityName = "dac";
    public Color BestQualityColor = Color.green;

    public string NormalQualityName = "padac";
    public Color NormalQualityColor = Color.yellow;

    public string WorstQualityName = "granpadac";
    public Color WorstQualityColor = Color.red;

    public Gradient QualityColors;

    bool start = false;


    // Use this for initialization
    public void Start()
    {
        // Read Elan File
        ReadElanFile();

        //Compute Color Quality 
        ComputeQualityGradient();

        //Set Locutors
        SetLocutor(Locutor1, Locutor1Name, Locutor1GestureList);
        SetLocutor(Locutor2, Locutor2Name, Locutor2GestureList);

        start = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (!start)
        {
            return;
        }

        //get time frame
        CurrentFrame = (int)gameObject.GetComponent<MocapPlayer>().ElanTime;

        //Update Frame
        Locutor1.GetComponent<Locutor>().CurrentFrame = CurrentFrame;
        Locutor2.GetComponent<Locutor>().CurrentFrame = CurrentFrame;


        List<Gesture> GList = Locutor1.GetComponent<Locutor>().GestureList;

    }

    //Read ELan export file
    void ReadElanFile()
    {

        Data = File.ReadAllLines(path);
        ReadElanHeader();
        ReadElanData();

    }

    // Read Header of Elan Export
    void ReadElanHeader()
    {
        string[] SubData;
        char Separator = '\t';

        SubData = Data[0].Split(Separator);

        //int CurrentBoneID = 0;
        for (int i = 0; i < SubData.Length; i++)
        {
            Locutor1Name = SubData[2];
            Locutor2Name = SubData[4];
        }
    }

    // Read Header of Elan Export
    void ReadElanData()
    {
        string[] SubData;


        for (int i = 1; i < Data.Length; i++)
        {

            SubData = Data[i].Split(Separator);

            Gesture CurrentGesture = new Gesture();
            LoadGestureList.Add(CurrentGesture);

            CurrentGesture.StartFrame = int.Parse(SubData[0]);
            CurrentGesture.StopFrame = int.Parse(SubData[1]);
            CurrentGesture.ComputeDuration();


            //Default Value
            int LocAdr = 2;


            if (SubData[2] == "")
            {
                LocAdr = 3;
                CurrentGesture.Locutor = Locutor2;
                CurrentGesture.LocutorName = Locutor2Name;
                Locutor2GestureList.Add(CurrentGesture);
            }
            else
            {
                LocAdr = 2;
                CurrentGesture.Locutor = Locutor1;
                CurrentGesture.LocutorName = Locutor1Name;
                Locutor1GestureList.Add(CurrentGesture);
            }

            // Gesture ID
            CurrentGesture.ID = i;

            // Gesture Type
            CurrentGesture.Type = SubData[LocAdr];

            //Gesture Quality 
            CurrentGesture.Quality = 0;

            if (SubData[LocAdr + 2] == BestQualityName)
            {
                CurrentGesture.Quality = 1;
            }

            if (SubData[LocAdr + 2] == NormalQualityName)
            {
                CurrentGesture.Quality = 0.5f;
            }

            if (SubData[LocAdr + 2] == WorstQualityName)
            {
                CurrentGesture.Quality = 0;
            }

        }

        //Set Gesture Count:
        LoadGestureListCount = LoadGestureList.Count;
        Locutor1GestureListCount = Locutor1GestureList.Count;
        Locutor2GestureListCount = Locutor2GestureList.Count;

    }

    // Set Locutor on Object
    void SetLocutor(GameObject Obj, string Name, List<Gesture> GList)
    {
        Locutor Loc = Obj.GetComponent<Locutor>();
        if (!Loc)
        {
            Loc = Obj.AddComponent<Locutor>();
        }

        //Name
        Loc.Name = Name;
        Loc.QualityColors = QualityColors;

        // DEBUG / DEGUEU
        for (int i = 0; i < GList.Count; i++)
        {
            Loc.GestureList.Add(GList[i]);
        }


    }

    // Compute Color graient From Colors parameters
    void ComputeQualityGradient()
    {
        //Gradient QualityColors = new Gradient;

        GradientColorKey[] QualityColorKeys = new GradientColorKey[3];

        QualityColorKeys[0].color = BestQualityColor;
        QualityColorKeys[0].time = 1;

        QualityColorKeys[1].color = NormalQualityColor;
        QualityColorKeys[1].time = 0.5f;

        QualityColorKeys[2].color = WorstQualityColor;
        QualityColorKeys[2].time = 0;

        GradientAlphaKey[] QualityAlphaKeys = new GradientAlphaKey[2];

        QualityAlphaKeys[0].alpha = 1;
        QualityAlphaKeys[0].time = 1;

        QualityAlphaKeys[0].alpha = 1;
        QualityAlphaKeys[0].time = 0;


        QualityColors.SetKeys(QualityColorKeys, QualityAlphaKeys);

    }
}


