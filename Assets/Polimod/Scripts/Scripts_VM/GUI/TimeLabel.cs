using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLabel : MonoBehaviour
{
    Text Label;
    public MocapPlayer MP;

    // Start is called before the first frame update
    void Start()
    {
        Label = gameObject.GetComponent<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Label.text = MP.VideoCurrentTime;
    }
}
