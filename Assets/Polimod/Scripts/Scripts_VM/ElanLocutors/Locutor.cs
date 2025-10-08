//=====================================
//             Locutor
// Create by Vincent MEYRUEIS 2018
// INREV Dept ATI University Paris8
//            Version 1.0
//=====================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locutor : MonoBehaviour {

    public string Name = "Locutor Name";
    public List<Gesture> GestureList = new List<Gesture>();
    public int CurrentFrame = 0;
    public int GestureNb = -1;
    public int GestureElanID = -1;
    public string Time = "";
    public int GestureQuality = 0;

    //Gesture type
    public string GestureType = "";

    //Quality
    public Gradient QualityColors = new Gradient();
    public Color QualityColor = Color.black;


	// Update is called once per frame
	void Update () {

        //Find Gesture 
        GestureType = "";
        GestureNb = -1;
        GestureElanID = -1;

        for (int i = 0; i < GestureList.Count ; i++){
            if ( CurrentFrame >= GestureList[i].StartFrame) { 
                if (CurrentFrame <= GestureList[i].StopFrame){
                    GestureType = GestureList[i].Type;
                    GestureNb = i;
                    GestureElanID = GestureList[i].ID;
                    QualityColor = QualityColors.Evaluate(GestureList[i].Quality);
                }
            }
        }

        Time = FormatTime((float) CurrentFrame);

	}

    // Time to String
    string FormatTime(float time)
    {
        time /= 1000;
        int intTime = (int)time;
        int minutes = intTime / 60;
        int seconds = intTime % 60;
        float fraction = time * 1000;
        fraction = (fraction % 1000);
        string timeText = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, fraction);
        return timeText;
    }

}

