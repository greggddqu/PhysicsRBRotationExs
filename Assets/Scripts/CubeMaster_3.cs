using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMaster_3 : MonoBehaviour
{
    static Material lineMaterial;
    public Vector3 tPosDotxz;
    public Vector3 POS;

    public float radius = 3.0f;
    public float xPhase = 0.5f;
    public float yPhase = 0.4f;
    public float zPhase = 0.1f;

    void Update()
    {
        Vector3 tPos = Vector3.zero;
        Vector3 scale = transform.localScale;
        tPos.x = Mathf.Sin(xPhase + Time.time) * radius * scale.x;
        tPos.y = Mathf.Cos(yPhase + Time.time) * radius * scale.y;
        tPos.z = Mathf.Sin(zPhase + Time.time) * radius * scale.z;
        transform.position = tPos;
        POS = tPos;

        Vector3 unitVec_axis = new Vector3(1,0,0);//(0,1,0)
        Vector3 unitVec_zaxis = new Vector3(0, 0, 1);
        tPosDotxz = tPos - Vector3.Dot(tPos, unitVec_axis) * unitVec_axis 
            - Vector3.Dot(tPos, unitVec_zaxis) * unitVec_zaxis;
        // tPos.y

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
        GL.Vertex(POS); //position of object

        //this is the component of the line pointing from origin to object 
        //that lies in the x,z plane
        //Color color2 = Color.blue;
        //GL.Color(color2);
        //GL.Vertex3(0, 0, 0);//world origin
        //GL.Vertex(tPosxz);//x,z component of object position

        Color color3 = Color.green;
        GL.Color(color3);
        GL.Vertex3(0, 0, 0);
        GL.Vertex(tPosDotxz);//x,z component of the object position

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
