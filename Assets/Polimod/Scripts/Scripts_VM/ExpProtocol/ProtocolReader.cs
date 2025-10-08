//=====================================
//          Protocol Reader
// Create by Vincent MEYRUEIS 2018
// INREV Dept ATI University Paris8
//            Version 1.0
//=====================================


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;
using System.Globalization;

public class ProtocolReader : MonoBehaviour {

    //File Path
    public string path = "Assets/Data/MocapParams/MocapListForUnity.csv";
    public char Separator = '\t';
    public int LineID = 0;

    //Video
    public VideoPlayer videoPlayer;
    //public VideoClip videoClip;
    public string videoName = ""; 
    public string Videopath = "Assets/Data/Video/";

    //Mocap
    public MocapPlayer MocapPlayer;

    public string Mocap1AnimName = "";
    public Animator Mocap1Anim;
    public Vector3 Mocap1Position;
    public Quaternion Mocap1Orientation;
    public float Mocap1Offset = 0.0f;

    public string Mocap2AnimName = "";
    public Animator Mocap2Anim;
    public Vector3 Mocap2Position;
    public Quaternion Mocap2Orientation; 
    public float Mocap2Offset = 0.0f;

    //ELan
    public ElanReader ElanReader;
    public string ElanName = "";
    public string ElanPath = "Assets/Data/Elan/";

    //Locutor
    public string Locutor1Name = "";
    public GameObject Locutor1;

    public string Locutor2Name = "";
    public GameObject Locutor2; 


    // Use this for initialization
    void Awake () {


        ReadFile();

        SetObject();

        SetParameters();

        //Restart();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void SetObject()
    {
        //Mocap Player
        MocapPlayer = gameObject.GetComponent<MocapPlayer>();
        if (!MocapPlayer)
        {
            MocapPlayer = gameObject.AddComponent<MocapPlayer>();
        }

        //Elan
        ElanReader = gameObject.GetComponent<ElanReader>();
        if (!MocapPlayer)
        {
            ElanReader = gameObject.AddComponent<ElanReader>();
        }

        //Video Player
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        if (!videoPlayer)
        {
            videoPlayer = gameObject.AddComponent<VideoPlayer>();
        }

        //Animator
        Mocap1Anim = Locutor1.GetComponent<Animator>();
        Mocap2Anim = Locutor2.GetComponent<Animator>();



    }

    void ReadFile()
    {
        string[] Data = File.ReadAllLines(path);
        string[] SubData;
       
        SubData = Data[LineID].Split(Separator);


            //Video
            videoName = SubData[0];

            //Mocap Files
            Mocap1AnimName = SubData[1];
            Mocap2AnimName = SubData[2];


            //Locutors Name
            Locutor1Name = SubData[3];
            Locutor2Name = SubData[4];

            //Elan Path
            ElanName = SubData[5];

            //MocapOffset
            Mocap1Offset = float.Parse(SubData[6], CultureInfo.InvariantCulture);
            Mocap2Offset = float.Parse(SubData[7], CultureInfo.InvariantCulture);

            //Mocap POS
            //Mocap1Position = Vector3.Par
            //Mocap1Orientation = 

            //Mocap1Position = Vector3.Par
            //Mocap1Orientation =

    }

    void SetParameters()
    {

        //set Locutors
        Locutor1.name = Locutor1Name;
        Locutor2.name = Locutor2Name;

        //Set Animator
        Mocap1Anim.runtimeAnimatorController = Resources.Load("AnimatorController/" + Mocap1AnimName) as RuntimeAnimatorController;
        Mocap2Anim.runtimeAnimatorController = Resources.Load("AnimatorController/" + Mocap2AnimName) as RuntimeAnimatorController;

        //Set Video
        videoPlayer.url = "file:///"+Videopath+videoName;

        //Set Mocap Offset
        MocapPlayer.MocapObj1 = Locutor1;
        MocapPlayer.Mocap1Offset = Mocap1Offset;
        Locutor1.SetActive(false);

        MocapPlayer.MocapObj2 = Locutor2;
        MocapPlayer.Mocap2Offset = Mocap2Offset;
        Locutor2.SetActive(false);


        //Set Elean Parms
        ElanReader.path = ElanPath+ElanName;
        ElanReader.Locutor1 = Locutor1;
        ElanReader.Locutor2 = Locutor2;

    }

    public void Restart()
    {
        ElanReader.Start();
        MocapPlayer.Start();

        ReadFile();

        SetObject();

        SetParameters();

        videoPlayer.Play();
    }

}
