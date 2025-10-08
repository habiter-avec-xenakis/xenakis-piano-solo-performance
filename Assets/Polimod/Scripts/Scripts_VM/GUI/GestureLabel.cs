using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureLabel : MonoBehaviour
{

    Text Label;
    public GameObject Locutor;
    Locutor Loc;
    
    // Start is called before the first frame update
    void Start()
    {
        Label = gameObject.GetComponent<Text>();
        Label.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        Loc = Locutor.GetComponent<Locutor>();
        
        if (!Loc) return;

        Label.text = Loc.GestureType;
        Label.color = Loc.QualityColor;
    }
}
