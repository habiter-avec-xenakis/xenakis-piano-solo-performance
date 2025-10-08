using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Globalization;

public class UnityRecieve_FACEMOTION3D : MonoBehaviour
{
	// broadcast address
	public bool gameStartWithConnect = false;
	public string iOS_IPAddress = "255.255.255.255";
	private int send_port = 49993;
	private UdpClient client;
	private bool StartFlag = true;

	//object names
	public bool specifyObjectNameInsideUnity = false;
	public string faceObjectGroupName = "";
	public string headBoneName = "";
	public string rightEyeBoneName = "";
	public string leftEyeBoneName = "";
	public string neckBoneName = "";
	public string spineBoneName = "";
	public string headPositionObjectName = "";

	private UdpClient udp;
	private Thread thread;
	private SkinnedMeshRenderer meshTarget;
	private List<SkinnedMeshRenderer> meshTargetList;
	private string faceObjGrpName = "";
	private string objectNames = "";
	private List<GameObject> objectArray;
	private List<GameObject> headObjectArray;
	private List<GameObject> rightEyeObjectArray;
	private List<GameObject> leftEyeObjectArray;
	private List<GameObject> neckObjectArray;
	private List<GameObject> spineObjectArray;
	private List<GameObject> headPositionObjectArray;

	private string messageString = "";
	public int LOCAL_PORT = 50003;

	//blendShape settings
	public bool ignore_iOS_blendShapePrefix = true;
	public bool UsePartialMatches = true;
	public string blendShapePrefixInUnity = "";
	
	// tcp
	public bool useTCP = false;
	private string combineString = "";
	public string IPAddressOfThisPC = "0.0.0.0";
	private TcpListener tcpListener;
	private Thread tcpListenerThread;
	private TcpClient connectedTcpClient;

	// Start is called 

	void StartFunction()
	{
		if (StartFlag == true)
		{
			StartFlag = false;

			//Find Game Objects inside Unity Settings
			if (specifyObjectNameInsideUnity == true)
			{
				FindGameObjectsInsideUnitySettings();
			}

			//Send to iOS
			if (gameStartWithConnect == true)
			{
				Connect_to_iOS_App();
			}

			if (useTCP == true)
			{
				//Recieve tcp from iOS
				CreateTcpServer();
			}
			else
			{
				//Recieve udp from iOS
				CreateUdpServer();
			}
		}

	}

	void Start()
	{
		StartFunction();
	}

