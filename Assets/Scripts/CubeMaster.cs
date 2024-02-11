using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMaster : MonoBehaviour
{
    static Material lineMaterial;
    public Vector3 POS;

    public float radius = 1.0f;
    public float xPhase = 0.5f;
    public float yPhase = 0.4f;
    public float zPhase = 0.1f;

    void FixedUpdate()
    {
        Vector3 tPos = Vector3.zero;
        tPos.x = Mathf.Cos(xPhase + Time.time) * radius;
        tPos.y = 1.0f*Mathf.Cos(yPhase + Time.time) * radius;
        tPos.z = Mathf.Sin(zPhase + Time.time) * radius;
        transform.position = tPos;
        POS = tPos;
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
        GL.Vertex3(0,0,0);//the origin
        GL.Vertex(POS); //position of object

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
