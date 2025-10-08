using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailsPanel : MonoBehaviour
{
    public bool Active = false;
    public GameObject Locutor;
    public MotionViewRibbon[] HandsTrail;

    public Toggle Trail;
    public Toggle Speed;
    public Toggle Acc;
    public Toggle Jerk;

    public Toggle RightHand;
    public Toggle LeftHand;

    public Slider Lenght;
    public int MinLenght = 0;
    public int MaxLenght = 100;


    // Start is called before the first frame update
    void Start()
    {
        Trail = GameObject.Find(gameObject.name + "/Trail").GetComponent<Toggle>();
        Speed = GameObject.Find(gameObject.name + "/Speed").GetComponent<Toggle>();
        Acc = GameObject.Find(gameObject.name + "/Acc").GetComponent<Toggle>();
        Jerk = GameObject.Find(gameObject.name + "/Jerk").GetComponent<Toggle>();

        RightHand = GameObject.Find(gameObject.name + "/Right Hand").GetComponent<Toggle>();
        LeftHand = GameObject.Find(gameObject.name + "/Left Hand").GetComponent<Toggle>();

        Lenght = GameObject.Find(gameObject.name + "/Lenght").GetComponent<Slider>();




    }

    // Update is called once per frame
    void Update()
    {
        HandsTrail = Locutor.GetComponentsInChildren<MotionViewRibbon>();

        if (HandsTrail.Length == 0 || !Locutor.activeSelf )
        {
            Disactivate();
            return;
        }

        Activate();

        for (int i = 0; i < HandsTrail.Length; i++)
        {
            SetValues(HandsTrail[i]);
            SetTrailsParams(HandsTrail[i]);
        }



        HandsTrail[1].Trail = RightHand.isOn;
        HandsTrail[0].Trail = LeftHand.isOn;

    }

    void SetValues (MotionViewRibbon Hand )
    {
        Hand.Trail = true;
        Hand.SpeedActive = Speed.isOn;
        Hand.AccActive = Acc.isOn;
        Hand.JerkActive = Jerk.isOn;
        Hand.PosActive = Trail.isOn;
        Hand.TrailLength = (int)Lenght.value;
    }

    public void Activate()
    {
        Active = true;
        Trail.interactable = true;
        Speed.interactable = true;
        Acc.interactable = true;
        Jerk.interactable = true;
        RightHand.interactable = true;
        LeftHand.interactable = true;
        Lenght.interactable = true;
    }

    public void Disactivate()
    {
        Active = false;
        Trail.interactable = false;
        Speed.interactable = false;
        Acc.interactable = false;
        Jerk.interactable = false;
        RightHand.interactable = false;
        LeftHand.interactable = false;
        Lenght.interactable = false;
    }

    void SetTrailsParams(MotionViewRibbon Hand)
    {
        Hand.TrailLinesWidth = 0.005f;
        Hand.GizmoAccScale = 0.2f;
        Hand.GizmoSpeedScale = 0.8f;
        Hand.GizmoJerkScale = 0.02f;



    }









}
