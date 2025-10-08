//=====================================
//          Ribbon Renderer
// Create by Vincent MEYRUEIS 2018
// INREV Dept ATI University Paris8
//            Version 1.0
//=====================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonRenderer : MonoBehaviour {

    //Ribbon
    public List<Vector3> Vectors = new List<Vector3>();
    public List<Vector3> Positions = new List<Vector3>();
    //public int Leght;

    //Mesh Data
    Mesh Mesh;
    List<Vector3> vertices = new List<Vector3>();
    List<Color> colors = new List<Color>();
    List<int> triangles = new List<int>();
    List<Vector2> UVs = new List<Vector2>();

    //Material and Color
    public Gradient Color = new Gradient();
    public AnimationCurve Alpha = new AnimationCurve();  
    public Material Material;
    public Texture2D Texture;
    public int TextureSize = 128;




    // Use this for initialization
    void InitRibbon () {

        MeshFilter MF = GetComponent<MeshFilter>();
        if (!MF){
            MF = gameObject.AddComponent<MeshFilter>();
        }

        MeshRenderer MR = GetComponent<MeshRenderer>();
        if (!MR) {
            MR = gameObject.AddComponent<MeshRenderer>();
        }

        if (!MF.mesh) {
            Mesh = new Mesh();
            Mesh.name = "RibbonMesh";
            MF.mesh = Mesh;
        }
        else
        {
            Mesh = MF.mesh;
            Mesh.name = "RibbonMesh";
            MF.mesh = Mesh;
        }

        MR.material = Material;

        if (!Texture)
        {
           
            Texture = new Texture2D(TextureSize, TextureSize, TextureFormat.ARGB32,false);
            Texture.name = "RibbonTexture";

            Texture.wrapMode = TextureWrapMode.Clamp;

            UpdateTexture();
        }

        MR.material.mainTexture = Texture;


    }


    void Start(){
        InitRibbon();
    }

    // Update is called once per frame
    void Update () {

        UpdateTexture();
      
        UpdateMesh();


    }


    void UpdateTexture() {

        for (int u = 0; u < TextureSize; u++)
        {
            for (int v = 0; v < TextureSize; v++)
            {
                Color PixelColor = new Color();

                float ColorLength = (float)v / TextureSize;
                float AlphaLength = (float)u / TextureSize;

                //Color
                PixelColor = Color.Evaluate(1-ColorLength);

                //Alpha
                PixelColor.a *= Alpha.Evaluate(1-AlphaLength);

                Texture.SetPixel(u, v, PixelColor);
            }
        }

        Texture.Apply();
    }


    void UpdateMesh(){
        
        //UpdateMesh
       
        Mesh.Clear();
        vertices.Clear();
        colors.Clear();
        triangles.Clear();
        UVs.Clear();

        //Verticies
        for (int i = 0; i < Vectors.Count; i++)
        {
            //Compute normalized length
            float Length = (float)i / (Vectors.Count - 1);
            Vector4 TPos;


            //Bases
            //vertices.Add( Positions[i]); // Local Pos
            TPos = (Positions[i]);
            TPos.w = 1;
            vertices.Add(transform.worldToLocalMatrix * TPos); //World Pos
            colors.Add(Color.Evaluate(1));
            UVs.Add(new Vector2(Length, 0));


            //Tips
            //vertices.Add(Positions[i] + Vectors[i]); //Local Pos
            TPos = (Positions[i] + Vectors[i]); 
            TPos.w = 1;
            vertices.Add(transform.worldToLocalMatrix * TPos); //World Pos
            colors.Add(Color.Evaluate(0));
            UVs.Add(new Vector2(Length, 1));
        }



        if (Vectors.Count >= 2)
        {
            //Tiangles
            for (int i = 0; i < Vectors.Count-1; i++)
            {
                //triangles1
                triangles.Add(2*i);
                triangles.Add(1+2*i);
                triangles.Add(2+2*i);

                //triangle2
                triangles.Add(2+2*i);
                triangles.Add(1+2*i);
                triangles.Add(3+2*i);
            }

            //Set Mesh Data
            Mesh.SetVertices(vertices);
            Mesh.SetTriangles(triangles, 0);
            Mesh.SetColors(colors);
            Mesh.uv = UVs.ToArray();

            Mesh.RecalculateNormals();
            Mesh.RecalculateBounds();
        }
    }




















}
