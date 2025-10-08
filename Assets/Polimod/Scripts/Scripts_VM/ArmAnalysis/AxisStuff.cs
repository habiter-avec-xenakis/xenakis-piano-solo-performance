//=====================================
//           Axis Bones
// Create by Vincent MEYRUEIS 2017
// INREV Dept ATI University Paris8
//            Version 1.0
//=====================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AxisStuff: MonoBehaviour {


	public class AxisBone{
		
		//Bones Data
		public string Name;
		public List<Vector3> Positions;
		public List<Vector3> Speeds;	
		public List<Quaternion> Quats; 	
		public List<Vector3> Accs; 	
		public List<Vector3> Gyros;
		public List<float> Times; 
		public List<Vector3> Jerk; 	
		public List<Quaternion> Quat;


		/*
		public string Name 				= "Default-Bone";
		public List<Vector3> Positions	= new List<Vector3> ();
		public List<Vector3> Speeds		= new List<Vector3> ();
		public List<Quaternion> Quats	= new List<Quaternion> ();
		public List<Vector3> Accs		= new List<Vector3> ();
		public List<Vector3> Gyros		= new List<Vector3> ();

		public List<float> Times 		= new List<float> ();
		public List<Vector3> Jerk 		= new List<Vector3> ();
		*/



		public AxisBone (){

			Name 		= "Default-Bone";

			Positions	= new List<Vector3> ();
			Speeds		= new List<Vector3> ();
			Quats		= new List<Quaternion> ();
			Accs		= new List<Vector3> ();
			Gyros		= new List<Vector3> ();
			Times 		= new List<float> ();
			Jerk 		= new List<Vector3> ();
		}


		void Clear() {
			Positions.Clear ();
			Speeds.Clear ();
			Accs.Clear ();
			Gyros.Clear ();
			Quats.Clear ();
			Times.Clear ();
			Jerk.Clear ();
		}
	}

}

