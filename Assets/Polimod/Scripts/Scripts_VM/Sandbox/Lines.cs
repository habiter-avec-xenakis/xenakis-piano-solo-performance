using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lines : MonoBehaviour
{
	// When added to an object, draws colored rays from the
	// transform position.
	public Vector3 Origine;
	public Vector3 End;
	public Color Col; 

	static Material lineMaterial;
	static void CreateLineMaterial()
	{
		if (!lineMaterial)
		{
			// Unity has a built-in shader that is useful for drawing
			// simple colored things.
			Shader shader = Shader.Find("Hidden/Internal-Colored");
			lineMaterial = new Material(shader);
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			// Turn on alpha blending
			lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			// Turn backface culling off
			lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
			// Turn off depth writes
			lineMaterial.SetInt("_ZWrite", 0);
		}
	}

	// Will be called after all regular rendering is done
	public void OnDrawGizmos()
	{
		CreateLineMaterial();
		// Apply the line material
		DrawLine (Origine, End, Col);


	}

	void DrawLine(Vector3 Origine , Vector3 End, Color Col )
	{
		//GL.PushMatrix ();
		// Set transformation matrix for drawing to
		// match our transform
		//GL.MultMatrix (transform.localToWorldMatrix);

		// Draw lines
		GL.Begin (GL.LINES);

		lineMaterial.SetPass (0);
	
		// Vertex colors change from red to green
		GL.Color(Col);

		// vertex at Origne
		//GL.Vertex3(Origine.x,Origine.y,Origine.z);
		GL.Vertex3(0,0,0);

		// vertex at End
		GL.Vertex3(0,10,0);

		GL.End ();
		//GL.PopMatrix ();
	}

}