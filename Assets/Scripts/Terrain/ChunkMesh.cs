using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkMesh : MonoBehaviour
{
	public List<Vector3> verts;
	public List<int> tris;
	public List<Vector2> uvs;
	public int triNum = 0;
	public Mesh mesh;
	private ChunkData cd;
	private MeshCollider col;

	public void GenMesh(){
		
		verts = new List<Vector3>();
		tris = new List<int>();
		uvs = new List<Vector2>();
		mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();
		col = GetComponent<MeshCollider>();
		cd = GetComponent<ChunkData>();
		triNum = 0;
		foreach(Vector2Int loc in cd.tiles)
		{
			if(Island.instance.tiles.ContainsKey(HexGrid.MoveTo(loc, 0)) && Island.instance.tiles.ContainsKey(HexGrid.MoveTo(loc, 1)))
			{
				verts.Add(HexGrid.GridToWorld(loc, Island.instance.tiles[loc]) - cd.transform.position);
				Vector2Int l = HexGrid.MoveTo(loc, 0);
				verts.Add(HexGrid.GridToWorld(l, Island.instance.tiles[l]) - cd.transform.position);
				l = HexGrid.MoveTo(loc, 1);
				verts.Add(HexGrid.GridToWorld(l, Island.instance.tiles[l]) - cd.transform.position);
				tris.Add(triNum++);
				tris.Add(triNum++);
				tris.Add(triNum++);
				uvs.Add(Vector2.one / 2f);
				uvs.Add(Vector2.one / 2f);
				uvs.Add(Vector2.one / 2f);
			}
			if (Island.instance.tiles.ContainsKey(HexGrid.MoveTo(loc, 3)) && Island.instance.tiles.ContainsKey(HexGrid.MoveTo(loc, 4)))
			{
				verts.Add(HexGrid.GridToWorld(loc, Island.instance.tiles[loc]) - cd.transform.position);
				Vector2Int l = HexGrid.MoveTo(loc, 3);
				verts.Add(HexGrid.GridToWorld(l, Island.instance.tiles[l]) - cd.transform.position);
				l = HexGrid.MoveTo(loc, 4);
				verts.Add(HexGrid.GridToWorld(l, Island.instance.tiles[l]) - cd.transform.position);
				tris.Add(triNum++);
				tris.Add(triNum++);
				tris.Add(triNum++);
				uvs.Add(Vector2.one / 2f);
				uvs.Add(Vector2.one / 2f);
				uvs.Add(Vector2.one / 2f);
			}
		}
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.RecalculateNormals();
		col.sharedMesh = mesh;
	}
}