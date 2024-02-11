using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMaster_1 : MonoBehaviour
{
    public Quaternion myQRot;
    public float myAngle;
    public Vector3 myAxis = Vector3.zero;
    public float rotationRate = 1.0f;
    static Material lineMaterial;

    // Start is called before the first frame update
    void Start()
    {
        myAngle = 10.0f;
        myAxis = new Vector3(2.0f,-1.0f,2.0f);// Vector3.one;
        myQRot = Quaternion.AngleAxis(myAngle, myAxis);
        transform.rotation = myQRot;
    }

    // Update is called once per frame
    void Update()
    {
        myAngle += rotationRate * 5;
        myQRot = Quaternion.AngleAxis(myAngle, myAxis);
        transform.rotation = myQRot;
    }





    public void OnRenderObject()
    {

        // Draw lines
        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);
        GL.PushMatrix();

        GL.Begin(GL.LINES);

        Color color1 = Color.red;
        GL.Color(color1);
        GL.Vertex3(0, 0, 0);//the origin
        GL.Vertex(myAxis * 10.0f); //rotation axis

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
