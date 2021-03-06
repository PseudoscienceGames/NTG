﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMapMesh : MonoBehaviour
{
	public List<Vector3> verts = new List<Vector3>();
	public List<int> tris = new List<int>();
	public List<Vector2> uvs = new List<Vector2>();

	public void GenMesh()
	{
		int vertCount = 0;
		foreach (BattleMapTile tile in GetComponent<BattleMapData>().tiles.Values)
		{
			verts.AddRange(tile.verts);
			uvs.AddRange(tile.uvs);
			foreach (int tri in tile.tris)
			{
				tris.Add(tri + vertCount);
			}
			vertCount += 6;
		}
		ExpandDoubles();
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.RecalculateNormals();
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}
	void ExpandDoubles()
	{
		List<Vector3> newVerts = new List<Vector3>();
		List<int> newTris = new List<int>();
		List<Vector2> newUVs = new List<Vector2>();
		foreach (int tri in tris)
		{
			newVerts.Add(verts[tri]);
			newTris.Add(newVerts.Count - 1);
		}
		while (newUVs.Count < newVerts.Count)
		{
			newUVs.AddRange(new List<Vector2> { new Vector2(0, 0), new Vector2(0.5f, 1), new Vector2(1, 0) });
		}
		uvs = newUVs;
		verts = newVerts;
		tris = newTris;
	}
}
