using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class RoadMesh : MonoBehaviour
{
	public static RoadMesh Instance;
	public List<Vector2Int> roadPoints = new List<Vector2Int>();

	private void Awake()
	{
		Instance = this;
	}

	public void AddRoad(Vector2Int a, Vector2Int b)
	{
		roadPoints.Add(a);
		roadPoints.Add(b);
		DrawRoads();
	}

	private void DrawRoads()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		List<Vector3> verts = new List<Vector3>();
		List<int> tris = new List<int>();
		int triNum = 0;
		List<Vector2Int> locs = new List<Vector2Int>(roadPoints);
		while(locs.Count > 0)
		{
			Vector3 v1 = Island.instance.GridToWorld(locs[0]);
			Vector3 v2 = Island.instance.GridToWorld(locs[1]);
			locs.RemoveAt(0);
			locs.RemoveAt(0);
			verts.Add(v1);
			verts.Add(v2);
			verts.Add(v2 + (Vector3.up * 0.03f));
			verts.Add(v1 + (Vector3.up * 0.03f));
			tris.Add(triNum);
			tris.Add(triNum + 1);
			tris.Add(triNum + 2);
			tris.Add(triNum);
			tris.Add(triNum + 2);
			tris.Add(triNum + 3);
			triNum += 4;
		}
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();

	}
}