	void CreateTcpServer()
	{
		//https://gist.github.com/danielbierwirth/0636650b005834204cb19ef5ae6ccedb
		tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequests));
		tcpListenerThread.IsBackground = true;
		tcpListenerThread.Start();
	}

	void CreateUdpServer()
	{
		udp = new UdpClient(LOCAL_PORT);
		udp.Client.ReceiveTimeout = 5;

		thread = new Thread(new ThreadStart(ThreadMethod));
		thread.Start();
	}

	private void ListenForIncommingRequests()
	{
		try
		{
			// Create listener
			tcpListener = new TcpListener(IPAddress.Parse(IPAddressOfThisPC), LOCAL_PORT);
			tcpListener.Start();
			//Debug.Log("Server is listening");

			Byte[] bytes = new Byte[8096];
			while (true)
			{
				using (connectedTcpClient = tcpListener.AcceptTcpClient())
				{
					// Get a stream object for reading
					using (NetworkStream stream = connectedTcpClient.GetStream())
					{
						int length;
						// Read incomming stream into byte arrary.
						while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
						{
							var incommingData = new byte[length];
							Array.Copy(bytes, 0, incommingData, 0, length);
							// Convert byte array to string message.
							string clientMessage = Encoding.ASCII.GetString(incommingData);

							combineString += clientMessage;
							string[] del = { "___FACEMOTION3D" };
							string[] splited = combineString.Split(del, StringSplitOptions.None);
							int splited_len = splited.Length;
							if (splited_len == 1)
							{
								continue;
							}

							int counter = 0;
							foreach (string sp in splited)
							{
								combineString = sp;
								counter += 1;
								if (counter < splited_len)
								{
									messageString = combineString;
									combineString = "";
								}
							}
						}
					}
				}
			}
		}
		catch
		{
		}
	}

	IEnumerator WaitProcess(float WaitTime)
	{
		yield return new WaitForSeconds(WaitTime);
	}

	void Connect_to_iOS_App()
	{
		string sendMessage = "UnityDirectConnect_FACEMOTION3D";
		if (useTCP == true)
		{
			sendMessage += "|protocol=tcp";
		}
		SendMessage_to_iOSapp(sendMessage);
	}

	void StopStreaming_iOS_App()
	{
		SendMessage_to_iOSapp("StopStreaming_FACEMOTION3D");
	}

	void SendMessage_to_iOSapp(string sendMessage)
	{
		try
		{
			client = new UdpClient();
			client.Connect(iOS_IPAddress, send_port);
			byte[] dgram = Encoding.UTF8.GetBytes(sendMessage);
			client.Send(dgram, dgram.Length);
			StartCoroutine(WaitProcess(0.01f));
			client.Send(dgram, dgram.Length);
			StartCoroutine(WaitProcess(0.01f));
			client.Send(dgram, dgram.Length);
			StartCoroutine(WaitProcess(0.01f));
		}
		catch { }
		try
		{
			if (iOS_IPAddress.Contains(".255") == false)
			{
				var tcpClient = new TcpClient();
				var result = tcpClient.BeginConnect(iOS_IPAddress, send_port, null, null);
				var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(2));
				if (success)
				{
					tcpClient.EndConnect(result);
					var networkStream = tcpClient.GetStream();
					var buffer = Encoding.UTF8.GetBytes(sendMessage);
					networkStream.Write(buffer, 0, buffer.Length);
				}
			}
		}
		catch { }
	}

	// Update is called once per frame
	void Update()
	{
		try
		{
			if (specifyObjectNameInsideUnity == false)
			{
				SetAnimation_from_iOS_settings();
			}
			else if (specifyObjectNameInsideUnity == true)
			{
				SetAnimation_inside_Unity_settings();
			}
		}
		catch
		{ }
	}

	void SetAnimation_from_iOS_settings()
	{
		string[] strArray1 = messageString.Split('=');

		if (strArray1.Length == 2)
		{
			//blendShapes
			foreach (string message in strArray1[0].Split('|'))
			{
				string[] strArray2 = message.Split('&');

				if (strArray2.Length == 1)
				{
					string[] strArray3 = strArray2[0].Split('!');

					if (strArray3[0] == "faceObjGrp")
					{
						if (faceObjGrpName != strArray3[1])
						{
							faceObjGrpName = strArray3[1];
							meshTargetList = new List<SkinnedMeshRenderer>();

							GameObject faceObjGrp = GameObject.Find(faceObjGrpName);
							if (faceObjGrp != null)
							{
								List<GameObject> list = FACEMOTION3D_GetAllChildren.GetAll(faceObjGrp);

								foreach (GameObject obj in list)
								{
									meshTarget = obj.GetComponent<SkinnedMeshRenderer>();
									if (meshTarget != null)
									{
										if (HasBlendShapes(meshTarget) == true)
										{
											meshTargetList.Add(meshTarget);
										}
									}
								}
							}
						}
					}
				}
				else if (strArray2.Length == 2)
				{
					SetBlendShapeWeightFromStrArray(strArray2);
				}
			}
			string objectNamesNow = GetCombineNames(strArray1[1]);

			if (objectNamesNow != objectNames)
			{
				UpdateObjectArray(strArray1[1]);
			}


			int objectArrayIndex = 0;
			//jointNames
			foreach (string message in strArray1[1].Split('|'))
			{
				string[] strArray2 = message.Split('#');

				if (strArray2.Length == 2)
				{
					string[] commaList = strArray2[1].Split(',');
					string[] objNameList = commaList[3].Split(':');
					for (int j = 0; j < objNameList.Length; j++)
					{
						if (objectArray[objectArrayIndex] != null)
						{
							if (strArray2[0].Contains("Position"))
							{
								objectArray[objectArrayIndex].transform.localPosition = new Vector3(float.Parse(commaList[0], CultureInfo.InvariantCulture), float.Parse(commaList[1], CultureInfo.InvariantCulture), float.Parse(commaList[2], CultureInfo.InvariantCulture));
							}
							else
							{
								objectArray[objectArrayIndex].transform.localRotation = Quaternion.Euler(float.Parse(commaList[0], CultureInfo.InvariantCulture), float.Parse(commaList[1], CultureInfo.InvariantCulture), float.Parse(commaList[2], CultureInfo.InvariantCulture));
							}
						}
						objectArrayIndex++;
					}
				}
			}
		}
	}

	void SetBlendShapeWeightFromStrArray(string[] strArray2)
	{
		string mappedShapeName = strArray2[0];
		if (ignore_iOS_blendShapePrefix == true)
		{
			var prefix_strArray = mappedShapeName.Split(new[] { '.' }, 2);
			if (prefix_strArray.Length == 2)
			{
				mappedShapeName = prefix_strArray[1];
			}
		}

		if (blendShapePrefixInUnity != "")
		{
			System.Text.StringBuilder sb_blendShape = new System.Text.StringBuilder();
			sb_blendShape.Append(blendShapePrefixInUnity);
			sb_blendShape.Append(mappedShapeName);
			mappedShapeName = sb_blendShape.ToString();
		}

		float weight = float.Parse(strArray2[1], CultureInfo.InvariantCulture);

		foreach (SkinnedMeshRenderer meshTarget in meshTargetList)
		{
			var shared_mesh = meshTarget.sharedMesh;
			if (UsePartialMatches == false)
			{
				int index = shared_mesh.GetBlendShapeIndex(mappedShapeName);
				if (index > -1)
				{
					meshTarget.SetBlendShapeWeight(index, weight);
				}
			}
			else
			{
				for (int b_count = 0; b_count < shared_mesh.blendShapeCount; b_count++)
				{
					string sharedMeshBlendShapeName = shared_mesh.GetBlendShapeName(b_count);
					if (sharedMeshBlendShapeName.Contains(mappedShapeName))
					{
						meshTarget.SetBlendShapeWeight(b_count, weight);
						break;
					}
				}
			}
		}
	}

	void SetAnimation_inside_Unity_settings()
	{
		try
		{
			string[] strArray1 = messageString.Split('=');

			if (strArray1.Length == 2)
			{
				//blendShapes
				foreach (string message in strArray1[0].Split('|'))
				{
					string[] strArray2 = message.Split('&');
					if (strArray2.Length == 2)
					{
						SetBlendShapeWeightFromStrArray(strArray2);
					}
				}

				foreach (string message in strArray1[1].Split('|'))
				{
					string[] strArray2 = message.Split('#');

					if (strArray2.Length == 2)
					{
						string[] commaList = strArray2[1].Split(',');
						if (strArray2[0] == "head")
						{
							foreach (GameObject headObject in headObjectArray)
							{
								headObject.transform.localRotation = Quaternion.Euler(float.Parse(commaList[0], CultureInfo.InvariantCulture), float.Parse(commaList[1], CultureInfo.InvariantCulture), float.Parse(commaList[2], CultureInfo.InvariantCulture));
							}
						}
						else if (strArray2[0] == "rightEye")
						{
							foreach (GameObject rightEyeObject in rightEyeObjectArray)
							{
								rightEyeObject.transform.localRotation = Quaternion.Euler(float.Parse(commaList[0], CultureInfo.InvariantCulture), float.Parse(commaList[1], CultureInfo.InvariantCulture), float.Parse(commaList[2], CultureInfo.InvariantCulture));
							}
						}
						else if (strArray2[0] == "leftEye")
						{
							foreach (GameObject leftEyeObject in leftEyeObjectArray)
							{
								leftEyeObject.transform.localRotation = Quaternion.Euler(float.Parse(commaList[0], CultureInfo.InvariantCulture), float.Parse(commaList[1], CultureInfo.InvariantCulture), float.Parse(commaList[2], CultureInfo.InvariantCulture));
							}
						}
						else if (strArray2[0] == "neck")
						{
							foreach (GameObject neckObject in neckObjectArray)
							{
								neckObject.transform.localRotation = Quaternion.Euler(float.Parse(commaList[0], CultureInfo.InvariantCulture), float.Parse(commaList[1], CultureInfo.InvariantCulture), float.Parse(commaList[2], CultureInfo.InvariantCulture));
							}
						}
						else if (strArray2[0] == "spine")
						{
							foreach (GameObject spineObject in spineObjectArray)
							{
								spineObject.transform.localRotation = Quaternion.Euler(float.Parse(commaList[0], CultureInfo.InvariantCulture), float.Parse(commaList[1], CultureInfo.InvariantCulture), float.Parse(commaList[2], CultureInfo.InvariantCulture));
							}
						}
						else if (strArray2[0] == "headPosition")
						{
							foreach (GameObject headPositionObject in headPositionObjectArray)
							{
								headPositionObject.transform.localPosition = new Vector3(float.Parse(commaList[0], CultureInfo.InvariantCulture), float.Parse(commaList[1], CultureInfo.InvariantCulture), float.Parse(commaList[2], CultureInfo.InvariantCulture));
							}
						}
					}
				}
			}
		}
		catch
		{
		}
	}

	void FindGameObjectsInsideUnitySettings()
	{
		//Find BlendShape Objects
		meshTargetList = new List<SkinnedMeshRenderer>();

		GameObject faceObjGrp = GameObject.Find(faceObjectGroupName);
		if (faceObjGrp != null)
		{
			List<GameObject> list = FACEMOTION3D_GetAllChildren.GetAll(faceObjGrp);

			foreach (GameObject obj in list)
			{
				meshTarget = obj.GetComponent<SkinnedMeshRenderer>();
				if (meshTarget != null)
				{
					if (HasBlendShapes(meshTarget) == true)
					{
						meshTargetList.Add(meshTarget);
					}
				}
			}
		}

		//Find Bone Objects
		headObjectArray = new List<GameObject>();
		foreach (string headString in headBoneName.Split(','))
		{
			GameObject headObject = GameObject.Find(headString);
			if (headObject != null)
			{
				headObjectArray.Add(headObject);
			}
		}

		rightEyeObjectArray = new List<GameObject>();
		foreach (string rightEyeString in rightEyeBoneName.Split(','))
		{
			GameObject rightEyeObject = GameObject.Find(rightEyeString);
			if (rightEyeObject != null)
			{
				rightEyeObjectArray.Add(rightEyeObject);
			}
		}

		leftEyeObjectArray = new List<GameObject>();
		foreach (string leftEyeString in leftEyeBoneName.Split(','))
		{
			GameObject leftEyeObject = GameObject.Find(leftEyeString);
			if (leftEyeObject != null)
			{
				leftEyeObjectArray.Add(leftEyeObject);
			}
		}

		neckObjectArray = new List<GameObject>();
		foreach (string neckString in neckBoneName.Split(','))
		{
			GameObject neckObject = GameObject.Find(neckString);
			if (neckObject != null)
			{
				neckObjectArray.Add(neckObject);
			}
		}

		spineObjectArray = new List<GameObject>();
		foreach (string spineString in spineBoneName.Split(','))
		{
			GameObject spineObject = GameObject.Find(spineString);
			if (spineObject != null)
			{
				spineObjectArray.Add(spineObject);
			}
		}

		headPositionObjectArray = new List<GameObject>();
		foreach (string headPositionString in headPositionObjectName.Split(','))
		{
			GameObject headPositionObject = GameObject.Find(headPositionString);
			if (headPositionObject != null)
			{
				headPositionObjectArray.Add(headPositionObject);
			}
		}

	}

	void ThreadMethod()
	{
		//Process once every 5ms
		long next = DateTime.Now.Ticks + 50000;
		long now;

		while (true)
		{
			try
			{
				IPEndPoint remoteEP = null;
				byte[] data = udp.Receive(ref remoteEP);
				messageString = Encoding.ASCII.GetString(data);
			}
			catch
			{
			}

			do
			{
				now = DateTime.Now.Ticks;
			}
			while (now < next);
			next += 50000;
		}
	}

	private string GetCombineNames(string strArray)
	{
		StringBuilder sb = new StringBuilder("");
		foreach (string message in strArray.Split('|'))
		{
			if (message != "")
			{
				string[] strArray1 = message.Split('#');
				string[] commaList = strArray1[1].Split(',');
				sb.Append(commaList[3]);
				sb.Append(",");
			}

		}
		return sb.ToString();
	}

	private void UpdateObjectArray(string strArray)
	{
		objectArray = new List<GameObject>();
		foreach (string message in strArray.Split('|'))
		{
			if (message != "")
			{
				string[] strArray1 = message.Split('#');
				string[] commaList = strArray1[1].Split(',');
				string[] objNameList = commaList[3].Split(':');
				for (int i = 0; i < objNameList.Length; i++)
				{
					GameObject bufObj = GameObject.Find(objNameList[i]);
					objectArray.Add(bufObj);
				}
			}
		}
	}

	public string GetMessageString()
	{
		return messageString;
	}

	void OnEnable()
	{
		StartFunction();
	}

	void OnDisable()
	{
		try
		{
			OnApplicationQuit();
		}
		catch
		{
		}
	}

	void OnApplicationQuit()
	{
		if (StartFlag == false)
		{
			StartFlag = true;
			if (useTCP == true)
			{
				StopTCP();
			}
			else
			{
				StopUDP();
			}
		}
	}

	public void StopTCP()
	{
		if (gameStartWithConnect == true)
		{
			StopStreaming_iOS_App();
		}
		tcpListenerThread.Abort();
	}

	public void StopUDP()
	{
		if (gameStartWithConnect == true)
		{
			StopStreaming_iOS_App();
		}
		udp.Dispose();
		thread.Abort();
	}

	private bool HasBlendShapes(SkinnedMeshRenderer skin)
	{
		if (!skin.sharedMesh)
		{
			return false;
		}

		if (skin.sharedMesh.blendShapeCount <= 0)
		{
			return false;
		}

		return true;
	}

}
public static class FACEMOTION3D_GetAllChildren
{
	public static List<GameObject> GetAll(this GameObject obj)
	{
		List<GameObject> allChildren = new List<GameObject>();
		allChildren.Add(obj);
		GetChildren(obj, ref allChildren);
		return allChildren;
	}

	public static void GetChildren(GameObject obj, ref List<GameObject> allChildren)
	{
		Transform children = obj.GetComponentInChildren<Transform>();
		if (children.childCount == 0)
		{
			return;
		}
		foreach (Transform ob in children)
		{
			allChildren.Add(ob.gameObject);
			GetChildren(ob.gameObject, ref allChildren);
		}
	}
}