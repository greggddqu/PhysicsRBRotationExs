using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMyLine : MonoBehaviour
{
    public Vector3 myVec = Vector3.zero;
    public Vector3 myPerp = Vector3.zero;
    public Vector3 myU = Vector3.zero;
    public Vector3 myNewVec = Vector3.zero;
    Vector3 myNewVecUnity = Vector3.zero;
    public float coneAngle = 15f;
    public float myangle = 0.0f;
    public float delta_myangle = 0.0f;
    public float rotationRate = 0.0f;
    public Quaternion myq;
    public Quaternion myqs;
    static Material lineMaterial;
    public float radius = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Set up vectors
        //first the direction we want to rotate around
        myU = Vector3.one;
        myU = myU.normalized;
        //Debug.Log("This is the direction vector " + myU);

        //next set up the vector we want to rotate
        //first rotate myU by coneAngle
        //to do this we need a perpendicular to myU
        //so we cross myU into the unit x vector (or anything handy)
      
        myPerp = Vector3.Cross(Vector3.right,myU); //
        //Debug.Log("This is the vector to rotate " + myVec);

        //rotate myU about myPerp by coneAngle to get myVec
        myq = Quaternion.AngleAxis(coneAngle, myPerp);
        myVec = myq * myU;

        //Now rotate myVec about myU in steps
        rotationRate = 0.1f;

        //unity will call OnRenderObject...for now pass the vectors to render them   
    }

    // Update is called once per frame
    void Update()
    {
        delta_myangle = rotationRate * Time.deltaTime;
        myangle += delta_myangle;
        myq = Quaternion.AngleAxis(myangle, myU);
        myVec = (myq * myVec);

    }
    public void OnRenderObject()
    {

        // Draw lines
        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);
        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        GL.MultMatrix(transform.localToWorldMatrix);

        Color color1 = Color.red;
        GL.Begin(GL.LINES);
        GL.Color(color1);
        GL.Vertex3(0, 0, 0);
        GL.Vertex(myVec);

        Color color2 = Color.blue;
        GL.Color(color2);
        GL.Vertex3(0, 0, 0);
        GL.Vertex(myU);

        Color color3 = Color.green;
        GL.Color(color3);
        GL.Vertex3(0, 0, 0);
        GL.Vertex(myPerp);

        GL.End();
        GL.PopMatrix();

    }
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

}
