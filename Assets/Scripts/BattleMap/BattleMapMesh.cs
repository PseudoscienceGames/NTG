using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class BattleMapMesh : MonoBehaviour
{
	public List<Vector3> verts;
	public List<int> tris;
	public List<Vector2> uvs;
	public int triNum = 0;
	public Mesh mesh;
	private BattleMapData bmd;
	private MeshCollider col;

	public void GenMesh()
	{

		verts = new List<Vector3>();
		tris = new List<int>();
		uvs = new List<Vector2>();
		mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();
		col = GetComponent<MeshCollider>();
		bmd = GetComponent<BattleMapData>();
		triNum = 0;
		foreach (Vector2Int loc in bmd.gridLocs)
		{
			if (bmd.gridLocs.Contains(HexGrid.MoveTo(loc, 0)) && bmd.gridLocs.Contains(HexGrid.MoveTo(loc, 1)))
			{
				verts.Add(HexGrid.GridToWorld(loc));
				Vector2Int l = HexGrid.MoveTo(loc, 0);
				verts.Add(HexGrid.GridToWorld(l));
				l = HexGrid.MoveTo(loc, 1);
				verts.Add(HexGrid.GridToWorld(l));
				tris.Add(triNum++);
				tris.Add(triNum++);
				tris.Add(triNum++);
				uvs.Add(Vector2.one / 2f);
				uvs.Add(Vector2.one / 2f);
				uvs.Add(Vector2.one / 2f);
			}
			if (bmd.gridLocs.Contains(HexGrid.MoveTo(loc, 3)) && bmd.gridLocs.Contains(HexGrid.MoveTo(loc, 4)))
			{
				verts.Add(HexGrid.GridToWorld(loc));
				Vector2Int l = HexGrid.MoveTo(loc, 3);
				verts.Add(HexGrid.GridToWorld(l));
				l = HexGrid.MoveTo(loc, 4);
				verts.Add(HexGrid.GridToWorld(l));
				tris.Add(triNum++);
				tris.Add(triNum++);
				tris.Add(triNum++);
				uvs.Add(Vector2.one / 2f);
				uvs.Add(Vector2.one / 2f);
				uvs.Add(Vector2.one / 2f);
			}
		}
		//CollapseDoubles();
		//Fix Normals
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.RecalculateNormals();
		col.sharedMesh = mesh;
	}
	void CollapseDoubles()
	{
		List<Vector3> newVerts = new List<Vector3>();
		List<int> newTris = new List<int>();
		List<Vector2> newUVs = new List<Vector2>();
		foreach (int tri in tris)
		{
			bool add = true;
			foreach (Vector3 v in newVerts)
			{
				if (Vector3.Distance(v, verts[tri]) < 0.01f)
				{
					newTris.Add(newVerts.IndexOf(v));
					add = false;
				}
			}
			if (add)
			{
				newVerts.Add(verts[tri]);
				newTris.Add(newVerts.Count - 1);
			}
		}
		for (int i = 0; i < newVerts.Count; i++)
			newUVs.Add(Vector2.zero);
		uvs = newUVs;
		verts = newVerts;
		tris = newTris;

	}
}
