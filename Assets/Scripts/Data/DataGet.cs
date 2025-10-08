using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
using Klak.Timeline.Midi;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DataGet : MonoBehaviour
{
    public DataSet[] dataSets;
    public string dataPath;
    public DataPlayback dataPlayback;

    public UnityEvent dataSetsFound = new UnityEvent();

    void Start()
    {
        GetAllDataSets();
    }

    void GetAllDataSets()
    {
        DirectoryInfo dirInfo = new DirectoryInfo(dataPath);
        FileInfo[] fileInfo = dirInfo.GetFiles();

        List<string> nameList = new List<string>();

        foreach (FileInfo file in fileInfo)
        {
            string name = Path.GetFileName(file.ToString());
            //Debug.Log("[ORIGINAL]" + name);

            if (name == "._.DS_Store" || name == ".DS_Store")
            {
                continue;
            }

            name = name.Split('.')[0];
            //Debug.Log("[FILTERED]" + name);

            if(!nameList.Contains(name))
            {
                nameList.Add(name);
            }
        }

        List<DataSet> dataSetList = new List<DataSet>();
        foreach(string n in nameList)
        {
            DataSet dataSet = new DataSet();
            GetDataSetPaths(dataSet, dataPath, n);
            dataSetList.Add(dataSet);
        }
        dataSets = dataSetList.ToArray();
        Debug.Log("DataSetFound invoke");
        dataSetsFound.Invoke();
    }

    private void GetDataSetPaths(DataSet dataSet, string path, string name)
    {
        dataSet.pathsVideo = new string[2];

        DirectoryInfo dirInfo = new DirectoryInfo(path);
        FileInfo[] fileInfo = dirInfo.GetFiles();

        foreach (FileInfo file in fileInfo)
        {
            if(file.ToString().Contains(name))
            {
                string fileName = Path.GetFileName(file.ToString());
                string[] fileNameSplit = fileName.Split('.');

                //Debug.Log("Split count : " + fileNameSplit.Length);

                if(fileNameSplit.Length == 2)
                {
                    switch(fileNameSplit[1])
                    {
                        case ("aif"):
                            //Debug.Log("[DataGet] Found audio.");
                            dataSet.pathAudio = file.ToString();
                            break;
                        case ("mid"):
                            //Debug.Log("[DataGet] Found midi.");
                            dataSet.pathMidi = file.ToString();
                            break;
                        case ("mubu"):
                            //Debug.Log("[DataGet] Found mubu.");
                            break;
                        case ("txt"):
                            //Debug.Log("[DataGet] Found text file.");
                            break;
                    }
                }

                if (fileNameSplit.Length == 3)
                {
                    if (fileNameSplit[2] == "mov")
                    {
                        if (fileNameSplit[1] == "v1")
                        {
                            dataSet.pathsVideo[0] = file.ToString();
                        }
                        if (fileNameSplit[1] == "v2")
                        {
                            dataSet.pathsVideo[1] = file.ToString();
                        }
                    }

                    if (fileNameSplit[1] == "midi" && fileNameSplit[2] == "txt")
                    {
                        dataSet.pathMidiTxt = file.ToString();
                    }
                }

            }
        }
    }

    void Update()
    {
        
    }
}