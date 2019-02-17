using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TerrainMesh : MonoBehaviour
{
	List<Vector3> verts = new List<Vector3>();
	List<int> tris = new List<int>();
	List<Vector2> uvs = new List<Vector2>();
	TerrainData data;

	public void GenMesh()
	{
		data = GetComponent<TerrainData>();
		int vertNum = 0;
		for (int x = -data.size; x < data.size; x++)
		{
			for (int y = -data.size; y < data.size; y++)
			{
				if (data.locs.ContainsKey(new Vector2Int(x, y + 1)) && data.locs.ContainsKey(new Vector2Int(x + 1, y + 1)) && data.locs.ContainsKey(new Vector2Int(x + 1, y)))
				{
					verts.Add(data.locs[new Vector2Int(x, y)]);
					verts.Add(data.locs[new Vector2Int(x, y + 1)]);
					verts.Add(data.locs[new Vector2Int(x + 1, y + 1)]);
					verts.Add(data.locs[new Vector2Int(x + 1, y)]);
					if (y % 2 == 0)
					{
						tris.Add(vertNum);
						tris.Add(vertNum + 1);
						tris.Add(vertNum + 3);
						tris.Add(vertNum + 1);
						tris.Add(vertNum + 2);
						tris.Add(vertNum + 3);
					}
					else
					{
						tris.Add(vertNum);
						tris.Add(vertNum + 1);
						tris.Add(vertNum + 2);
						tris.Add(vertNum);
						tris.Add(vertNum + 2);
						tris.Add(vertNum + 3);
					}

					uvs.Add(new Vector2(x + 50, y + 50) / 100);
					uvs.Add(new Vector2(x + 50, y + 51) / 100);
					uvs.Add(new Vector2(x + 51, y + 51) / 100);
					uvs.Add(new Vector2(x + 51, y + 50) / 100);


					//uvs.Add(new Vector2(verts[vertNum].y / 10f, 0.5f));
					//uvs.Add(new Vector2(verts[vertNum + 1].y / 10f, 0.5f));
					//uvs.Add(new Vector2(verts[vertNum + 2].y / 10f, 0.5f));
					//uvs.Add(new Vector2(verts[vertNum + 3].y / 10f, 0.5f));

					vertNum += 4;
				}
			}
		}
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.RecalculateNormals();
	}
}
