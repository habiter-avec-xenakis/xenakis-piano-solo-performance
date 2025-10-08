using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace extOSC
{
	public class OSCScoreFollower : MonoBehaviour
	{	
		private OSCReceiver _receiver;
		public Slider slider;
		public TextMeshProUGUI text;

		public string _oscAddress = "/";
		public int port;

		public float geste;

		protected virtual void Start()
		{
			_receiver = gameObject.AddComponent<OSCReceiver>();
			_receiver.LocalPort = port;
			_receiver.Bind(_oscAddress, MessageReceived);
		}

		protected void MessageReceived(OSCMessage message)
		{
			Debug.Log(message);

			geste = message.Values[0].FloatValue;
			slider.value = geste;
			text.text = geste.ToString("0.00000");
		}
	}
}
