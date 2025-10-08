using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDebug : MonoBehaviour {



    Mesh Mesh; 


	// Use this for initialization
	void Start () {
        MeshFilter MF = gameObject.GetComponent<MeshFilter>();
        Mesh = MF.mesh;


	}
	
	// Update is called once per frame
	void Update () {
        DebugMesh();

    }



    void DebugMesh()
    {
        for (int i = 0; i < Mesh.uv.Length; i++)
        {
            Vector3 Pos = Mesh.vertices[i];
            Vector2 UV = Mesh.uv[i];
        }




    }
}
