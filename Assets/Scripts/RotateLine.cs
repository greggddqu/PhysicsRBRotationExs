using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLine : MonoBehaviour
{
    //This class computes shows how to rotate vectors. It plots and prints out the results
    public Vector3 myVec = Vector3.zero;
    public Vector3 myU = Vector3.zero;
    public Vector3 myNewVec = Vector3.zero;
    public float myangle = 0.0f;
    public float myangledeg = 0f;
    public Quaternion myq;
    public Quaternion myqs;
    static Material lineMaterial;
    public float radius = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Set up vectors
        //first the direction we want to rotate around
        myU = Vector3.one; //(1,1,1)
        myU = myU.normalized; //(1,1,1)/sqrt(3)
        Debug.Log("This is the direction vector " + myU);

        //next set up the vector we want to rotate
        myVec = Vector3.right; //(1,0,0)
        //Vector3.Cross(Vector3.right,myVec); //
        Debug.Log("This is the vector to rotate " + myVec);

        //rotate myVec about myU by 'angle' 
        myangle = 2.0f * Mathf.PI / 3.0f; //in radians
        myangledeg = myangle * 180 / Mathf.PI; //in degrees
        myq = Quaternion.AngleAxis(myangledeg, myU); // use quaternion.AngleAxis method transform
        Debug.Log("myq " + myq.w + " " + myq.x + " " + myq.y + " " + myq.z);

        // Unity computes the transform myNewVec = myq * myVec * myqs

        // using the short hand operator '*' and returns the rotated vector
        Vector3 myNewVecUnity = (myq * myVec);
        Debug.Log("The new vector Unity rotated with myq " + myNewVecUnity);

        // component by component this looks like:
        Vector3 vpart1 = Mathf.Cos(myangle) * myVec;
        Vector3 vpart2 = (1 - Mathf.Cos(myangle)) * Vector3.Dot(myU, myVec) * myU;
        Vector3 vpart3 = Mathf.Sin(myangle) * Vector3.Cross(myU, myVec);
        myNewVec = vpart1 + vpart2 + vpart3;
        Debug.Log("The new vector rotated by component " + myNewVec);

        //unity will call OnRenderObject...for now pass the vectors to render them
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
        GL.Vertex(myNewVec);

        GL.End();
        GL.PopMatrix();

    }

    // Update is called once per frame
    void Update()
    {
 
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
