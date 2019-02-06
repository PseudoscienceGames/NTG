using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Terrain : MonoBehaviour
{
	public int size = 100;
	public int[,] heights;
	List<Vector3> verts = new List<Vector3>();
	List<int> tris = new List<int>();
	List<Vector2> uvs = new List<Vector2>();

	private void Start()
	{
		GenTerrain();
		GenMesh();
	}

	void GenTerrain()
	{
		heights = new int[size, size];

		for (int x = 0; x < size; x++)
		{
			for (int y = 0; y < size; y++)
			{
				heights[x, y] = Mathf.RoundToInt(Mathf.PerlinNoise(x / 19f, y / 19f) * 15);
			}
		}
	}

	void GenMesh()
	{
		int vertNum = 0;
		for (int x = 0; x < size; x++)
		{
			for (int y = 0; y < size; y++)
			{
				float h = heights[x, y] / 3f;
				verts.Add(new Vector3(x, h, y));
				verts.Add(new Vector3(x, h, y + 1));
				verts.Add(new Vector3(x + 1, h, y + 1));
				verts.Add(new Vector3(x + 1, h, y));
				tris.Add(vertNum);
				tris.Add(vertNum + 1);
				tris.Add(vertNum + 2);
				tris.Add(vertNum);
				tris.Add(vertNum + 2);
				tris.Add(vertNum + 3);
				uvs.Add(new Vector2(0, 0));
				uvs.Add(new Vector2(0, 1));
				uvs.Add(new Vector2(1, 1));
				uvs.Add(new Vector2(1, 0));
				vertNum += 4;
				Debug.Log(x + " " + y);
				if(x < size - 1 && heights[x + 1, y] != h)
				{
					verts.Add(new Vector3(x + 1, h, y));
					verts.Add(new Vector3(x + 1, h, y + 1));
					verts.Add(new Vector3(x + 1, heights[x + 1, y] / 3f, y));
					tris.Add(vertNum);
					tris.Add(vertNum + 1);
					tris.Add(vertNum + 2);
					vertNum += 3;
					uvs.Add(new Vector2(1, 0));
					uvs.Add(new Vector2(0, 0));
					uvs.Add(new Vector2(1, 1));
				}
				if (x > 0 && heights[x - 1, y] != h)
				{
					verts.Add(new Vector3(x, h, y + 1));
					verts.Add(new Vector3(x, h, y));
					verts.Add(new Vector3(x, heights[x - 1, y] / 3f, y + 1));
					tris.Add(vertNum);
					tris.Add(vertNum + 1);
					tris.Add(vertNum + 2);
					vertNum += 3;
					uvs.Add(new Vector2(1, 0));
					uvs.Add(new Vector2(0, 0));
					uvs.Add(new Vector2(1, 1));
				}
				if (y < size - 1 && heights[x, y + 1] != h)
				{
					verts.Add(new Vector3(x + 1, h, y + 1));
					verts.Add(new Vector3(x, h, y + 1));
					verts.Add(new Vector3(x + 1, heights[x, y + 1] / 3f, y + 1));
					tris.Add(vertNum);
					tris.Add(vertNum + 1);
					tris.Add(vertNum + 2);
					vertNum += 3;
					uvs.Add(new Vector2(1, 0));
					uvs.Add(new Vector2(0, 0));
					uvs.Add(new Vector2(1, 1));
				}
				if (y > 0 && heights[x, y - 1] != h)
				{
					verts.Add(new Vector3(x, h, y));
					verts.Add(new Vector3(x + 1, h, y));
					verts.Add(new Vector3(x, heights[x, y - 1] / 3f, y));
					tris.Add(vertNum);
					tris.Add(vertNum + 1);
					tris.Add(vertNum + 2);
					vertNum += 3;
					uvs.Add(new Vector2(1, 0));
					uvs.Add(new Vector2(0, 0));
					uvs.Add(new Vector2(1, 1));
				}

			}
		}
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.uv = uvs.ToArray();
		Debug.Log(verts.Count);
	}
}

