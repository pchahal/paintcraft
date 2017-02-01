using System.Collections.Generic;
using UnityEngine;

public class UVEditor : MonoBehaviour
{
    public Vector2 uvOffset;
    List<Vector2> uv0 = new List<Vector2>();
    List<Vector2> uv1 = new List<Vector2>();


    void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        for (int i = 0; i < mesh.uv.Length; i++)
        {
            uv0.Add(new Vector2(mesh.uv[i].x, mesh.uv[i].y));
            uv1.Add(new Vector2(mesh.uv[i].x + uvOffset.x, mesh.uv[i].y + uvOffset.y));
        }
    }

    public void ChangeUVs(int uvGroup)
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        if (uvGroup == 0)
            mesh.SetUVs(0, uv0);
        else
            mesh.SetUVs(0, uv1);


    }
}





