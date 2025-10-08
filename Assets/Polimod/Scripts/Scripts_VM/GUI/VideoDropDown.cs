using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class VideoDropDown : MonoBehaviour
{

    public string path = "Assets/Data/MocapParams/MocapListForUnity.tsv";
    public char Separator = '\t';

    public ProtocolReader Protocol; 

    public List<string> VideoNames;

    public Dropdown VideoDrop;

    //public int Adr = 0;



    // Start is called before the first frame update
    void Start()
    {
        Protocol = GameObject.Find("ExpPlayer").GetComponent<ProtocolReader>();

        VideoDrop = gameObject.GetComponent<Dropdown>();

        path = Protocol.path;
        Separator = Protocol.Separator;

        ReadFile();
    }

    void ReadFile()
    {
        string[] Data = File.ReadAllLines(path);

        for (int i = 1; i < Data.Length ; i++)
        {
            string[] SubData = Data[i].Split(Separator);
            //string[] SubData2 = SubData[0].Split(Separator2);

            //Video
            VideoNames.Add(SubData[0]);
        }

        VideoDrop.AddOptions(VideoNames);
        VideoDrop.value = Protocol.LineID - 1;
    }

    


    // Update is called once per frame
    public void UpdateValue()
    {
        Protocol.LineID = VideoDrop.value + 1;
        Protocol.Restart();
    }


}
