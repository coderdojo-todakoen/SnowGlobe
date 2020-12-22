using System.Collections.Generic;
using UnityEngine;

public class Globe : MonoBehaviour
{
    void Awake()
    {
        // ドーム部分のメッシュを作成します
        MakeMesh();
    }

    // ドーム部分のメッシュを作成します
    private void MakeMesh()
    {
        // メッシュを作成します
        Mesh mesh = new Mesh();

        // 頂点の座標を格納するリスト
        List<Vector3> vertices = new List<Vector3>();

        // 最上部の頂点をリストへ格納します
        vertices.Add(new Vector3(0, 1, 0));

        // ドームを分割して、上から頂点の座標をリストへ格納します
        const int DIV = 8;
        for (int i = 1; i != DIV; ++i) {
            float t = i * Mathf.PI / DIV;
            for (int j = 0; j != 2 * DIV; ++j) {
                float p = j * Mathf.PI / DIV;

                float x = Mathf.Sin(t) * Mathf.Cos(p);
                float z = Mathf.Sin(t) * Mathf.Sin(p);
                float y = Mathf.Cos(t);
                vertices.Add(new Vector3(x, y, z));
            }
        }

        // 頂点座標をメッシュへ格納します
        mesh.vertices = vertices.ToArray();

        // メッシュの三角形のリスト
        List<int> triangles = new List<int>();

        // 最上部の三角形をリストへ格納します
        for (int j = 0; j != 2 * DIV; ++j) {
            if (j < 2 * DIV - 1) {
                triangles.Add(0); triangles.Add(j + 1); triangles.Add(j + 2);
            } else {
                triangles.Add(0); triangles.Add(j + 1); triangles.Add(1);
            }
        }

        // 上から三角形をリストへ格納します
        for (int i = 1; i != DIV - 2; ++i) {
            for (int j = 0; j != 2 * DIV; ++j) {
                if (j < 2 * DIV - 1) {
                    triangles.Add(2 * (i - 1) * DIV + j + 1); triangles.Add(2 * i * DIV + j + 1); triangles.Add(2 * (i - 1) * DIV + j + 2);
                    triangles.Add(2 * (i - 1) * DIV + j + 2); triangles.Add(2 * i * DIV + j + 1); triangles.Add(2 * i * DIV + j + 2);
                } else {
                    triangles.Add(2 * (i - 1) * DIV + j + 1); triangles.Add(2 * i * DIV + j + 1); triangles.Add(2 * (i - 2) * DIV + j + 2);
                    triangles.Add(2 * (i - 2) * DIV + j + 2); triangles.Add(2 * i * DIV + j + 1); triangles.Add(2 * (i - 1) * DIV + j + 2);
                }
            }
        }

        // 三角形のリストをメッシュへ格納します
        mesh.triangles = triangles.ToArray();

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
