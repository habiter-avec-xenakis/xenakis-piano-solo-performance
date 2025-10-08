using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using extOSC;

public class UIOSCMyos : MonoBehaviour
{
    public OSCMyos oscMyos;

    public UIOSCMyosValueBlock leftForceMean;
    public UIOSCMyosValueBlock leftForce1;
    public UIOSCMyosValueBlock leftForce2;
    public UIOSCMyosValueBlock leftForce3;
    public UIOSCMyosValueBlock leftForce4;
    public UIOSCMyosValueBlock leftForce5;
    public UIOSCMyosValueBlock leftForce6;
    public UIOSCMyosValueBlock leftForce7;
    public UIOSCMyosValueBlock leftForce8;

    public UIOSCMyosValueBlock rightForceMean;
    public UIOSCMyosValueBlock rightForce1;
    public UIOSCMyosValueBlock rightForce2;
    public UIOSCMyosValueBlock rightForce3;
    public UIOSCMyosValueBlock rightForce4;
    public UIOSCMyosValueBlock rightForce5;
    public UIOSCMyosValueBlock rightForce6;
    public UIOSCMyosValueBlock rightForce7;
    public UIOSCMyosValueBlock rightForce8;

    private void Update()
    {
        leftForceMean.textValue.text = FormattedValue(oscMyos.lForceMean);
        leftForce1.textValue.text = FormattedValue(oscMyos.lForce8[0]);
        leftForce2.textValue.text = FormattedValue(oscMyos.lForce8[1]);
        leftForce3.textValue.text = FormattedValue(oscMyos.lForce8[2]);
        leftForce4.textValue.text = FormattedValue(oscMyos.lForce8[3]);
        leftForce5.textValue.text = FormattedValue(oscMyos.lForce8[4]);
        leftForce6.textValue.text = FormattedValue(oscMyos.lForce8[5]);
        leftForce7.textValue.text = FormattedValue(oscMyos.lForce8[6]);
        leftForce8.textValue.text = FormattedValue(oscMyos.lForce8[7]);

        rightForceMean.textValue.text = FormattedValue(oscMyos.rForceMean);
        rightForce1.textValue.text = FormattedValue(oscMyos.rForce8[0]);
        rightForce2.textValue.text = FormattedValue(oscMyos.rForce8[1]);
        rightForce3.textValue.text = FormattedValue(oscMyos.rForce8[2]);
        rightForce4.textValue.text = FormattedValue(oscMyos.rForce8[3]);
        rightForce5.textValue.text = FormattedValue(oscMyos.rForce8[4]);
        rightForce6.textValue.text = FormattedValue(oscMyos.rForce8[5]);
        rightForce7.textValue.text = FormattedValue(oscMyos.rForce8[6]);
        rightForce8.textValue.text = FormattedValue(oscMyos.rForce8[7]);
    }

    private string FormattedValue(float value)
    {
        return value.ToString("0.000");
    }
}
