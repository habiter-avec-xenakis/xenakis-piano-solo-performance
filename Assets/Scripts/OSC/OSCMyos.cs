using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace extOSC
{
	public class OSCMyos : MonoBehaviour
	{
		private OSCReceiver _receiver;

		[Header("OSC")]
		public string _oscAddress = "/";
		public int port;
		public string ipAdress;

		[Header("Modes")]
		public bool simulationMode = false;
		public bool testMode = false;
		[Range(0f,1f)]
		public float testModeValue;
		[Range(0f, 1f)]
		public float testModeValueLeft;
		[Range(0f, 1f)]
		public float testModeValueRight;

		private float _lForceMean;
		public float lForceMean
		{
			get { return _lForceMean; }
		}
		private float _rForceMean;
		public float rForceMean
        {
			get { return _rForceMean; }
        }

		private float[] _lForce8 = new float[8];
		public float[] lForce8
        {
			get { return _lForce8; }
        }
		private float[] _rForce8 = new float[8];
		public float[] rForce8
		{
			get { return _rForce8; }
		}

		protected virtual void Start()
		{
			_receiver = gameObject.AddComponent<OSCReceiver>();
			_receiver.LocalPort = port;
			_receiver.Bind(_oscAddress, MessageReceived);
		}

		protected virtual void Update()
		{
			if(simulationMode)
            {
				float lNoiseMain = Mathf.PerlinNoise(Time.time * 0.5f + (1.3f), 2.5f);
				float rNoiseMain = Mathf.PerlinNoise(Time.time * 0.5f + (3.3f), 0.5f);

				for (int i = 0; i < 8; i++)
				{
					_lForce8[i] = lNoiseMain + Mathf.PerlinNoise(Time.time * 10f + (i * 0.3f), 0.5f) * 0.25f;
					_rForce8[i] = rNoiseMain + Mathf.PerlinNoise(Time.time * 10f + (i * 0.2f), 2.75f) * 0.25f;
					_lForceMean = lNoiseMain;
					_rForceMean = rNoiseMain;
				}
			}

			if(testMode)
            {
				for (int i = 0; i < 8; i++)
				{
					_lForce8[i] = testModeValue;
					_rForce8[i] = testModeValue;
					_lForceMean = testModeValueLeft;
					_rForceMean = testModeValueRight;
				}
			}
		}

		protected void MessageReceived(OSCMessage message)
		{
			//Debug.Log(message);
			if(message.Values[0].StringValue == "/rhand")
            {
				if(message.Values[1].StringValue == "/rforcemean")
                {
					_rForceMean = message.Values[2].FloatValue;
				}
                if (message.Values[1].StringValue == "/rforce8")
                {
                    _rForce8[0] = message.Values[2].FloatValue;
					for(int i = 0; i < 8; i++)
                    {
						_rForce8[i] = message.Values[i + 2].FloatValue;

					}
                }
            }
			else if(message.Values[0].StringValue == "/lhand")
            {
				if (message.Values[1].StringValue == "/lforcemean")
				{
					_lForceMean = message.Values[2].FloatValue;
				}
				if (message.Values[1].StringValue == "/lforce8")
				{
					_lForce8[0] = message.Values[2].FloatValue;
					for (int i = 0; i < 8; i++)
					{
						_lForce8[i] = message.Values[i + 2].FloatValue;

					}
				}
			}
		}
		public void SetTestValueLeft(float value)
        {
			testModeValueLeft = value;
        }

		public void SetTestValueRight(float value)
        {
			testModeValueRight = value;
		}
	}
}
