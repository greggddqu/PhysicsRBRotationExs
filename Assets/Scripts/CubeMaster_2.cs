using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMaster_2 : MonoBehaviour
{
    
    public Quaternion myQRot;
    public float myAngle;
    public Vector3 myObjAxis = Vector3.zero;
    public Vector3 myWorldAxis = Vector3.zero;
    public float rotationRate = 1.0f;
    static Material lineMaterial;

    // Start is called before the first frame update
    void Start()
    {
        myAngle = 10.0f;
        myObjAxis = Vector3.right;
        myQRot = Quaternion.AngleAxis(myAngle, myObjAxis);
        transform.rotation = myQRot;
        myWorldAxis = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        myAngle += rotationRate;
        myQRot = Quaternion.AngleAxis(myAngle, myObjAxis);
        transform.position = new Vector3(0,2,0) + Vector3.right * 5.0f;
        transform.rotation = myQRot;
        //transform.RotateAround(Vector3.zero, myWorldAxis, myAngle);
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
        //GL.MultMatrix(transform.localToWorldMatrix);

        GL.Begin(GL.LINES);

        Color color1 = Color.red;
        GL.Color(color1);
        GL.Vertex3(0, 0, 0);//the origin
        GL.Vertex(myObjAxis * 10.0f); //rotation axis

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
